using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiGeekNews.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreationPage()
        {
            return View(); 
        }
        public ActionResult ListingPage()
        { 
            return View();
        }
        public ActionResult Settings()
        {
            return View();
        }
    }
}