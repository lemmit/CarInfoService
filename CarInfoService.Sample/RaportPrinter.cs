using System;
using CarInfoService.Models;

namespace CarInfoService.Sample
{
    class RaportPrinter
    {
        public void Print(Report report) {
            if (report != null) {
                System.Console.WriteLine("RAPORT:");
                Console.WriteLine(report.Brand);
                Console.WriteLine(report.Model);
                Console.WriteLine(report.Insurance);
                Console.WriteLine(report.Registered);
            }
        }
    }
}
