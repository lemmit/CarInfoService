using CarInfoService.Models;
using CarInfoService.Pages;

namespace CarInfoService.Services
{
    public interface IReportExtractor
    {
        Report ExtractFrom(ICarHistoryPage historyPage);
    }
}