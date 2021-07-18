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
        public const string Created = "201";
        public const string Accepted = "202";
        public const string NoContent = "204";
        
        
        public StatusCode() : base(new []{OK, Created, Accepted, NoContent}, new []
        {
            nameof(OK),
            nameof(Created),
            nameof(Accepted),
            "No Content"
        })
        {
            
        }
    }
}