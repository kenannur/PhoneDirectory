using ReportApi.Messaging.Consumer.Models;

namespace ReportApi.Messaging.Consumer.Client
{
    public interface IQueueConsumer
    {
        void ProcessReport(ReportRequest request);
    }
}
