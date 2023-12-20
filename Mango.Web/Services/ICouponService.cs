using Mango.Common.Dtos;
using Mango.Web.Models.Dtos;

namespace Mango.Web.Services
{
    public interface ICouponService
    {
        Task<ResponseDto> GetAllAsync();
        Task<ResponseDto> GetByIdAsync(int id);
        Task<ResponseDto> GetByCodeAsync(string code);
        Task<ResponseDto> CreateCouponAsync(CouponDto coupon);
        Task<ResponseDto> UpdateAsync(CouponDto coupon);
        Task<ResponseDto> DeleteByIdAsync(int id);
    }
}
