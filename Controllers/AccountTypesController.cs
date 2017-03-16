using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class AccountTypesController : Controller
    {
        public ActionResult Index()
        {
            var Types = new List<VMAccountType>();

            using (ISession session = Hook.OpenSession())
            {
                Types.AddRange(session.QueryOver<VMAccountType>().List());

                ViewBag.datasource = Types.AsEnumerable();
                ViewBag.Title = VMAccountType.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            ViewBag.Title = $"New {VMAccountType.SingleName}";
            return View("Edit", new VMAccountType());
        }

        public ActionResult Edit(int id)
        {
            VMAccountType vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMAccountType>(id);
                ViewBag.Title = $"Edit {VMAccountType.SingleName}: {vm.Name}";
            }
            return View("Edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(VMAccountType vm)
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
                    var vm = session.Get<VMAccountType>(id);
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