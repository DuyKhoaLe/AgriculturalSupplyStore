using AgriculturalSupplyStore.Helpers;
using AgriculturalSupplyStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturalSupplyStore.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<GioHangHHVM>>(MySetting.CART_KEY) ?? new List<GioHangHHVM>();
            return View("CartPanel", new CartModel
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p => p.ThanhTien)
            });
        }
    }
}
