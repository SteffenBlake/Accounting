using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Accounting.Controllers
{
    public class IncomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Types()
        {
            return View();
        }
    }
}