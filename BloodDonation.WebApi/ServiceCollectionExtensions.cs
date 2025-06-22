using BloodDonation.Application.Mappers;
using BloodDonation.Infrastructures;
using BloodDonation.WebApi.Middlewares;
using FluentValidation;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scrutor;
using System.Reflection;
using System.Text;

namespace BloodDonation.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
                       .UseSimpleAssemblyNameTypeSerializer()
                       .UseRecommendedSerializerSettings()
                       .UseMemoryStorage()
                       );
        services.AddHangfireServer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Blood Donation Support System", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);
            opt.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Bearer Generated JWT-Token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"

            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = JwtBearerDefaults.AuthenticationScheme
                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header,
                                }, Array.Empty<string>()
                            }
                        });
        });

        services.AddRouting(x => x.LowercaseUrls = true);
        services.AddSingleton<GlobalErrorHandlingMiddleware>();
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("VERYSTRONGPASSWORD_CHANGEMEIFYOUNEED")),
                ValidateIssuer = true,
                ValidIssuer = "BloodDonation.WebApi",
                ValidAudience = "BloodDonation.Client",
                ValidateAudience = true
            };
        });
        services.AddHttpContextAccessor();
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:SqlServer"]));
        //services.AddDbContext<AppDbContext>(opt => opt
        //    .UseLazyLoadingProxies()
        //    .UseSqlServer(configuration.GetConnectionString("SqlServer")));
        // DI Auto Registration
        services.Scan(scan =>
        {
            scan.FromAssemblies(getAssemblies())
                .AddClasses()
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime();
        });
        // AutoMapper
        services.AddAutoMapper(typeof(MapperConfigurationProfile));
        services.AddValidatorsFromAssemblies(getAssemblies());

        return services;
    }
    private static Assembly[] getAssemblies()
        => [AssemblyReference.Assembly, Application.AssemblyReference.Assembly, Infrastructures.AssemblyReference.Assembly];


}