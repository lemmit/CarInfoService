using System.Collections.Specialized;
using System.Net;
using System.Text;
using CarInfoService.Clients;
using CarInfoService.Models;
using CarInfoService.Pages;
using CarInfoService.PolishDatabase.Pages;

namespace CarInfoService.PolishDatabase
{
    internal class PolishDatabaseClient : IDatabaseClient
    {
        private const string MozillaUserAgent =
            "User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";

        private readonly CookieContainer _cookieContainer;

        private readonly PolishDatabaseUrlsProvider _urlProvider;
        private readonly WebClientWithCookieHandling _webClient;
        private MainPage _mainPage;

        public PolishDatabaseClient()
        {
            _cookieContainer = new CookieContainer();
            _urlProvider = new PolishDatabaseUrlsProvider();
            SetSecurityControlParameters();
            _webClient = new WebClientWithCookieHandling(_cookieContainer);
            SetUserAgent();
            SetInitialCookies();
        }

        public ICarHistoryPage NavigateToHistoryPage(VehicleData vehicleData)
        {
            return GetCarHistoryPage(vehicleData);
        }

        public IMainPage NavigateToMainPage()
        {
            if (_mainPage == null)
            {
                _mainPage = GetMainPage();
            }
            return _mainPage;
        }


        private void SetInitialCookies()
        {
            _cookieContainer.Add(_urlProvider.GetMainUri(), new Cookie("ciasteczka", "true"));
        }

        private void SetUserAgent()
        {
            _webClient.Headers.Add("User-Agent", MozillaUserAgent);
        }

        private static void SetSecurityControlParameters()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12
                                                   | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
        }

        private CarHistoryPage GetCarHistoryPage(VehicleData vehicleData)
        {
            if (_mainPage == null)
            {
                _mainPage = GetMainPage();
            }
            var reqparam = CreateCarHistoryRequestParams(_mainPage, vehicleData);
            AddHeadersForCarHistoryRequest(vehicleData, _mainPage.ActionUrl);
            var responsebody = MakeRequestForCarHistory(_mainPage.ActionUrl, reqparam);

            var historyPage = new CarHistoryPage(responsebody);
            return historyPage;
        }

        private MainPage GetMainPage()
        {
            var unparsedMainPage = RequestMainPage();
            var mainPage = new MainPage(unparsedMainPage);
            return mainPage;
        }

        private string RequestMainPage()
        {
            var downloaded = _webClient.DownloadString(_urlProvider.GetMainUrl());
            return downloaded;
        }

        private string MakeRequestForCarHistory(string actionUrl, NameValueCollection reqparam)
        {
            var responsebytes = _webClient.UploadValues(actionUrl, "POST", reqparam);
            var responsebody = Encoding.UTF8.GetString(responsebytes);
            return responsebody;
        }

        private void AddHeadersForCarHistoryRequest(VehicleData vehicleData, string actionUrl)
        {
            _webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            _webClient.Headers["Origin"] = "https://historiapojazdu.gov.pl";
            _webClient.Headers["Referer"] = actionUrl;

            var mainUrl = _urlProvider.GetMainUri();
            _cookieContainer.Add(mainUrl, new Cookie("rej", vehicleData.RegisterationNumber));
            _cookieContainer.Add(mainUrl, new Cookie("vin", vehicleData.VehicleIndentificationNumber));
            _cookieContainer.Add(mainUrl, new Cookie("data", vehicleData.RegisterationData));
        }

        private NameValueCollection CreateCarHistoryRequestParams(MainPage mainPage, VehicleData vehicleData)
        {
            var encodedUrlVal = mainPage.EncodedUrlVal;
            var viewstateVal = mainPage.ViewStateValue;
            var rej = vehicleData.RegisterationNumber;
            var vin = vehicleData.VehicleIndentificationNumber;
            var data = vehicleData.RegisterationData;

            var reqparam = new NameValueCollection
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