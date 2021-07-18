
using System;
using PX.Data;
using PX.Data.BQL;

namespace JsonConfigurator.DAC
{
    [Serializable]
    [PXCacheName("JsonMappingConfiguration")]
    public class JsonMappingConfiguration : IBqlTable
    {
        #region MappingID

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Mapping ID")]
        public virtual string MappingID { get; set; }

        public abstract class mappingID : BqlString.Field<mappingID>
        {
        }

        #endregion

        #region GraphName

        [PXDBString(100)]
        [PXUIField(DisplayName = "Graph")]
        public virtual string GraphName { get; set; }

        public abstract class graphName : BqlString.Field<graphName>
        {
        }

        #endregion

        

        #region ConfigString

        [PXDBText()]
        [PXUIField(DisplayName = "Config String")]
        public virtual string ConfigString { get; set; }

        public abstract class configString : BqlString.Field<configString>
        {
        }

        #endregion

        
    }
}