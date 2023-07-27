using Autofac;
using Library.API.Models;

namespace Library.API
{
    public class ApiModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookModel>().AsSelf();
            base.Load(builder);
        }
    }
}
