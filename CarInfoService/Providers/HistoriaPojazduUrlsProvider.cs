using System;

namespace CarInfoService.Providers
{
    public class HistoriaPojazduUrlsProvider
    {
        const string _pdfUrl = "https://historiapojazdu.gov.pl/historia-pojazdu-web/historiaPojazdu.xhtml";
        const string _captchaUrl = "https://historiapojazdu.gov.pl/historia-pojazdu-web/captcha";
        const string _mainUrl = "https://historiapojazdu.gov.pl";
        static readonly Uri _mainUri = new Uri(_mainUrl);

        public string PdfUrl => _pdfUrl;
        public string CaptchaUrl => _captchaUrl;
        public string MainUrl => _mainUrl;
        public Uri MainUri => _mainUri;
    }
}
