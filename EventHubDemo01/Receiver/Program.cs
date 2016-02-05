using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading.Tasks;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string eventHubConnectionString = "Endpoint=sb://gftesthub-ns.servicebus.windows.net/;SharedAccessKeyName=ReceiveRule;SharedAccessKey=wEO+aZpuPDKDp4Z8zBUlbLQ5CEhEtBqKfjztpo5MM3Y=";
            string eventHubName = "gftesthub";
            string storageAccountName = "gfeventhubstorage";
            string storageAccountKey = "DkOjsj/2NSWY62+N79+kJbR9EvPk5seonYab5U5g95n4qfKvPUPVAqNPY7R3QdX64HBomXmsCfBQIH6HMHABIw==";
            string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);

            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
            Console.WriteLine("Registering EventProcessor...");
            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>().Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
