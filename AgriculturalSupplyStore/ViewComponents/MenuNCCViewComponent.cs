using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturalSupplyStore.ViewComponents
{
	public class MenuNCCViewComponent : ViewComponent
	{
		private readonly AgriculturalsupplystoreDbContext db;

		public MenuNCCViewComponent(AgriculturalsupplystoreDbContext context) => db = context;

		public IViewComponentResult Invoke()
		{
			var data = db.NhaCungCaps.Select(lo => new MenuNCCVM
			{
				MaNCC = lo.MaNcc,
				TenCongTy = lo.TenCongTy,
				Logo = lo.Logo,
				MoTa = lo.MoTa
			});

			return View(data);
		}
	}
}
