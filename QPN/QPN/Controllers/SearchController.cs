using HtmlAgilityPack;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QPN.Controllers
{
    public class SearchController : ApiController
    {
        //public List<string> Get()
        //{
        //    var url = "https://szukaj.ipn.gov.pl/search?" +
        //"q=" + "test" +
        //"&site=&btnG=Szukaj&client=default_frontend&output=xml_no_dtd&proxystylesheet=default_frontend&sort=date%3AD%3AL%3Ad1&wc=200&wc_mc=1&oe=UTF-8&ie=UTF-8&ud=1&exclude_apps=1&tlen=200&size=50&doctype=WEB";

        //    var web = new HtmlWeb();
        //    var doc = web.Load(url);
        //    var nodes = doc.DocumentNode.SelectNodes(".res-item");
        //    var result = nodes.Select(n => n.InnerText).ToList();
        //    return result ?? new List<string>();
        //}
        //public IHttpActionResult GetSearch(string query)
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
        public IHttpActionResult GetSearch(string query)
        {
            var url = "https://szukaj.ipn.gov.pl/search?" +
        "q=" + query +
        "&site=&btnG=Szukaj&client=default_frontend&output=xml_no_dtd&proxystylesheet=default_frontend&sort=date%3AD%3AL%3Ad1&wc=200&wc_mc=1&oe=UTF-8&ie=UTF-8&ud=1&exclude_apps=1&tlen=200&size=50&doctype=WEB";

            var web = new HtmlWeb();
            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'res-item')]");
            var result = nodes.Select(n => BuildDiv(n)).ToList();


            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        private LinkResult BuildDiv(HtmlNode n)
        {
            var text = n.InnerText;
            var doc = new HtmlDocument();
            doc.LoadHtml(n.FirstChild.OuterHtml);
            var anchor = doc.DocumentNode.SelectSingleNode("//a");
            if (anchor != null)
            {
                string link = anchor.Attributes["href"].Value;
                text = text.Replace(link, string.Empty); 
                return new LinkResult() { Link = link, Text = text };
            }
            return new LinkResult();
        }
        class LinkResult
        {
            public string Text  { get; set; }
            public string Link  { get; set; }

        }
    }
}
