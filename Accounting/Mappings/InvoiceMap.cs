using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using Accounting.Models;

namespace Accounting.Mappings
{
    public class InvoiceMap : ClassMap<VMInvoice>
    {
        public InvoiceMap()
        {
            Table("dbo.Invoice");
            Id(m => m.Id);
            HasMany(m => m.Entries).Inverse();
            Map(m => m.PlaceType);
            Map(m => m.TaxType);
            Map(m => m.Date);
            Map(m => m.Account);
        }

    }
}