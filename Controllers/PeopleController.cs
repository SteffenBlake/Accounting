using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class PeopleController : Controller
    {
        public ActionResult Index()
        {
            var vmList = new List<VMPerson>();

            using (ISession session = Hook.OpenSession())
            {
                vmList.AddRange(session.QueryOver<VMPerson>().List());

            ViewBag.datasource = vmList.AsEnumerable();
            ViewBag.Title = VMPerson.PluralName;
            return View();
            }
        }

        public ActionResult New()
        {
            ViewBag.Title = $"New {VMPerson.SingleName}";
            return View("Edit", new VMPerson());
        }

        public ActionResult Edit(int id)
        {
            VMPerson vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMPerson>(id);
                ViewBag.Title = $"Edit {VMPerson.SingleName}: {vm.FirstName}";
            }
                return View("Edit", vm);
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