using CarInfoService.Services;

namespace CarInfoService.PolishDatabase
{
    public class PolishDatabaseCarInfoServiceFactory
    {
        public CarInfoService Create()
        {
            return new CarInfoService(
                new PolishDatabaseClient(),
                new ReportExtractor()
                );
        }
    }
}