using Nop.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Plugin.LDTracker.Data;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.LDTracker.Domain;
using Nop.Core.Data;
using Autofac.Core;
using Nop.Data;
using Nop.Plugin.LDTracker.Services;

namespace Nop.Plugin.LDTracker.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_product_view_tracker";

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<LotteryCategoryService>().As<ILotteryCategoryService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<LDTrackerObjectContext>(builder, CONTEXT_NAME);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<LotteryCategory>>()
                .As<IRepository<LotteryCategory>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<Lottery>>()
                .As<IRepository<Lottery>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<LotteryFull>>()
                .As<IRepository<LotteryFull>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<LotteryCategoryService>().As<ILotteryCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<LotteryCustomerService>().As<ILotteryCustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<LotteryFullService>().As<ILotteryFullService>().InstancePerLifetimeScope();
            builder.RegisterType<LotteryService>().As<ILotteryService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<LDTrackerObjectContext>(builder, CONTEXT_NAME);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<LotteryCategory>>()
                .As<IRepository<LotteryCategory>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<LotteryCustomer>>()
                .As<IRepository<LotteryCustomer>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<Lottery>>()
                .As<IRepository<Lottery>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<LotteryFull>>()
                .As<IRepository<LotteryFull>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
