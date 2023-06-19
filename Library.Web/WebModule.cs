using Autofac;
using Library.Web.Areas.Admin.Models;
using Library.Web.Models;

namespace Library.Web
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //book Model
            builder.RegisterType<BookCreateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BookEditModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BookListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BookDeleteModel>().AsSelf().InstancePerLifetimeScope();

            //user Model
            builder.RegisterType<RegisterModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoginModel>().AsSelf().InstancePerLifetimeScope();

            
            base.Load(builder);
        }
    }
}
