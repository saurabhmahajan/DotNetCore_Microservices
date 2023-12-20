namespace Mango.Services.CouponAPI.Models.Dtos
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinAmount { get; set; }
    }
}
