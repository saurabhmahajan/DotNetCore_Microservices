using Mango.Common.Dtos;
using Mango.Common.Models;
using Mango.Web.Models.Dtos;

namespace Mango.Web.Services;

public class CouponService : ICouponService
{
    private readonly string _baseUrl;
    private readonly IBaseService _baseService;

    public CouponService(string baseUrl, IBaseService baseService)
    {
        _baseUrl = baseUrl + "/api/coupon";
        _baseService = baseService;
    }
    public async Task<ResponseDto> GetAllAsync()
    {
        var requestDto = new RequestDto
        {
            ApiType = ApiType.GET,
            Url = _baseUrl
        };
        var responseDto = await _baseService.SendAsync(requestDto);
        return responseDto;
    }

    public async Task<ResponseDto> GetByIdAsync(int id)
    {
        var requestDto = new RequestDto
        {
            ApiType = ApiType.GET,
            Url = _baseUrl + $"/{id}"
        };
        var responseDto = await _baseService.SendAsync(requestDto);
        return responseDto;
    }

    public async Task<ResponseDto> GetByCodeAsync(string code)
    {
        var requestDto = new RequestDto
        {
            ApiType = ApiType.GET,
            Url = _baseUrl + $"GetByCodeAsync/{code}"
        };
        var responseDto = await _baseService.SendAsync(requestDto);
        return responseDto;
    }

    public async Task<ResponseDto> CreateCouponAsync(CouponDto coupon)
    {
        var requestDto = new RequestDto
        {
            ApiType = ApiType.POST,
            Url = _baseUrl,
            Data = coupon
        };
        var responseDto = await _baseService.SendAsync(requestDto);
        return responseDto;
    }

    public async Task<ResponseDto> UpdateAsync(CouponDto coupon)
    {
        var requestDto = new RequestDto
        {
            ApiType = ApiType.PUT,
            Url = _baseUrl,
            Data = coupon
        };
        var responseDto = await _baseService.SendAsync(requestDto);
        return responseDto;
    }

    public async Task<ResponseDto> DeleteByIdAsync(int id)
    {
        var requestDto = new RequestDto
        {
            ApiType = ApiType.DELETE,
            Url = _baseUrl + $"/{id}"
        };
        var responseDto = await _baseService.SendAsync(requestDto);
        return responseDto;
    }
}