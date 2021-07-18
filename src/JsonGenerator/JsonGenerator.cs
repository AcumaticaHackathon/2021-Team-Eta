using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using PX.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Json.Net;
using PX.Objects.SO;

namespace Hackathon.Eta.JsonGenerator
{
    public class JsonGenerator
    {

        public static string Generate(string inputJson, PXGraph inputGraph)
        {
            // Parse json into json object
            //JObject inputObj = JObject.Parse(inputJson);
            string outputJson;

            //Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(inputJson);
            StringReader sr = new StringReader(inputJson);
            JsonTextReader jr = new JsonTextReader(sr);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            bool isStruct = true;

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                //writer.WriteStartObject();

                while (jr.Read())
                {
                    if (jr.TokenType == JsonToken.StartObject)
                    {
                        continue;
                    }
                    else if (jr.TokenType == JsonToken.EndObject)
                    {
                        //if (!jr.Path.Contains("content["))
                        //{
                        try
                        {
                            writer.WriteEndObject();
                        }
                        catch { }
                        //}
                        //continue;
                    }
                    else if (jr.TokenType == JsonToken.PropertyName)
                    {
                        isStruct = false;
                        string propName = jr.Value.ToString();

                        if (propName == "structure")
                        {
                            isStruct = true;
                            jr.Read(); // true
                            jr.Read(); // content
                            //writer.WriteStartArray();
                        }
                        else if (propName == "name")
                        {
                            jr.Read(); // output node
                            writer.WriteStartObject();
                            writer.WritePropertyName(jr.Value.ToString());
                        }
                        else if (propName == "value")
                        {
                            jr.Read(); // output value

                            if (jr.TokenType == JsonToken.StartObject)
                            {
                                //writer.WriteStartObject();
                                continue;
                            }
                            else if (jr.TokenType == JsonToken.StartArray)
                            {
                                writer.WriteStartArray();
                                continue;
                            }

                            writer.WriteValue(jr.Value);
                            //writer.WriteEndObject();
                        }
                        else if (propName == "bound")
                        {
                            jr.Read(); // true
                            jr.Read(); // view
                            jr.Read(); // view name

                            string viewName = jr.Value.ToString();

                            //var view = ((SOOrderEntry)inputGraph).Views[viewName];

                            jr.Read(); // field
                            jr.Read(); // field name

                            string fieldName = jr.Value.ToString();

                            //var graph = loadGraph("PX.Objects.SO.SOOrderEntry", "");
                            //var graph = PXGraph.CreateInstance<SOOrderEntry>();
                            int startRow = 0;
                            int totalRows = 1;
                            var res = inputGraph.ExecuteSelect(viewName, null, null, null, new[] { false }, null, ref startRow, 0, ref totalRows).Cast<object>().ToArray();

                            //var view = inputGraph.GetType().GetProperty(viewName).GetValue(inputGraph, null);
                            //var res = ((PXSelectBase)view).View.Select(null, null, null, null, null, null, ref startRow, 0, ref totalRows);
                            //var res = graph.Document.Select();

                            foreach (SOOrder item in res)
                            {
                                var val = item.GetType().GetProperty(fieldName).GetValue(item, null);
                                writer.WriteValue(val);
                            }
                        }

                        //writer.WritePropertyName(jr.Value.ToString());
                    }
                    else if (jr.TokenType == JsonToken.StartArray)
                    {
                        writer.WriteStartArray();
                        //continue;
                    }
                    else if (jr.TokenType == JsonToken.EndArray)
                    {
                        try
                        {
                            writer.WriteEndArray();
                        }
                        catch { }
                        //continue;
                    }
                    else if (jr.TokenType == JsonToken.String)
                    {
                        writer.WriteValue(jr.Value.ToString());
                        //writer.WriteEndObject();
                    }
                    else if (jr.TokenType == JsonToken.Boolean)
                    {
                        writer.WriteValue(jr.Value);
                        //writer.WriteEndObject();
                    }
                    else if (jr.TokenType == JsonToken.Integer)
                    {
                        writer.WriteValue(jr.Value);
                        //writer.WriteEndObject();
                    }
                    else if (jr.TokenType == JsonToken.Date)
                    {
                        writer.WriteValue(jr.Value);
                        //writer.WriteEndObject();
                    }
                }

                //writer.WriteEnd();
                //writer.WriteEndObject();

                writer.Flush();
                sw.Flush();

                outputJson = sb.ToString();
            }

            //using (JsonWriter writer = new JsonTextWriter(sw))
            //{
            //    writer.Formatting = Formatting.Indented;
            //    writer.WriteStartObject();
            //    GenerateSub(dict, outputObj, writer);
            //    writer.WriteEndObject();
            //}

            return outputJson;
        }


        private static PXGraph loadGraph(string graphName, string stateId)
        {
            //Type gtype = System.Web.Compilation.PXBuildManager.GetType(graphName, true);
            Type gtype = typeof(SOOrderEntry);
            //using (new PXPreserveScope())
            //{
                PXGraph graph = new PXGraph<SOOrderEntry>(); // PXGraph.CreateInstance(gtype);
                if (gtype == typeof(PXGraph)) graph.Caches[typeof(AccessInfo)].Current = graph.Accessinfo;
                graph.Load();
                return graph;
            //}
        }


        private static void GenerateSub(Dictionary<string, object> genObj, JObject outputObj, JsonWriter writer)
        {
            foreach (KeyValuePair<string, object> obj in genObj)
            {
                if (obj.Value is JArray) // Dictionary<string, object>)
                {
                    JArray ja = new JArray();
                    
                    ////var sub = (Dictionary<string, object>)obj.Value;
                    //GenerateSub(sub, outputObj, writer);
                    //writer.WriteEndArray();
                }
                else //if (obj.Value is string)
                {
                    JObject jo = new JObject();
                }
            }
        }
    }
}
