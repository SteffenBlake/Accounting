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

            ViewBag.datasource = People.AsEnumerable();
            ViewBag.Title = "People";
            return View();
            }
        }

        public ActionResult New()
        {
            ViewBag.datasource = new VMPerson();
            ViewBag.Title = "New Person";
            return View("Edit");
        }

        public ActionResult Edit(int id)
        {
            using (ISession session = Hook.OpenSession())
            {
                var vm = session.Get<VMPerson>(id);
                ViewBag.datasource = vm;
                ViewBag.Title = $"Edit: {vm.FullName}";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(VMPerson vm)
        {
            using (ISession session = Hook.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(vm);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return View();
                    }
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (ISession session = Hook.OpenSession())
                {
                    var vm = session.Get<VMPerson>(id);
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(vm);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
    }
}