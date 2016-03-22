using HtmlAgilityPack;

namespace CarInfoService.Pages
{
    public abstract class Page
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
