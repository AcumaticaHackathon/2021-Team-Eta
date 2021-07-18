#region #Copyright
//  ----------------------------------------------------------------------------------
// COPYRIGHT (c) 2021 CONTOU CONSULTING
// ALL RIGHTS RESERVED
// AUTHOR: Kyle Vanderstoep
// CREATED DATE: 2021/07/17
// ----------------------------------------------------------------------------------
#endregion

using PX.Data.BQL;

namespace JsonConfigurator
{
    public class Constants
    {
        public const string JsonWebhookHandler = "JsonConfigurator.JsonWebhookHandler";

        public class jsonWebhookHandler : BqlString.Constant<jsonWebhookHandler>
        {
            public jsonWebhookHandler() : base(JsonWebhookHandler)
            {
            }
        }

        
    }
}