using Microsoft.Azure.EventHubs.Processor;

namespace event_processor_live_data_no_checkpoints
{
    class SimpleEventProcessorFactory : IEventProcessorFactory
    {
        public SimpleEventProcessorFactory(string randomStr)
        {
            this.randomStr = randomStr;
        }
        private string randomStr;

        IEventProcessor IEventProcessorFactory.CreateEventProcessor(PartitionContext context)
        {
            return new SimpleEventProcessor(randomStr);
        }
    }
}
