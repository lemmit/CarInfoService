using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using CarInfoService.Factories;
using CarInfoService.Models;
using CarInfoService.Pages;
using CarInfoService.Providers;
using CarInfoService.Services;

namespace CarInfoService
{
    public class CarInfoService
    {
        public Report GetVehicleRaport(VehicleData vehicleData)
        {
            try
            {
                var cc = new CookieContainer();
                var client = new HistoriaPojazduClientFactory().Create(cc);
                var urlsProvider = new HistoriaPojazduUrlsProvider();

                var mainPage = GetMainPage(client, urlsProvider);
                var historyPage = GetCarHistoryPage(vehicleData, mainPage, client, cc, urlsProvider);

                if(!historyPage.VehicleFound)
                {
                    Debug.WriteLine("Vehicle not found.");
                    return null;
                }
                return new ReportExtractor().ExtractFrom(historyPage);

            }catch(Exception e){
                Debug.WriteLine("Exception: " + e);
            }
            return null;
        }

        private static CarHistoryPage GetCarHistoryPage(VehicleData vehicleData, MainPage mainPage,
            WebClientWithCookieHandling client, CookieContainer cc, HistoriaPojazduUrlsProvider urlsProvider)
        {
            var reqparam = CreateCarHistoryRequestParams(mainPage, vehicleData);
            AddHeadersForCarHistoryRequest(vehicleData, client, mainPage.ActionUrl, cc, urlsProvider);
            var responsebody = RequestForCarHistory(client, mainPage.ActionUrl, reqparam);

            var historyPage = new CarHistoryPage(responsebody);
            return historyPage;
        }

        private static MainPage GetMainPage(WebClientWithCookieHandling client, HistoriaPojazduUrlsProvider urlsProvider)
        {
            var unparsedMainPage = RequestMainPage(client, urlsProvider);
            var mainPage = new MainPage(unparsedMainPage);
            return mainPage;
        }

        private static string RequestMainPage(WebClientWithCookieHandling client, HistoriaPojazduUrlsProvider urlsProvider)
        {
            var downloaded = client.DownloadString(urlsProvider.MainUrl);
            return downloaded;
        }

        private static string RequestForCarHistory(WebClientWithCookieHandling client, string actionUrl,
            NameValueCollection reqparam)
        {
            var responsebytes = client.UploadValues(actionUrl, "POST", reqparam);
            var responsebody = Encoding.UTF8.GetString(responsebytes);
            return responsebody;
        }

        private static void AddHeadersForCarHistoryRequest(VehicleData vehicleData, WebClientWithCookieHandling client,
            string actionUrl, CookieContainer cc, HistoriaPojazduUrlsProvider urlsProvider)
        {
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers["Origin"] = "https://historiapojazdu.gov.pl";
            client.Headers["Referer"] = actionUrl;

            cc.Add(urlsProvider.MainUri, new Cookie("rej", vehicleData.RegisterationNumber));
            cc.Add(urlsProvider.MainUri, new Cookie("vin", vehicleData.VehicleIndentificationNumber));
            cc.Add(urlsProvider.MainUri, new Cookie("data", vehicleData.RegisterationData));
        }

        private static NameValueCollection CreateCarHistoryRequestParams(MainPage mainPage, VehicleData vehicleData)
        {
            var encodedUrlVal = mainPage.EncodedUrlVal;
            var viewstateVal = mainPage.ViewStateValue;
            var rej = vehicleData.RegisterationNumber;
            var vin = vehicleData.VehicleIndentificationNumber;
            var data = vehicleData.RegisterationData;

            var reqparam = new NameValueCollection()
            {
                {
                    "_historiapojazduportlet_WAR_historiapojazduportlet_:formularz",
                    "_historiapojazduportlet_WAR_historiapojazduportlet_:formularz"
                },
                {"javax.faces.encodedURL", encodedUrlVal},
                {"_historiapojazduportlet_WAR_historiapojazduportlet_:rej", rej},
                {"_historiapojazduportlet_WAR_historiapojazduportlet_:vin", vin},
                {"_historiapojazduportlet_WAR_historiapojazduportlet_:data", data},
                {"_historiapojazduportlet_WAR_historiapojazduportlet_:btnSprawdz", "Sprawdź pojazd »"},
                {"javax.faces.ViewState", viewstateVal}
            };
            return reqparam;
        }
    }
}
