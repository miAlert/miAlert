using miAlert.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using miAlert.Models.RSSDatabase;

namespace miAlert.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RssFeedViewModel testData = new RssFeedViewModel();
            testData.RssItems = new List<RssItemViewModel>();
            //Read data from database
            var _context = new rssEntities();
            var _articles = _context.articles;
            foreach (var article in _articles)
            {
                testData.RssItems.Add(new RssItemViewModel() { Url = article.Link, Title = article.Title });
            }
            return View(testData);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Process(string url)
        {
            ViewBag.Url = url;
            return View();
        }
    }
}
