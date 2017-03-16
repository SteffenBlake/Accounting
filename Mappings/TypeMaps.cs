using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using Accounting.Models;

namespace Accounting.Mappings
{
    public class AccountTypeMap : ClassMap<VMAccountType>
    {
        public AccountTypeMap()
        {
            Table("dbo.AccountType");
            Id(m => m.Id, "Id");
            Map(m => m.Name);
            HasMany(m => m.Accounts).Inverse().Not.LazyLoad();
        }
    }

    public class PlaceTypeMap : ClassMap<VMPlaceType>
    {
        public PlaceTypeMap()
        {
            Table("dbo.PlaceType");
            Id(m => m.Id, "Id");
            Map(m => m.Name);
            HasMany(m => m.Invoices).Inverse().Not.LazyLoad();
        }
    }

    public class CostTypeMap : ClassMap<VMCostType>
    {
        public CostTypeMap()
        {
            Table("dbo.CostType");
            Id(m => m.Id, "Id");
            Map(m => m.Name);
            HasMany(m => m.Entries).Inverse().Not.LazyLoad();
        }
    }

    public class IncomeTypeMap : ClassMap<VMIncomeType>
    {
        public IncomeTypeMap()
        {
            Table("dbo.IncomeType");
            Id(m => m.Id, "Id");
            Map(m => m.Name);
            HasMany(m => m.Incomes).Inverse().Not.LazyLoad();
        }
    }

    public class TaxTypeMap : ClassMap<VMTaxType>
    {
        public TaxTypeMap()
        {
            Table("dbo.TaxType");
            Id(m => m.Id, "Id");
            Map(m => m.Name);
            Map(m => m.Rate);
            HasMany(m => m.Invoices).Inverse().Not.LazyLoad();
        }
    }
}