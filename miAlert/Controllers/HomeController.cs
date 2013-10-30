using miAlert.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace miAlert.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RssFeedViewModel testData = new RssFeedViewModel();
            testData.RssItems = new List<RssItemViewModel>();
            testData.RssItems.Add(new RssItemViewModel() { Url = "http://www.wthr.com/story/23499073/2013/09/22/man-dies-in-hendricks-county-tractor-rollover", Title = "Man dies when tractor rolls over in Hendricks County" });
            testData.RssItems.Add(new RssItemViewModel() { Url = "http://kitchener.ctvnews.ca/drayton-area-man-dies-in-fatal-tractor-crash-1.1476488", Title = "Drayton-area man dies in fatal tractor crash", DeathInvolved = true });
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
