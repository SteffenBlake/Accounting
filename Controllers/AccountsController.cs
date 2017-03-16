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
            }
            return View("Index");
        }

        public ActionResult Index()
        {
            var Accounts = new List<VMAccount>();

            using (ISession session = Hook.OpenSession())
            {
                Accounts.AddRange(session.QueryOver<VMAccount>().List());

                ViewBag.datasource = Accounts.AsEnumerable();
                ViewBag.Title = VMAccount.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            using (ISession session = Hook.OpenSession())
            {
                ViewBag.Title = $"New {VMAccount.SingleName}";

                ViewBag.AccountTypes = session.QueryOver<VMAccountType>().List();
                ViewBag.People = session.QueryOver<VMPerson>().List();
                return View("Edit", new VMAccount());
            }
        }

        public ActionResult Edit(int id)
        {
            VMAccount vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMAccount>(id);
                vm.OwnerId = vm.Person.Id;
                vm.TypeId = vm.AccountType.Id;

                ViewBag.AccountTypes = session.QueryOver<VMAccountType>().List();
                ViewBag.People = session.QueryOver<VMPerson>().List();

                ViewBag.Title = $"Edit {VMAccount.SingleName}: {vm.Name}";
                return View("Edit", vm);
            }
        }

        [HttpPost]
        public ActionResult Edit(VMAccount vm)
        {
            if (vm.TypeId == null)
            {
                ViewBag.Error = "An Account Type must be selected.";
                return View("Edit", vm);
            }

            if (vm.OwnerId == null)
            {
                ViewBag.Error = "An Account Owner must be selected.";
                return View("Edit", vm);
            }
            using (ISession session = Hook.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        var AccountType = session.Get<VMAccountType>(vm.TypeId);
                        if (AccountType == null)
                        {
                            ViewBag.Error = $"No such Account Type with Id {vm.TypeId} was found.";
                            return View("Edit", vm);
                        }

                        var Person = session.Get<VMPerson>(vm.OwnerId);
                        if (AccountType == null)
                        {
                            ViewBag.Error = $"No such Person with Id {vm.OwnerId} was found.";
                            return View("Edit", vm);
                        }

                        vm.SetType(AccountType);
                        vm.Person = Person;
                        vm.CreationDate = DateTime.Today;

                        session.SaveOrUpdate(vm);
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                        ViewBag.Error = e.Message;
                        return View("Edit", vm);
                    }
                }
            }
            return RedirectToAction("Index");
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