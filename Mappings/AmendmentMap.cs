using FluentNHibernate.Mapping;
using Accounting.Models;

namespace Accounting.Mappings
{
    public class AmendmentMap : ClassMap<VMAmendment>
    {
        public AmendmentMap()
        {
            Table("dbo.Amendment");
            Id(m => m.Id);
            Map(m => m.Date);
            Map(m => m.NewAmount);
            Map(m => m.IsPosted);
            References(m => m.Account).Not.LazyLoad();
        }
    }
}