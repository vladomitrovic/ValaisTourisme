using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValaisTourisme.ViewModels;

namespace ValaisTourisme.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index(BookVM bookVM = null)
        {
            Session["filter"] = false;
            bookVM.Hotels = HotelManager.GetHotelsByDateLocationAndNbPerson(bookVM.Checkin, bookVM.Checkout, bookVM.Location, bookVM.NbPerson, 0, 5, null, null);
            Session["bookVM"] = bookVM;
            return View(bookVM);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


}
