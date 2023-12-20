using Mango.Web.Models.Dtos;
using Mango.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> Index()
        {
            List<CouponDto?> list = null;

            var response = await _couponService.GetAllAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await _couponService.CreateCouponAsync(couponDto);
                if (responseDto.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["error"] = responseDto.Message;
            }
            return View(couponDto);
        }

        public async Task<IActionResult> Delete(int couponId)
        {
            var responseDto = await _couponService.GetByIdAsync(couponId);

            if (responseDto is { IsSuccess: true })
            {
                var couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
                return View(couponDto);
            }

            TempData["error"] = "Coupon not found.";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CouponDto couponDto)
        {
            var responseDto = await _couponService.DeleteByIdAsync(couponDto.CouponId);
            if (responseDto is { IsSuccess: true })
            {
                TempData["success"] = "Coupon deleted successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = responseDto.Message;
            return View(couponDto);
        }
    }
}
