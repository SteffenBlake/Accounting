using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class AccountsController : Controller
    {
        public ActionResult ByPerson(int personId)
        {
            var Accounts = new List<VMAccount>();

            using (ISession session = Hook.OpenSession())
            {
                var person = session.Get<VMPerson>(personId);

                Accounts.AddRange(session.QueryOver<VMAccount>().Where(a => a.Person.Id == personId).List());

                ViewBag.datasource = Accounts.AsEnumerable();
                ViewBag.Title = $"{VMAccount.PluralName} - {person.FirstName}";
                return View("Index");
            }
        }

        public ActionResult Index()
        {
            var Types = new List<VMAccount>();

            using (ISession session = Hook.OpenSession())
            {
                Types.AddRange(session.QueryOver<VMAccount>().List());

                ViewBag.datasource = Types.AsEnumerable();
                ViewBag.Title = VMAccount.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            ViewBag.Title = $"New {VMAccount.SingleName}";
            return View("Edit", new VMAccount());
        }

        public ActionResult Edit(int id)
        {
            VMAccount vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMAccount>(id);
                ViewBag.Title = $"Edit {VMAccount.SingleName}: {vm.Name}";
            }
            return View("Edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(VMAccount vm)
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
                    var vm = session.Get<VMAccount>(id);
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