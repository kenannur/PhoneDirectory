﻿using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using ReportApi.Data.Repository;
using ReportApi.Messaging.Consumer.Models;
using ReportApi.Shared.Extensions;

namespace ReportApi.Messaging.Consumer.Client
{
    public class QueueConsumer : IQueueConsumer
    {
        private readonly IServiceScopeFactory _ssFactory;

        public QueueConsumer(IServiceScopeFactory ssFactory)
        {
            _ssFactory = ssFactory;
        }

        public void ProcessReport(ReportRequest request)
        {
            using var scope = _ssFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IContactInformationRepository>();

            var locationInfos = repository.GetLocationInformations();
            var orderedLocationGroups = locationInfos.GroupBy(x => x.Value)
                                                .OrderByDescending(x => x.Count())
                                                .ToList();

            var orderedLocations = orderedLocationGroups.Select(x => x.Key);

            var registeredPeoples = orderedLocationGroups.Select(x => new RegisteredPeopleInfo
            {
                Location = x.Key,
                Count = x.Count()
            });

            var registeredPhones = orderedLocationGroups.Select(x => new RegisteredPhoneInfo
            {
                Location = x.Key,
                Count = repository.GetPhoneNumbersCountAt(x.Key)
            });

            var report = new Report();
            report.LocationInformationFromMostToLeast = orderedLocations.ToList();
            report.NumberOfPeopleRegisteredAt = registeredPeoples.ToList();
            report.NumberOfPhoneRegisteredAt = registeredPhones.ToList();

            var jReport = report.ToJson();

            var fileDirectory = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(fileDirectory, $"{request.Id}.json");
            File.WriteAllText(fullPath, jReport);
        }


    }
}