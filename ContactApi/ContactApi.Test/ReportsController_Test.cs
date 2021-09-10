using ContactApi.Controllers;
using ContactApi.Messaging.Producer.Client;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ContactApi.Test
{
    public class ReportsController_Test
    {
        private readonly Mock<IQueueProducer> _mockOfQueueProducer;

        public ReportsController_Test()
        {
            _mockOfQueueProducer = new Mock<IQueueProducer>();
        }

        [Fact]
        public void ReportsController_CreateReport()
        {
            _mockOfQueueProducer.Setup(x => x.SendReportRequest(It.IsAny<string>()))
                                .Returns(true);

            var reportsController = new ReportsController(_mockOfQueueProducer.Object);
            var actionResult = reportsController.CreateReport();
            Assert.IsType<OkObjectResult>(actionResult);
        }
    }
}
