using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library.Application;
using Library.Infrastructure;
using Library.Persistence;
using Library.Persistence.Extensions;
using Library.Web;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//Serilog

builder.Host.UseSerilog((webHostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration.MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration);
});

try
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found");
    string migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    //Autofac
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
      //  containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new PersistenceModule(connectionString, migrationAssembly));
        containerBuilder.RegisterModule(new InfrastructureModule());
        containerBuilder.RegisterModule(new WebModule());
    });

    // Add services to the container.
    //builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //{
    //    options.UseSqlServer(connectionString, serverOptions => serverOptions.MigrationsAssembly(migrationAssembly));
    //});


    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddIdentity();

    builder.Services.AddControllersWithViews();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name:"areas",
        pattern:"{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    Log.Information("Application Started");
    app.Run();

}
catch(Exception ex)
{
    Log.Fatal(ex.Message);
}

