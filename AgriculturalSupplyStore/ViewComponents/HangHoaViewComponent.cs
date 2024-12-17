using Microsoft.AspNetCore.Mvc;
using AgriculturalSupplyStore.Data; 
using AgriculturalSupplyStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AgriculturalSupplyStore.ViewComponents
{
	[ViewComponent]
	public class HangHoaViewComponent : ViewComponent
	{
		private readonly AgriculturalsupplystoreDbContext _dbContext;

		public HangHoaViewComponent(AgriculturalsupplystoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IViewComponentResult> InvokeAsync(string maphan)
		{
			var hangHoas = await _dbContext.HangHoas
				.Where(p => p.MaPhan == maphan) 
				.Select(p => new MenuHangHoaVM
				{
					MaHh = p.MaHh,
					TenHh = p.TenHh,
					TenAlias = p.TenAlias,
					MaPhan = p.MaPhan,
					MoTaNgan = p.MoTaDonVi,
					DonGia = p.DonGia,
					Hinh = p.Hinh,
					NgaySx = p.NgaySx,
					GiamGia = p.GiamGia,
					SoLanXem = p.SoLanXem,
					ChiTiet = p.MoTa,
					MaNcc = p.MaNcc
				})
				.ToListAsync(); 
			return View(hangHoas);
		}
	}
}
