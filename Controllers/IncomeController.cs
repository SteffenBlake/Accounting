using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class IncomeController : Controller
    {
        public ActionResult Index()
        {
            var Types = new List<VMIncome>();

            using (ISession session = Hook.OpenSession())
            {
                Types.AddRange(session.QueryOver<VMIncome>().List());

                ViewBag.datasource = Types.AsEnumerable();
                ViewBag.Title = VMIncome.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            ViewBag.Title = $"New {VMIncome.SingleName}";
            return View("Edit", new VMIncome());
        }

        public ActionResult Edit(int id)
        {
            VMIncome vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMIncome>(id);
                ViewBag.Title = $"Edit {VMIncome.SingleName}: {vm.Account.Name}-{vm.Date.ToString()}";
            }
            return View("Edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(VMIncome vm)
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
                    var vm = session.Get<VMIncome>(id);
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