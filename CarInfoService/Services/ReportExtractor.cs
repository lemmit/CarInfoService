using CarInfoService.Models;
using CarInfoService.Pages;

namespace CarInfoService.Services
{
    public class ReportExtractor
    {
        public Report ExtractFrom(CarHistoryPage historyPage)
        {
            return new Report{
                    Model = historyPage.Model,
                    Brand = historyPage.Brand
                };
        }
    }
}