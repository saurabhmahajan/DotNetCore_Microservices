using AutoMapper;
using Mango.Common.Dtos;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(_db.Coupons.ToList());
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet()]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                _responseDto.Result = _mapper.Map<CouponDto>(_db.Coupons.First(c => c.CouponId == id));
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                var coupon = _db.Coupons.FirstOrDefault(c => c.CouponCode.ToLower().Equals(code.ToLower()));
                if (coupon == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "No record found.";
                    return _responseDto;
                }
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception e)
            {
                _responseDto.Message = "Something went wrong please try again later.";
            }
            return _responseDto;
        }

        [HttpPost]
        public ResponseDto Post([FromBody]CouponDto couponDto)
        {
            try
            {
                var couponToAdd = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(couponToAdd);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(couponToAdd);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            
            return _responseDto;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var coupon = _db.Coupons.FirstOrDefault(c => c.CouponId == id);
                if (coupon == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = $"Coupon with id {id} not found.";
                    return _responseDto;
                }

                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
