using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using Accounting.Models;

namespace Accounting.Mappings
{
    public class TransferMap : ClassMap<VMTransfer>
    {
        public TransferMap()
        {
            Table("dbo.Transfer");
            Id(m => m.Id);
            References(m => m.FromAccount);
            References(m => m.ToAccount);
            Map(m => m.Amount);
            Map(m => m.Date);
        }

    }
}