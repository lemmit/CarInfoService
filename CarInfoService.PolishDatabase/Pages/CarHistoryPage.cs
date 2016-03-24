using System.Linq;
using CarInfoService.Pages;
using HtmlAgilityPack;

namespace CarInfoService.PolishDatabase.Pages
{
    public class CarHistoryPage : Page, ICarHistoryPage
    {
        public CarHistoryPage(string page) : base(page)
        {
        }

        private HtmlNode Raport
        {
            get
            {
                var raport = Document.DocumentNode.Descendants("div")
                    .Single(
                        d =>
                            d.Attributes.Contains("class") &&
                            d.Attributes["class"].Value.Contains("raport-main-information"));
                return raport;
            }
        }

        public bool VehicleFound
        {
            get
            {
                var vehicle = Document.DocumentNode.SelectSingleNode("//*[@id=\"nieZnalezionoHistorii\"]");
                return vehicle == null;
            }
        }

        public string Model
        {
            get
            {
                var model =
                    Raport.SelectSingleNode(
                        "//*[@id=\"_historiapojazduportlet_WAR_historiapojazduportlet_:j_idt7:marka\"]").InnerText;
                return model;
            }
        }

        public string Brand
        {
            get
            {
                var brand =
                    Raport.SelectSingleNode(
                        "//*[@id=\"_historiapojazduportlet_WAR_historiapojazduportlet_:j_idt7:model\"]").InnerText;
                return brand;
            }
        }

        /*
           var oc_parent = raport.SelectSingleNode("//*[@id=\"_historiapojazduportlet_WAR_historiapojazduportlet_:j_idt7\"]");
            var oc_mid = oc_parent.Descendants("p")
                    .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("oc"))
                    .Single();
            var oc = oc_mid.Descendants("span").Single().InnerText;
        */
    }
}