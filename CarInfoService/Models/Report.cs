namespace CarInfoService.Models
{
    public class Report
    {
        public Report(string brand, string model, string insurance, string registered)
        {
            Brand = brand;
            Model = model;
            Insurance = insurance;
            Registered = registered;
        }

        public string Brand { get; }
        public string Model { get; }
        public string Insurance { get; }
        public string Registered { get; }
    }
}