using miAlert.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using miAlert.Models.RSSDatabase;
using miAlert.Models.AccessDatabase;

namespace miAlert.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var rssData = new RssFeedViewModel {RssItems = new List<article>()};
            //Read data from database
            var context = new rssEntities();
            var articles = context.articles.Where(a => a.Deleted == null || a.Deleted != true);
            foreach (var article in articles)
            {
                rssData.RssItems.Add(article);
            }
            return View(rssData);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Process(int articleId)
        {
            var context = new rssEntities();
            var articles = context.articles;
            var article = articles.Single(a => a.Id == articleId);
            ViewBag.Url = article.Link;
            return View();
        }

        [HttpGet]
        public PartialViewResult DeleteArticle(int articleId)
        {
            var rssData = new RssFeedViewModel {RssItems = new List<article>()};
            var context = new rssEntities();
            var articles = context.articles;
            var article = articles.Single(a => a.Id == articleId);
            article.Deleted = true;
            context.SaveChanges();
            return DisplayValidArticles();
        }

        [HttpGet]
        public PartialViewResult DisplayValidArticles()
        {
            var rssData = new RssFeedViewModel { RssItems = new List<article>() };
            var context = new rssEntities();
            var articlesToDisplay = context.articles.Where(a => a.Deleted == null || a.Deleted != true);
            foreach (var a in articlesToDisplay)
            {
                rssData.RssItems.Add(a);
            }
            ViewBag.rssType = "validArticles";
            return PartialView("_RSSFeed", rssData);
        }

        [HttpGet]
        public PartialViewResult DisplayDeletedArticles()
        {
            var rssData = new RssFeedViewModel { RssItems = new List<article>() };
            var context = new rssEntities();
            var articlesToDisplay = context.articles.Where(a => a.Deleted == true);
            foreach (var a in articlesToDisplay)
            {
                rssData.RssItems.Add(a);
            }
            ViewBag.rssType = "deletedArticles";
            return PartialView("_RSSFeed", rssData);
        }
    }
}
