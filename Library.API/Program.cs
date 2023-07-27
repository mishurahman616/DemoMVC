using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library.API;
using Library.Application;
using Library.Infrastructure;
using Library.Persistence;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Serilog Configuration
builder.Host.UseSerilog((webbuilderHostContext, loggerConfiguration) =>
{
    loggerConfiguration.MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration);
});

try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection String not found");
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName ?? throw new Exception("Assembly Name not found");
    //Autofac Configuration
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new PersistenceModule(connectionString, migrationAssembly));
        containerBuilder.RegisterModule(new InfrastructureModule());
        containerBuilder.RegisterModule(new ApiModule());
    });
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex.Message);
}

