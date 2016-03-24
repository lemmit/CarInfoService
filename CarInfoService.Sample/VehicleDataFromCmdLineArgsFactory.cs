using CarInfoService.Models;

namespace CarInfoService.Sample
{
    internal class VehicleDataFromCmdLineArgsFactory
    {
        public VehicleData Create(string[] args)
        {
            var sampleData = new VehicleData(args[0], args[1], args[2]
                );
            return sampleData;
        }
    }
}