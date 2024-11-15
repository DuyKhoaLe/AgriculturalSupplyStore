using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturalSupplyStore.ViewComponents
{
	public class MenuLoaiMayViewComponent : ViewComponent
	{
		private readonly AgriculturalsupplystoreDbContext db;

		public MenuLoaiMayViewComponent(AgriculturalsupplystoreDbContext context) => db = context;

		public IViewComponentResult Invoke()
		{
			var data = db.LoaiMays.Select(lo => new MenuLoaiMayVM
			{
				MaLoaiMay = lo.MaLoaiMay,
				TenLoaiMay = lo.TenLoaiMay,
				TenLoaiAlias = lo.TenLoaiAlias,
				Hinh = lo.Hinh,
				MoTa = lo.MoTa
			});
			return View(data);
		}
	}
}
