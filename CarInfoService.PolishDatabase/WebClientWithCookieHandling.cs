﻿using System;
using System.Net;

namespace CarInfoService.PolishDatabase
{
    internal class WebClientWithCookieHandling : WebClient
    {
        private readonly CookieContainer _container = new CookieContainer();

        public WebClientWithCookieHandling(CookieContainer container)
        {
            _container = container;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var r = base.GetWebRequest(address);
            var request = r as HttpWebRequest;
            if (request != null)
            {
                request.CookieContainer = _container;
            }
            return r;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            var response = base.GetWebResponse(request, result);
            ReadCookies(response);
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = base.GetWebResponse(request);
            ReadCookies(response);
            return response;
        }

        private void ReadCookies(WebResponse r)
        {
            var response = r as HttpWebResponse;
            if (response != null)
            {
                var cookies = response.Cookies;
                _container.Add(cookies);
            }
        }
    }
}