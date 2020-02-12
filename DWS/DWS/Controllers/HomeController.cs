using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DWS.Models;
using DWS.Providers;

namespace DWS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new CriteriaViewModel());
        }

        [HttpPost]
        public ActionResult Index(CriteriaViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var proc = CriteriaProvider.Process(model.Criteria);

            if (proc.Status == 0)
            {
                model.Image = proc.Image;
            }
            else
            {
                ModelState.AddModelError("Criteria", proc.Message);
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}