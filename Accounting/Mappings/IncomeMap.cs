using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using Accounting.Models;

namespace Accounting.Mappings
{
    public class IncomeMap : ClassMap<VMIncome>
    {
        public IncomeMap()
        {
            Table("dbo.Income");
            Id(m => m.Id);
            Map(m => m.Date);
            Map(m => m.Total);
            Map(m => m.FederalTax).Nullable();
            Map(m => m.ProvincialTax).Nullable();
            Map(m => m.CPP).Nullable();
            Map(m => m.EI).Nullable();
            Map(m => m.Account);
            Map(m => m.IncomeType);
        }

    }
}