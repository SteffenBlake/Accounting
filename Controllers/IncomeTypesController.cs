using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class IncomeTypesController : Controller
    {
        public ActionResult Index()
        {
            var Types = new List<VMIncomeType>();

            using (ISession session = Hook.OpenSession())
            {
                Types.AddRange(session.QueryOver<VMIncomeType>().List());

                ViewBag.datasource = Types.AsEnumerable();
                ViewBag.Title = VMIncomeType.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            ViewBag.Title = $"New {VMIncomeType.SingleName}";
            return View("Edit", new VMIncomeType());
        }

        public ActionResult Edit(int id)
        {
            VMIncomeType vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMIncomeType>(id);
                ViewBag.Title = $"Edit {VMIncomeType.SingleName}: {vm.Name}";
            }
            return View("Edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(VMIncomeType vm)
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
                        ViewBag.Error = e.Message;
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
                    var vm = session.Get<VMIncomeType>(id);
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
                ViewBag.Error = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}