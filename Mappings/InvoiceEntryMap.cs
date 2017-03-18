using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using Accounting.Models;

namespace Accounting.Mappings
{
    public class InvoiceEntryMap : ClassMap<VMInvoiceEntry>
    {
        public InvoiceEntryMap()
        {
            Table("dbo.InvoiceEntry");
            Id(m => m.Id);
            Map(m => m.IsPosted);
            References(m => m.Invoice);
            References(m => m.CostType);
            Map(m => m.Name);
            Map(m => m.Quantity);
            Map(m => m.Amount);
            Map(m => m.Discount).Nullable();
        }

    }
}