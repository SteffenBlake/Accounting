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
            HasMany(m => m.Entries).Inverse().Not.LazyLoad();
            References(m => m.PlaceType);
            References(m => m.TaxType);
            Map(m => m.Date);
            References(m => m.Account);
        }

    }
}