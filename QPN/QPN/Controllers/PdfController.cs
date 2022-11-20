using HtmlAgilityPack;
using QPN.Models;
using System.Linq;
using System.Web.Http;

namespace QPN.Controllers
{
    public class PdfController : ApiController
    {
        //public IHttpActionResult GetPdf(PdfModel[] questions)
        //{
        //    var url = "https://szukaj.ipn.gov.pl/search?" +
        //"q=" + query +
        //"&site=&btnG=Szukaj&client=default_frontend&output=xml_no_dtd&proxystylesheet=default_frontend&sort=date%3AD%3AL%3Ad1&wc=200&wc_mc=1&oe=UTF-8&ie=UTF-8&ud=1&exclude_apps=1&tlen=200&size=50&doctype=WEB";

        //    var web = new HtmlWeb();
        //    var doc = web.Load(url);
        //    var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'res-item')]");
        //    var result = nodes.Select(n => BuildDiv(n)).ToList();


        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(result);
        //}
    }
}
