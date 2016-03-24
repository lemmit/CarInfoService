using System;
using CarInfoService.Models;
using CarInfoService.PolishDatabase;

namespace CarInfoService.Sample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (!EnoughArgumentsSpecified(args))
            {
                ShowUsageExample();
                return;
            }
            var sampleData = ParseInputArguments(args);
            var raport = GetCarReport(sampleData);
            if (raport != null)
            {
                PrintReport(raport);
            }
            else CarInformationNotAvailable();

            WaitForUsersAction();
        }

        private static void WaitForUsersAction()
        {
            Console.ReadLine();
        }

        private static bool EnoughArgumentsSpecified(string[] args)
        {
            return args.Length >= 3;
        }

        private static VehicleData ParseInputArguments(string[] args)
        {
            var vehicleDataFromCmdLineArgsFactory = new VehicleDataFromCmdLineArgsFactory();
            return vehicleDataFromCmdLineArgsFactory.Create(args);
        }

        private static void CarInformationNotAvailable()
        {
            Console.WriteLine("Confirmation not possible. Information not available.");
        }

        private static void ShowUsageExample()
        {
            Console.WriteLine("Sample usage:\n" +
                              "carinfo <registerationNumber> <vin> <registerationData>\n" +
                              "e.g.:" +
                              "carinfo CGB23423 WGADFWEF93924f4 28.10.2007");
        }

        private static void PrintReport(Report raport)
        {
            var rp = new RaportPrinter();
            rp.Print(raport);
        }

        private static Report GetCarReport(VehicleData sampleData)
        {
            var carInfoService = new PolishDatabaseCarInfoServiceFactory().Create();
            var raport = carInfoService.GetVehicleRaport(sampleData);
            return raport;
        }
    }
}