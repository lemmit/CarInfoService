using System;

namespace CarInfoService.PolishDatabase
{
    internal class PolishDatabaseUrlsProvider
    {
        private const string PdfUrl = "https://historiapojazdu.gov.pl/historia-pojazdu-web/historiaPojazdu.xhtml";
        private const string CaptchaUrl = "https://historiapojazdu.gov.pl/historia-pojazdu-web/captcha";
        private const string MainUrl = "https://historiapojazdu.gov.pl";
        private static readonly Uri MainUri = new Uri(MainUrl);

        public string GetPdfUrl() => PdfUrl;
        public string GetCaptchaUrl() => CaptchaUrl;
        public string GetMainUrl() => MainUrl;
        public Uri GetMainUri() => MainUri;
    }
}