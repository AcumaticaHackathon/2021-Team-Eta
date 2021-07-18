#region #Copyright
//  ----------------------------------------------------------------------------------
// COPYRIGHT (c) 2021 CONTOU CONSULTING
// ALL RIGHTS RESERVED
// AUTHOR: Kyle Vanderstoep
// CREATED DATE: 2021/07/17
// ----------------------------------------------------------------------------------
#endregion

using System;
using System.Threading;
using PX.PushNotifications;
using PX.PushNotifications.NotificationSenders;

namespace JsonConfigurator
{
    public class IntegrationPushNotification : IPushNotificationSender
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SendAndForget(NotificationResultWrapper results, CancellationToken cancellationToken, Action<string> onSendingFailed,
            Action finalizer)
        {
            throw new NotImplementedException();
        }

        public void Send(NotificationResultWrapper results, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public string Address { get; }
        public string Name { get; }
    }
}