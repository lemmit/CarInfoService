using CarInfoService.Models;
using CarInfoService.Pages;

namespace CarInfoService.Clients
{
    public interface IDatabaseClient
    {
        ICarHistoryPage NavigateToHistoryPage(VehicleData vehicleData);
        IMainPage NavigateToMainPage();
    }
}