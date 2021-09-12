using System;
using System.Collections.Generic;
using ContactApi.Messaging.Producer.Client;
using ContactApi.Messaging.Producer.Settings;
using Moq;
using Xunit;

namespace ContactApi.Test
{
    public class QueueProducer_Test
    {
        public QueueProducer_Test()
        { }

        [Theory]
        [MemberData(nameof(GetRabbitMqSettingsDatas))]
        public void QueueProducer_SendReportRequest(IRabbitMqSettings settings, bool expected)
        {
            var queueProducer = new QueueProducer(settings);
            var result = queueProducer.DeleteContactInformations(It.IsAny<Guid>());
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetRabbitMqSettingsDatas() => new List<object[]>
        {
            new object[] { GetRabbitMqSettings(), true },
            new object[] { new RabbitMqSettings(), false }
        };

        private static IRabbitMqSettings GetRabbitMqSettings() => new RabbitMqSettings
        {
            Hostname = "localhost",
            Username = "guest",
            Password = "guest",
            QueueName = "ReportQueue"
        };
    }
}
