using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AggregatorApi.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace AggregatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IContactInformationHttpClient _contactInformationHttpClient;

        public ReportsController(IContactInformationHttpClient contactInformationHttpClient)
        {
            _contactInformationHttpClient = contactInformationHttpClient;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken)
        {
            var reportName = Guid.NewGuid().ToString();
            var result = await _contactInformationHttpClient.CreateReportAsync(reportName, cancellationToken);
            if (result is not null)
            {
                await UploadReportAsync(reportName, result);
                var reportFullPath = Path.Combine(Directory.GetCurrentDirectory(), $"{reportName}.json");
                return Ok($"Your report has been created. Report Path = {reportFullPath}");
            }
            return StatusCode(500, "Internal server error");
        }

        private static async Task UploadReportAsync(string reportName, string reportContent)
        {
            var fileDirectory = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(fileDirectory, $"{reportName}.json");
            await System.IO.File.WriteAllTextAsync(fullPath, reportContent);
        }
    }
}
