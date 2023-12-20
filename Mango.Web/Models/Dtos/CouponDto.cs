using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models.Dtos
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinAmount { get; set; }
    }
}
