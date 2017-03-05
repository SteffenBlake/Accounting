using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using Accounting.Models;

namespace Accounting.Mappings
{
    public class PersonMap : ClassMap<VMPerson>
    {
        public PersonMap()
        {
            Table("dbo.Person");
            Id(m => m.Id);
            HasMany(m => m.Accounts).Inverse().Not.LazyLoad();
            Map(m => m.FirstName).Nullable();
            Map(m => m.MiddleName).Nullable();
            Map(m => m.LastName).Nullable();
            Map(m => m.Color);
        }

    }
}