using CarInfoService.Models;
using CarInfoService.Pages;

namespace CarInfoService.Services
{
    public class ReportExtractor : IReportExtractor
    {
        public Report ExtractFrom(ICarHistoryPage historyPage)
        {
            return new Report(
                model: historyPage.Model,
                brand: historyPage.Brand,
                insurance: "",
                registered: "yes"
                );
        }
    }
}