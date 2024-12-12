using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AgriculturalSupplyStore.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace AgriculturalSupplyStore.Controllers
{
    public class GioHangController : Controller
    {
        private readonly AgriculturalsupplystoreDbContext db;

        public GioHangController(AgriculturalsupplystoreDbContext context)
        {
            db = context;
        }
     
        public List<GioHangHHVM> Cart => HttpContext.Session.Get<List<GioHangHHVM>>(MySetting.CART_KEY) ?? new List<GioHangHHVM>();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                item = new GioHangHHVM
                {
                    MaHh = hangHoa.MaHh,
                    TenHh = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = hangHoa.Hinh ?? string.Empty,
                    SoLuong = quantity

                };
                giohang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }
            HttpContext.Session.Set(MySetting.CART_KEY, giohang);    
            return RedirectToAction("Index");
        }
        public IActionResult RemoveCart(int id)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                giohang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, giohang);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Checkout()
        {            
            if (Cart.Count == 0)
            {
                return Redirect("/");
            }
            return View(Cart);
        }
    }
}
