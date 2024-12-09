using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.Helpers;
using AgriculturalSupplyStore.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturalSupplyStore.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly AgriculturalsupplystoreDbContext db;
        private readonly IMapper _mapper;

        public KhachHangController(AgriculturalsupplystoreDbContext context, IMapper mapper) 
        {
            db = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid) 
            {
                var khachhang = _mapper.Map<KhachHang>(model);
                khachhang.RandomKey = MyUtil.GenerateRandomKey();
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
