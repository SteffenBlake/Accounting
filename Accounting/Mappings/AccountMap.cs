using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using Accounting.Models;

namespace Accounting.Mappings
{
    public class AccountMap : ClassMap<VMAccount>
    {
        public AccountMap()
        {
            Table("dbo.Account");
            Id(m => m.Id);
            Map(m => m.Name);
            Map(m => m.Person);
            Map(m => m.AccountType);
            HasMany(m => m.Incomes).Inverse();
            HasMany(m => m.Invoices).Inverse();
            HasMany(m => m.InTransfers).Inverse();
            HasMany(m => m.OutTransfers).Inverse();
        }

    }
}