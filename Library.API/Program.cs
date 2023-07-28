using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library.API;
using Library.Application;
using Library.Infrastructure;
using Library.Persistence.Extensions;
using Library.Infrastructure.Securities.Authorization;
using Library.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;

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
    builder.Services.AddIdentity();

    builder.Services.AddAuthentication()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
            };
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("CourseViewRequirementPolicy", policy =>
        {
            policy.AuthenticationSchemes.Clear();
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireAuthenticatedUser();
            policy.Requirements.Add(new BookCreateRequirement());
        });
    });

    builder.Services.AddSingleton<IAuthorizationHandler, BookCreateRequirementHandler>();
    

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

