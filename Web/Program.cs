using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Globalization;
using System.Reflection;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ChangeLanguage>();
// Add services to the container.
builder.Services.AddMvc()
.AddViewLocalization().AddDataAnnotationsLocalization(option =>
{
    option.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
        return factory.Create("SharedResource", assemblyName.Name);
    };

});

builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(
       options => {
           var supportedCultures = new List<CultureInfo>

           {
                        new CultureInfo("az"),
                        new CultureInfo("en-US"),
                        new CultureInfo("ru-RU")
           };

           options.DefaultRequestCulture = new RequestCulture(culture: "az", uiCulture: "az");

           options.SupportedCultures = supportedCultures;
           options.SupportedUICultures = supportedCultures;
           options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
       });

builder.Services.AddDbContext<AgencyContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AgencyContext>();

builder.Services.AddScoped<ProductManager>();

builder.Services.AddScoped<LanguageService>();
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
app.UseRequestLocalization();
app.UseAuthorization();

app.UseRouting();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
