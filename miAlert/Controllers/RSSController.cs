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
            var feed = GetFeeds();
            var context = new rssEntities();
            var articles = context.articles;
            foreach (var item in feed.RssItems)
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

        private static RssFeedViewModel GetFeeds()
        {
            WebClient client = new WebClient();
            XmlDocument doc = new XmlDocument();
            RssFeedViewModel testData = new RssFeedViewModel();
            testData.RssItems = new List<article>();
            //XmlDocument temp = new XmlDocument();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            List<string> links = new List<string>();
            //links.Add("http://www.google.com/alerts/feeds/11811034629636691510/10685173545303329123");
            //links.Add("http://www.google.com/alerts/feeds/11811034629636691510/9512555827744550986");
            links.Add("http://www.google.com/alerts/feeds/11811034629636691510/2840522102031033964");
            links.Add("http://www.google.com/alerts/feeds/11811034629636691510/13622941500318588654");

            foreach (string link in links)
            {
                using (Stream data = client.OpenRead(link))
                {
                    using (StreamReader reader = new StreamReader(data))
                    {
                        //string s = reader.ReadToEnd();
                        doc.LoadXml(reader.ReadToEnd());
                        //doc.AppendChild(doc.LoadXml(reader.ReadToEnd()));
                        //elems = doc.DocumentElement.ChildNodes;

                        foreach (XmlNode entry in doc.DocumentElement.ChildNodes)
                        {
                            if (entry.Name == "entry")
                                testData.RssItems.Add(new article()
                                {
                                    Title =
                                        Regex.Replace(WebUtility.HtmlDecode(entry["title"].InnerText), "<.*?>",
                                            String.Empty),
                                    Link = 
                                        entry["link"].GetAttribute("href")
                                            .Split(new string[] { "url?q=" }, StringSplitOptions.None)[1]
                                            .Split(new string[] { "&ct=" }, StringSplitOptions.None)[0].Trim(),
                                    Date = ParseGoogleDateTime(entry["published"].InnerText),
                                    Content =
                                        Regex.Replace(WebUtility.HtmlDecode(entry["content"].InnerText), "<.*?>",
                                            String.Empty),
                                    Deleted =  false
                                });
                        }
                    }
                }
            }
            return testData;
        }

        private static DateTime ParseGoogleDateTime(string date)
        {
            //DateTime d = DateTime.ParseExact(date, "yyyy-MM-ddThh:mm:ssZ", CultureInfo.InvariantCulture);
            return DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        }

    }
}
