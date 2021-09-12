using System;

namespace ContactApi.Messaging.Producer.Client
{
    public interface IQueueProducer
    {
        bool DeleteContactInformations(Guid contactId);
    }
}
