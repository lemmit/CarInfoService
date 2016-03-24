using System;
using CarInfoService.Models;

namespace CarInfoService.Sample
{
    internal class RaportPrinter
    {
        public void Print(Report report)
        {
            if (report == null)
            {
            }
            Console.WriteLine("RAPORT:");
            Console.WriteLine(report.Brand);
            Console.WriteLine(report.Model);
            Console.WriteLine(report.Insurance);
            Console.WriteLine(report.Registered);
        }
    }
}