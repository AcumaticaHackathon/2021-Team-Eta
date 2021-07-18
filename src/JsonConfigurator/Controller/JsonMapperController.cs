using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using JsonConfigurator.DAC;
using PX.Common;
using PX.Data;

namespace JsonConfigurator
{
    [RoutePrefix("jsonmapping")]
    public class JsonMapperController : ApiController
    {
        // 1) A method which gets graph ID and returns structure of its views
        // 2) A method which gets JSON description made by user to save
        // 3) A method which returns previously stored JSON description
        // 4) A method which makes something useful with final JSON

        #region Request Models

        public class FieldDescription
        {
            public string ID { get; set; }
            public string Column { get; set; }

            public FieldDescription(string id, string column)
            {
                ID = id;
                Column = column;
            }
        }

        public class ViewDescription
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public List<FieldDescription> Fields { get; set; }
            public bool Primary { get; set; }

            public ViewDescription()
            {
                Fields = new List<FieldDescription>();
            }
        }

        public class ViewList
        {
            public List<ViewDescription> views;

            public ViewList()
            {
                views = new List<ViewDescription>();
            }
        }

        public class MappingRecord
        {
            public string MappingID { get; set; }

            public string GraphName { get; set; }

            public string ConfigString { get; set; }
        }

        #endregion

        #region Api methods

        [Route("saveconfig")]
        [HttpPost]
        public IHttpActionResult SaveConfig([FromBody] MappingRecord payload)
        {
            var record = JsonMappingRepository.GetMappingRecord(payload.MappingID);

            if (record is null)
            {
                JsonMappingRepository.InsertNewRecord(payload);
            }
            else
            {
                JsonMappingRepository.UpdateRecord(payload);
            }

            return Ok();
        }

        [Route("loadconfig")]
        [HttpGet]
        public IHttpActionResult LoadConfig([FromUri] string mappingId)
        {
            var record = JsonMappingRepository.GetMappingRecord(mappingId);

            return Ok(record?.ConfigString);
        }

        [Route("graphdetails")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Get("PX.Objects.SO.SOOrderEntry");
        }


        [Route("graphdetails")]
        [HttpGet]
        public IHttpActionResult Get([FromUri] string graphName)
        {
            var graph = loadGraph(graphName);
            var viewList = new ViewList();

            // Get graph views
            var fieldNames = graph.GetType().GetFields()
                .Where(f => f.FieldType.GetInheritanceChain().Contains(typeof(PXSelectBase))).Select(f => f.Name);

            fieldNames.Where(f => f.IsUiView()).ForEach(f => AddView(graph, f, viewList, f == graph.PrimaryView));

            //// Get Extension views
            //var extensions = graph.GetExtensions();
            //extensions.ForEach(e => e.Views.ForEach(v => AddView(graph, v.Value, viewList)));

            return Json(viewList);
        }

        private void AddView(PXGraph graph, string viewName, ViewList viewList, bool isPrimary = false)
        {
            var viewField = graph.GetType().GetField(viewName);
            PXViewNameAttribute viewNameAttribute = viewField
                ?.GetCustomAttribute(typeof(PXViewNameAttribute), false) as PXViewNameAttribute;

            string viewDescr = viewNameAttribute?.GetName() ?? viewName;

            var viewObject = new ViewDescription()
            {
                ID = viewName.EscapeJson(),
                Name = viewDescr.EscapeJson(),
                Primary = isPrimary
            };

            PXView view = graph.Views[viewName];
            if (view is null) return;

            foreach (Type tableType in view.BqlSelect.GetTables())
            {
                IBqlTable dac = Activator.CreateInstance(tableType) as IBqlTable;

                // Get Main Fields of the Table
                foreach (PropertyInfo property in dac.GetType().GetProperties())
                {
                    var uiAttributeName =
                        property.GetCustomAttribute<PXUIFieldAttribute>()?.DisplayName ?? property.Name;
                    viewObject.Fields.Add(
                        new FieldDescription(uiAttributeName.EscapeJson(), property.Name.EscapeJson()));
                }

                // Get Extension fields of the table
                var extensions = dac.GetExtensions() ?? Array.Empty<PXCacheExtension>();
                foreach (var extension in extensions)
                {
                    foreach (PropertyInfo property in extension.GetType().GetProperties())
                    {
                        var uiAttributeName = property.GetCustomAttribute<PXUIFieldAttribute>()?.DisplayName ??
                                              property.Name;
                        viewObject.Fields.Add(new FieldDescription(uiAttributeName.EscapeJson(),
                            property.Name.EscapeJson()));
                    }
                }
            }

            viewList.views.Add(viewObject);
        }

        private PXGraph loadGraph(string graphName)
        {
            Type gtype = PXBuildManager.GetType(graphName, true);
            using (new PXPreserveScope())
            {
                PXGraph graph = PXGraph.CreateInstance(gtype, "");
                if (gtype == typeof(PXGraph)) graph.Caches[typeof(AccessInfo)].Current = graph.Accessinfo;
                graph.Load();
                return graph;
            }
        }

        #endregion

        #region Fields

        #endregion
    }
}