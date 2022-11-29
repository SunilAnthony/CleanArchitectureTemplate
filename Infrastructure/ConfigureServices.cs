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
    
        //services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddTransient<IStudentRepository, StudentRepository>();
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
