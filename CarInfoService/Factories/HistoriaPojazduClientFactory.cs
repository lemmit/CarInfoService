using System.Net;
using CarInfoService.Providers;

namespace CarInfoService.Factories
{
    public class HistoriaPojazduClientFactory
    {
        const string MozillaUserAgent = "User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
        public WebClientWithCookieHandling Create(CookieContainer cc)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12
                                                   | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };


            var client = new WebClientWithCookieHandling(cc);
            client.Headers.Add("User-Agent", MozillaUserAgent);
            var mainUri = new HistoriaPojazduUrlsProvider().MainUri;
            cc.Add(mainUri, new Cookie("ciasteczka", "true"));
            return client;
        }
    }
}
