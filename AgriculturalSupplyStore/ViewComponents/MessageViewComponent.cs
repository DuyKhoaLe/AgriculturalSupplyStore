using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.Helpers;
using AgriculturalSupplyStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriculturalSupplyStore.ViewComponents
{
    public class MessageViewComponent : ViewComponent
    {
        private readonly AgriculturalsupplystoreDbContext db;

        public MessageViewComponent(AgriculturalsupplystoreDbContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Gopies.Select(lo => new MessageModel
            {
                MaGy = lo.MaGy,
                HoTen = lo.HoTen,
                NoiDung = lo.NoiDung,
                NgayGy = lo.NgayGy,
            });
            return View(data);
        }
    }
}
