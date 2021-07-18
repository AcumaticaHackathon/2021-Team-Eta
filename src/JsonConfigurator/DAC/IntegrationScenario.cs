
using System;
using PX.Data;
using PX.Data.BQL;

namespace JsonConfigurator.DAC
{
    [Serializable]
    [PXCacheName("IntegrationScenario")]
    public class IntegrationScenario : IBqlTable
    {
        #region Scenario ID
        [PXDBString(15, IsUnicode = true, IsKey = true)]
        [PXUIField(DisplayName = "Scenario ID")]
        [PXSelector(typeof(Search<IntegrationScenario.scenarioID>))]
        public virtual string ScenarioID { get; set; }

        public abstract class scenarioID : BqlString.Field<scenarioID>
        {
        }
        #endregion

        #region LastScenarioLineNbr

        [PXDBInt()]
        [PXDefault(0)]
        public virtual int? LastScenarioLineNbr { get; set; }

        public abstract class lastScenarioLineNbr : BqlInt.Field<lastScenarioLineNbr>
        {
        }

        #endregion

        
    }
}