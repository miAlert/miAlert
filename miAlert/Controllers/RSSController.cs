using miAlert.Models.Home;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using miAlert.Models.RSSDatabase;
using System.Text;
using System.Data.Entity.Validation;

namespace miAlert.Controllers
{
    public class RSSController : Controller
    {
        //
        // GET: /RSS/

        public static void UpdateArticles()
        {
            XmlNodeList entries = GetFeeds();
            var testData = new RssFeedViewModel {RssItems = new List<article>()};
            foreach (XmlNode entry in entries)
            {
                if (entry.Name == "entry")
                    testData.RssItems.Add(new article()
                    {
                        Title = Regex.Replace(WebUtility.HtmlDecode(entry["title"].InnerText), "<.*?>", String.Empty),
                        Link = entry["link"].GetAttribute("href").Split(new string[] { "url?q=" }, StringSplitOptions.None)[1]
                            .Split(new string[] { "&ct=" }, StringSplitOptions.None)[0].Trim(),
                        Date = ParseGoogleDateTime(entry["published"].InnerText),
                        Content = Regex.Replace(WebUtility.HtmlDecode(entry["content"].InnerText), "<.*?>", String.Empty)
                    });
            }
            //Put Data into database
            var context = new rssEntities();
            var articles = context.articles;
            foreach (var item in testData.RssItems)
            {
                if (articles.Count(a => a.Link == item.Link) == 0)
                {
                    var tempArticle = new article
                    {
                        Link = item.Link,
                        Title = item.Title,
                        Content = item.Content,
                        Date = item.Date
                    };
                    articles.Add(tempArticle);
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                
            }
        }
        private static XmlNodeList GetFeeds()
        {
            WebClient client = new WebClient();
            XmlDocument doc = new XmlDocument();
            //XmlDocument temp = new XmlDocument();
            XmlNodeList elems;
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            //List<string> links = new List<string>();
            //links.Add("http://www.google.com/alerts/feeds/11811034629636691510/10685173545303329123");
            //links.Add("http://www.google.com/alerts/feeds/11811034629636691510/9512555827744550986");
            //foreach (string link in links)
            //{
            using (Stream data = client.OpenRead("http://www.google.com/alerts/feeds/11811034629636691510/10685173545303329123"))
            {
                using (StreamReader reader = new StreamReader(data))
                {
                    //string s = reader.ReadToEnd();
                    doc.LoadXml(reader.ReadToEnd());
                    //doc.AppendChild(doc.LoadXml(reader.ReadToEnd()));
                    elems = doc.DocumentElement.ChildNodes;
                }
            }
            //}

            //elems = doc.DocumentElement.ChildNodes;
            return elems;
        }

        private static DateTime ParseGoogleDateTime(string date)
        {
            //DateTime d = DateTime.ParseExact(date, "yyyy-MM-ddThh:mm:ssZ", CultureInfo.InvariantCulture);
            return DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        }
    }
}
