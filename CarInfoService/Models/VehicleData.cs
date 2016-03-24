namespace CarInfoService.Models
{
    public class VehicleData
    {
        public VehicleData(string registerationNumber, string vin, string registerationData)
        {
            RegisterationNumber = registerationNumber;
            RegisterationData = registerationData;
            VehicleIndentificationNumber = vin;
        }

        public string RegisterationNumber { get; }
        public string VehicleIndentificationNumber { get; }
        public string RegisterationData { get; }
    }
}