using System.Collections.Generic;
using System.Linq;
using System.Net;
using CarInfoService.Pages;
using HtmlAgilityPack;

namespace CarInfoService.PolishDatabase.Pages
{
    public class MainPage : Page, IMainPage
    {
        public MainPage(string page) : base(page)
        {
        }

        private HtmlNode Descendants
        {
            get
            {
                var desc = Document.DocumentNode.Descendants("section")
                    .Single(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("box-form"));
                return desc;
            }
        }

        private IEnumerable<HtmlNode> Inputs
        {
            get
            {
                var inputs = Descendants.Descendants("input");
                return inputs;
            }
        }

        public string EncodedUrlVal
        {
            get
            {
                var encodedUrlVal = Inputs
                    .Single(
                        d =>
                            d.Attributes.Contains("name") &&
                            d.Attributes["name"].Value.Contains("javax.faces.encodedURL"))
                    .Attributes["value"].Value;
                return encodedUrlVal;
            }
        }

        public string ViewStateValue
        {
            get
            {
                var viewstate = Document.DocumentNode.SelectSingleNode("//*[@id=\"javax.faces.ViewState\"]");
                var viewstateVal = viewstate.Attributes["value"].Value;
                return viewstateVal;
            }
        }

        public string ActionUrl
        {
            get
            {
                var form = Descendants.Descendants("form").Single();
                var actionUrl = form.Attributes["action"].Value;
                actionUrl = WebUtility.HtmlDecode(actionUrl);
                return actionUrl;
            }
        }
    }
}