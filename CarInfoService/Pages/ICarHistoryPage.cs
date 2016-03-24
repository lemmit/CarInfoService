namespace CarInfoService.Pages
{
    public interface ICarHistoryPage
    {
        bool VehicleFound { get; }
        string Model { get; }
        string Brand { get; }
    }
}