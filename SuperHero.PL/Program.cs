using FRYMA_SuperHero.BL.Interface;
using FRYMA_SuperHero.BL.Reposoratory;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SuperHero.BL.Interface;
using SuperHero.BL.Mapper;
using SuperHero.BL.Reposoratory;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Database;
using SuperHero.DAL.Entities;
using SuperHero.PL.Hubs;
using SuperHero.PL.Languages;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt => {
    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(SharedResource));
});


// Enhancement ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(connectionString));


//Auto Mapper
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

//AddScope 
builder.Services.AddScoped(typeof(IBaseRepsoratory<>), typeof(BaseRepsoratory<>));
builder.Services.AddScoped(typeof(IServiesRep), typeof(ServiesRep));
// Identity Configuration

//SignalR
//builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    { 
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Error/Error");
    });


builder.Services.AddIdentityCore<Person>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddTokenProvider<DataProtectorTokenProvider<Person>>(TokenOptions.DefaultProvider);

builder.Services.AddSession(opt =>
opt.IdleTimeout = TimeSpan.FromMinutes(100)
);


// Password and user name configuration

builder.Services.AddIdentity<Person, IdentityRole>(options =>
{
    // Default Password settings.
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<ApplicationContext>();


var app = builder.Build();

var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});

app.UseSession();
//AddSignalR

app.MapHub<ChatHub>("/chatHub");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.Run();
