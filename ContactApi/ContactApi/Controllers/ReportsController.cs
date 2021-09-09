using System;
using ContactApi.Messaging.Producer.Client;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IQueueProducer _queueProducer;

        public ReportsController(IQueueProducer queueProducer)
        {
            _queueProducer = queueProducer;
        }

        [HttpPost("Create")]
        public IActionResult CreateReport()
        {
            var reportRequestId = Guid.NewGuid().ToString();
            if (_queueProducer.SendReportRequest(reportRequestId))
            {
                return Ok($"Your report request queued. Report Name = {reportRequestId}.json");
            }
            return StatusCode(500, "Could not connect to ReportQueue. Please try again later");
        }
    }
}
