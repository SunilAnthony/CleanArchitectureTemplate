using Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Common.Mappings;
using Newtonsoft.Json;
using Infrastructure.Pipes;
using FluentValidation;
using Application.Common.Behaviors;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Common.Interfaces.Services;
using Infrastructure.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CleanArchitectureDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options => {
                //options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                options.EnableSensitiveDataLogging();
                    
                });
        }

        services.AddHttpContextAccessor();

    
        //Add generic pipe which run top to bottom: order is important
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UserPipe<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));


        services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly,
            includeInternalTypes: true);
    
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IJWTService, JWTService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<SecurityDbContextInitializer>();

        services.AddAutoMapper(c => c.AddProfile<StudentMappingProfile>(), typeof(Application.AssemblyReference));

        services.AddMediatR(typeof(Application.AssemblyReference).Assembly);

        // Microsoft.AspNetCore.Mvc.NewtonsoftJson package is required
        services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None);

        

        return services;


    }
    public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Inject Asp.Net Identity
        services.AddDbContext<SecurityDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("UserManagementDB")));


        // Simplify the password requirements. Not recommended for production
        services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
        {
            config.Password.RequireDigit = false;
            config.Password.RequiredLength = 4;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequireUppercase = false;

        }).AddEntityFrameworkStores<SecurityDbContext>()
        .AddDefaultTokenProviders();

        // Enable Dual Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
            .AddJwtBearer(cfg =>  //Validate the Token - Add by Sunil Anthony
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    ValidateIssuer = true,  // Validate the JWT Issuer (iss) claim
                    ValidateAudience = true,  // Validate the JWT Audience (aud) claim
                    ValidateLifetime = true,  // Validate the token expiry
                    LifetimeValidator = CustomLifetimeValidator,
                    ValidateIssuerSigningKey = true,  // The signing key must match!
                    ClockSkew = TimeSpan.Zero, //If you want to allow a certain amount of clock drift, set that here:
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecretKey"]))
                };
                // This doesn't seems to be working, will have to look at it later
                cfg.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };

            });
        services.AddAuthorization(options =>
        {
            //Creating a policy with a role
            options.AddPolicy("Admin", policy => policy.RequireRole("Administrator"));

        });


        return services;
    }
    private static bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
    {
        if (expires != null)
        {
            return expires > DateTime.UtcNow;
        }
        return false;
    }
}
