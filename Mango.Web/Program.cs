using Mango.Web.Services;

namespace Mango.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // CreateCouponAsync services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddHttpClient();
            builder.Services.AddHttpClient<BaseService>();
            var couponServiceBaseUrl = builder.Configuration["CouponService:Url"];
            ArgumentException.ThrowIfNullOrEmpty(couponServiceBaseUrl);

            builder.Services.AddScoped<IBaseService, BaseService>();
            builder.Services.AddScoped<ICouponService>(x =>
                new CouponService(couponServiceBaseUrl, x.GetRequiredService<IBaseService>()));

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
