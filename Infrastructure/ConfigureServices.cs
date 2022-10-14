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

        //services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        //services.AddScoped<SecurityDbContextInitializer>();

        services.AddAutoMapper(c => c.AddProfile<StudentMappingProfile>(), typeof(Application.AssemblyReference));

        services.AddMediatR(typeof(Application.AssemblyReference).Assembly);

        // Microsoft.AspNetCore.Mvc.NewtonsoftJson package is required
        services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None);


        return services;
    }
}
