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
            SchemaAction.None();
            Table("dbo.Account");
            Id(m => m.Id);
            Map(m => m.Name);
            Map(m => m.OpeningBalance);
            Map(m => m.CreationDate);
            References(m => m.Person);
            References(m => m.AccountType);
            HasMany(m => m.Incomes).Inverse().Not.LazyLoad();
            HasMany(m => m.Invoices).Inverse().Not.LazyLoad();
            HasMany(m => m.InTransfers).Inverse().Not.LazyLoad();
            HasMany(m => m.OutTransfers).Inverse().Not.LazyLoad();
        }

    }
}