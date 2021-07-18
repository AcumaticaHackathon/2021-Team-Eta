
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JsonConfigurator.Attributes;
using PX.Api.Webhooks.DAC;
using PX.Data;
using PX.Data.BQL;



namespace JsonConfigurator.DAC
{
    [Serializable]
    [PXCacheName("IntegrationScenario")]
    public class IntegrationScenarioDetail : IBqlTable, ISortOrder
    {
        #region ScenarioID

        [PXDBString(15, IsUnicode = true)]
        [PXDefault(typeof(IntegrationScenario.scenarioID))]
        [PXParent(typeof(Select<IntegrationScenario, Where<IntegrationScenario.scenarioID, Equal<scenarioID>>>))]
        public virtual string ScenarioID { get; set; }

        public abstract class scenarioID : BqlString.Field<scenarioID>
        {
        }

        #endregion

        #region LineNbr

        [PXDBInt(IsKey = true)]
        [PXLineNbr(typeof(IntegrationScenario.lastScenarioLineNbr))]
        public virtual int? LineNbr { get; set; }

        public abstract class lineNbr : BqlInt.Field<lineNbr>
        {
        }

        #endregion

        #region SortOrder

        [PXDBInt()]
        [PXUIField(DisplayName = "Sort Order")]
        public virtual int? SortOrder { get; set; }

        public abstract class sortOrder : BqlInt.Field<sortOrder>
        {
        }

        #endregion

        #region Mapping

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Mapping")]
        [PXSelector(typeof(Search<JsonMappingConfiguration.mappingID>))]
        public virtual string Mapping { get; set; }

        public abstract class mapping : BqlString.Field<mapping>
        {
        }

        #endregion

        #region Direction
        [PXDBString(3)]
        [DirectionType]
        [PXUIField(DisplayName = "Direction")]
        public virtual string Direction { get; set; }

        public abstract class direction : BqlString.Field<direction>
        {
        }
        #endregion

        #region Webhook

        [PXDBGuid()]
        [PXUIField(DisplayName = "Webhook")]
        [PXUIEnabled(typeof(Where<direction, Equal<DirectionType.inboundRequest>>))]
        [PXSelector(typeof(Search<WebHook.webHookID>), DescriptionField = typeof(WebHook.name))]
        public virtual string Webhook { get; set; }

        public abstract class webhook : BqlString.Field<webhook>
        {
        }

        #endregion

        #region Url

        [PXDBString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "Url")]
        [PXUIEnabled(typeof(Where<direction, Equal<DirectionType.outboundRequest>>))]
        public virtual string Url { get; set; }

        public abstract class url : BqlString.Field<url>
        {
        }

        #endregion

        
        
    }
}