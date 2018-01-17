using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System;
using System.Threading.Tasks;

namespace event_processor_live_data_no_checkpoints
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            string eventHubPath = args[0]; // same as EntityPath
            string ehConnectionString = args[1];
            string leaseContainerName = args[2];
            string storageAccountName = args[3];
            string storageAccountKey = args[4];

            var eventProcessorHost = new EventProcessorHost(
                eventHubPath,
                PartitionReceiver.DefaultConsumerGroupName,
                ehConnectionString,
                string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey),
                leaseContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorFactoryAsync(new SimpleEventProcessorFactory("hello"), new EventProcessorOptions
            {
                InitialOffsetProvider = (partitionId) => DateTime.UtcNow
            });

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();
            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
