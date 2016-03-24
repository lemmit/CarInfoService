using System;
using System.Diagnostics;
using CarInfoService.Clients;
using CarInfoService.Models;
using CarInfoService.Services;

namespace CarInfoService
{
    public class CarInfoService
    {
        private readonly IDatabaseClient _client;
        private readonly IReportExtractor _raportExtractor;

        public CarInfoService(IDatabaseClient client,
            IReportExtractor reportExtractor)
        {
            _client = client;
            _raportExtractor = reportExtractor;
        }

        public Report GetVehicleRaport(VehicleData vehicleData)
        {
            try
            {
                var historyPage = _client.NavigateToHistoryPage(vehicleData);
                if (!historyPage.VehicleFound)
                {
                    Debug.WriteLine("Vehicle not found.");
                    return null;
                }
                return _raportExtractor.ExtractFrom(historyPage);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e);
            }
            return null;
        }
    }
}