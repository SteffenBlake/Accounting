using NHibernate;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Accounting.Mappings
{
    public static class Hook
    {

        public static ISession OpenSession()
        {
            var factory = Fluently.Configure().Database(
                MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\Projects\ASP.NET\Accounting\Accounting\App_Data\Database.mdf;Integrated Security=True")
                .ShowSql()
                )

                .Mappings(map =>
                {
                    map.FluentMappings.AddFromAssemblyOf<AccountTypeMap>();
                    map.FluentMappings.AddFromAssemblyOf<PlaceTypeMap>();
                    map.FluentMappings.AddFromAssemblyOf<CostTypeMap>();
                    map.FluentMappings.AddFromAssemblyOf<IncomeTypeMap>();
                    map.FluentMappings.AddFromAssemblyOf<TaxTypeMap>();
                    map.FluentMappings.AddFromAssemblyOf<AccountMap>();
                    map.FluentMappings.AddFromAssemblyOf<IncomeMap>();
                    map.FluentMappings.AddFromAssemblyOf<InvoiceMap>();
                    map.FluentMappings.AddFromAssemblyOf<InvoiceEntryMap>();
                    map.FluentMappings.AddFromAssemblyOf<PersonMap>();
                    map.FluentMappings.AddFromAssemblyOf<TransferMap>();
                    map.FluentMappings.AddFromAssemblyOf<AmendmentMap>();
                })

                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                .BuildSessionFactory();

            return factory.OpenSession();
        }
    }
}