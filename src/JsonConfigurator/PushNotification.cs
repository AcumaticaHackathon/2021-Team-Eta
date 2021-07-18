#region #Copyright
//  ----------------------------------------------------------------------------------
// COPYRIGHT (c) 2021 CONTOU CONSULTING
// ALL RIGHTS RESERVED
// AUTHOR: Kyle Vanderstoep
// CREATED DATE: 2021/07/17
// ----------------------------------------------------------------------------------
#endregion

using System.Collections.Generic;
using PX.PushNotifications.NotificationSenders;

namespace JsonConfigurator
{
    public class IntegrationPushNotificationFactory : IPushNotificationSenderFactory
    {
        public IPushNotificationSender Create(string address, string name, IDictionary<string, object> additionalParameters)
        {
            return new IntegrationPushNotification();
        }

        public string Type => "CJ";
        public string TypeDescription => "Custom JSON Request";
    }
}