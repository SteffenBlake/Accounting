using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Accounting.Models;
using Accounting;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class PeopleController : Controller
    {
        public ActionResult Index()
        {
            var People = new List<VMPerson>();

            using (ISession session = Hook.OpenSession())
            {
                People.AddRange(session.QueryOver<VMPerson>().List());
            }

            ViewBag.datasource = People;
            return View();
        }
    }
}