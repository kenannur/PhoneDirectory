namespace ContactApi.Messaging.Producer.Client
{
    public interface IQueueProducer
    {
        bool SendReportRequest(string requestId);
    }
}
