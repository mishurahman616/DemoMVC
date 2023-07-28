using Autofac;
using Library.Application.Services;
using Library.Infrastructure.Securities;
using Library.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class InfrastructureModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookService>().As<IBookService>().InstancePerLifetimeScope();
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
