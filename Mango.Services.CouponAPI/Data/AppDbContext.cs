using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new List<Coupon>
            {
                new Coupon { CouponId = 1, CouponCode = "Get20", DiscountAmount = 20, MinAmount = 100 },
                new Coupon { CouponId = 2, CouponCode = "Get30", DiscountAmount = 30, MinAmount = 200 },
            });
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
