
using System.Collections.Generic;
using System.Linq;
using JsonConfigurator.DAC;
using PX.Data;

namespace JsonConfigurator
{
    internal static class JsonMappingRepository
    {
        public static IEnumerable<string> GetKeys()
        {
            return PXDatabase.Select<JsonMappingConfiguration>().Select(c => c.MappingID);
        }

        public static JsonMappingConfiguration GetMappingRecord(string mappingID)
        {
            return PXDatabase.Select<JsonMappingConfiguration>().FirstOrDefault(s => s.MappingID == mappingID);
        }

        public static void InsertNewRecord(JsonMapperController.MappingRecord record)
        {
            PXDatabase.Insert<JsonMappingConfiguration>(
                new PXDataFieldAssign(nameof(JsonMappingConfiguration.MappingID), record.MappingID),
                new PXDataFieldAssign(nameof(JsonMappingConfiguration.GraphName), record.GraphName),
                new PXDataFieldAssign(nameof(JsonMappingConfiguration.ConfigString), record.ConfigString));
        }

        public static void UpdateRecord(JsonMapperController.MappingRecord record)
        {
            PXDatabase.Update<JsonMappingConfiguration>(
                new PXDataFieldRestrict(nameof(JsonMappingConfiguration.MappingID), record.MappingID),
                new PXDataFieldAssign(nameof(JsonMappingConfiguration.ConfigString), record.ConfigString));
        }
    }
}