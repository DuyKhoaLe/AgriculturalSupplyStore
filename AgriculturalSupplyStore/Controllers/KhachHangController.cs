using AgriculturalSupplyStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturalSupplyStore.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly AgriculturalsupplystoreDbContext db;

        public KhachHangController(AgriculturalsupplystoreDbContext context) 
        {
            db = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
