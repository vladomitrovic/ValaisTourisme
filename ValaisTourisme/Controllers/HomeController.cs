using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValaisTourisme.ViewModels;

namespace ValaisTourisme.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BookVM vm = new BookVM
            {
                Locations = {"Brig", "Sion", "Martigny"}
            };
         

            Session["bookVM"] = vm;

            return View(vm);
        }

        public ActionResult Search(FormCollection fc)
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}