using System;
using CarInfoService.Models;

namespace CarInfoService.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Sample usage:\n" +
                                  "carinfo <registerationNumber> <vin> <registerationData>\n" +
                                  "e.g.:" +
                                  "carinfo CGB23423 WGADFWEF93924f4 28.10.2007");
                return;
            }
            var sampleData = new VehicleData
            {
                RegisterationNumber = args[0],
                VehicleIndentificationNumber = args[1],
                RegisterationData = args[2]
            };
            var carInfoService = new CarInfoService();
            var raport = carInfoService.GetVehicleRaport(sampleData);
            if (raport != null)
            {
                new RaportPrinter().Print(raport);
            }
            else Console.WriteLine("Confirmation not possible.");

        }
    }
}
