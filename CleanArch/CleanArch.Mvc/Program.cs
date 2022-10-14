using CleanArch.Application.Validations;
using CleanArch.Application.ViewModels;
using CleanArch.Infrastructure.Data.Context;
using CleanArch.Infrastructure.Identity.Context;
using CleanArch.Infrastructure.IoC;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var identityConnectionString = builder.Configuration.GetConnectionString("UniversityIdentityDBConnection");
var universityConnectionString = builder.Configuration.GetConnectionString("UniversityDBConnection");
builder.Services.AddDbContext<SecurityDbContext>(options =>
    options.UseSqlServer(identityConnectionString));

builder.Services.AddDbContext<UniversityDBContext>(options =>
    options.UseSqlServer(universityConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SecurityDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddFluentValidationAutoValidation( config =>
{
    //Disable other validator providers from executing
    config.DisableDataAnnotationsValidation = true;
});

builder.Services.AddScoped<IValidator<CourseViewModel>, CourseViewModelValidator>();

builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
