using G_APIs.BussinesLogic;
using G_APIs.BussinesLogic.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddLogging();

        builder.Services.AddScoped<IAccount, Account>();
        builder.Services.AddScoped<IFund, Fund>();
        builder.Services.AddScoped<IReports, Reports>();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });


        //builder.Services.AddDbContext<GAccountingDbContext>(options =>
        //      options.UseNpgsql(builder.Configuration.GetConnectionString("GAccountingDbContext"),
        //options => options.UseNodaTime()));

        //builder.Services.AddSession(options =>
        //{
        //    options.IdleTimeout = TimeSpan.FromSeconds(10);
        //    options.Cookie.HttpOnly = true;
        //    options.Cookie.IsEssential = true;
        //});

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        //app.UseEndpoints(endpoints =>
        //{
        //    endpoints.MapControllerRoute(
        //        name: "default",
        //        pattern: "{controller=Home}/{action=Wallet}/{id?}");
        //});

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}