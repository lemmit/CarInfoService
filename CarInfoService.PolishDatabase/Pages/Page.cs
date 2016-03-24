using CarInfoService.Pages;
using HtmlAgilityPack;

namespace CarInfoService.PolishDatabase.Pages
{
    public abstract class Page : IPage
    {
        protected readonly HtmlDocument Document;

        protected Page(string page)
        {
            var html = new HtmlDocument();
            html.OptionOutputAsXml = true;
            html.LoadHtml(page);
            Document = html;
        }
    }
}