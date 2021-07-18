
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JsonConfigurator.Attributes;
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

        protected void _(Events.FieldUpdating< IntegrationScenarioDetail, IntegrationScenarioDetail.direction> e)
        {
            if (e.Row is null) return;
            switch (e.NewValue as string)
            {
                case DirectionType.InboundResponse:
                case DirectionType.OutboundResponse:
                    e.NewValue = e.OldValue;
                    throw new PXSetPropertyException("This value is not valid to be defined manually",
                        PXErrorLevel.Error);
                    break;
            }
        }

        protected void _(Events.RowUpdated<IntegrationScenarioDetail> e)
        {
            if (e.Row is null) return;
            string correspondingDir = "";
            switch (e.Row.Direction)
            {
                case DirectionType.InboundRequest:
                    correspondingDir = DirectionType.OutboundResponse;
                    break;
                case DirectionType.OutboundRequest:
                    correspondingDir = DirectionType.InboundResponse;
                    break;
            }

            if (Detail.SelectMain()
                .Any(d => d.Direction == correspondingDir && d.MatchingStep == e.Row.LineNbr)) return;

            Detail.Insert(new IntegrationScenarioDetail()
            {
                MatchingStep = e.Row.LineNbr,
                Direction = correspondingDir
            });
        }
    }
}