
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JsonConfigurator.DAC;
using PX.Data;


namespace JsonConfigurator
{
    public class IntegrationScenarioMaint : PXGraph<IntegrationScenarioMaint, IntegrationScenario>
    {
        public PXSelect<IntegrationScenario> Scenario;

        public PXOrderedSelect<IntegrationScenario, IntegrationScenarioDetail,
            Where<IntegrationScenarioDetail.scenarioID, Equal<Current<IntegrationScenario.scenarioID>>>, OrderBy<Asc<IntegrationScenarioDetail.sortOrder>>> Detail;

        protected void _(Events.RowInserting<IntegrationScenarioDetail> e)
        {
            if (e.Row is null) return;
            e.Row.SortOrder = Detail.Select().Count + 1;
        }
    }
}