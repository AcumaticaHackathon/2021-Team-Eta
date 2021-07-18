#region #Copyright
//  ----------------------------------------------------------------------------------
// COPYRIGHT (c) 2021 CONTOU CONSULTING
// ALL RIGHTS RESERVED
// AUTHOR: Kyle Vanderstoep
// CREATED DATE: 2021/07/17
// ----------------------------------------------------------------------------------
#endregion

using PX.Data;

namespace JsonConfigurator.Attributes
{
    public class StatusCode : PXStringListAttribute
    {
        public const string OK = "200";
        public const string Created = "202";
        public const string Unauthorized = "403";
        
        public StatusCode() : base(new []{OK, Created, Unauthorized}, new []
        {
            nameof(OK),
            nameof(Created),
            nameof(Unauthorized)
        })
        {
            
        }
    }
}