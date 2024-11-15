using Microsoft.AspNetCore.Mvc;
using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.ViewModels;

namespace AgriculturalSupplyStore.Controllers
{
	public class HangHoaController : Controller
	{
		private readonly AgriculturalsupplystoreDbContext db;
		public HangHoaController(AgriculturalsupplystoreDbContext context)
		{
			db = context;
		}
		public IActionResult Index(string MTMay)
		{
			ViewBag.MTMay = MTMay;

			return View();
		}	

		//Kiểu Máy là các sản phẩm khác nhau về phiên bản nhưng giống về chức năng
		public IActionResult KieuMay(int? loaimay)
		{
			var loaiMays = db.KieuMays.AsQueryable();
			if (loaimay.HasValue)
			{
                loaiMays = loaiMays.Where(p => p.MaLoaiMay == loaimay.Value);
			}

			var result = loaiMays.Select(p => new MenuKieuMayVM
			{
				MaKieuMay = p.MaKieuMay,
				TenKieuMay = p.TenKieuMay,
				TenKieuAlias = p.TenKieuAlias,
				Hinh = p.Hinh,
				MoTa = p.MoTa,
				MaLoaiMay = p.MaLoaiMay
			});
			return View(result);
		}


		//Bộ Phận là vị trí của máy
		public IActionResult BoPhan(int? kieumay)
		{
			var boPhans = db.BoPhans.AsQueryable();
			if (kieumay.HasValue)
			{
				boPhans = boPhans.Where(p => p.MaKieuMay == kieumay.Value);
			}

			var result = boPhans.Select(p => new MenuBoPhanVM
			{
				MaBoPhan = p.MaBoPhan,
				TenBoPhan = p.TenBoPhan,				
				TenBoPhanAlias = p.TenBoPhanAlias,
				Hinh = p.Hinh,
				MoTa = p.MoTa,
				MaKieuMay = p.MaKieuMay
			});
			return View(result);
		}

		//Phận là vị trí của Hàng hóa
		public IActionResult Phan(int? bophan)
		{
			var Phans = db.Phans.AsQueryable();
			if (bophan.HasValue)
			{
				Phans = Phans.Where(p => p.MaBoPhan == bophan.Value);
			}

			var result = Phans.Select(p => new MenuPhanVM
			{
				MaPhan = p.MaPhan,
				TenPhan = p.TenPhan,
				TenPhanAlias = p.TenPhanAlias,
				Hinh = p.Hinh,
				MoTa = p.MoTa,
				MaBoPhan = p.MaBoPhan
			});
			return View(result);
		}

		//Tìm Kiếm Hàng hóa
		public IActionResult Search(string? query)
		{
			var hangHoas = db.HangHoas.AsQueryable();
			if (query != null)
			{
				hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
			}

			var result = hangHoas.Select(p => new MenuHangHoaVM
			{
				MaHh = p.MaHh,
				TenHh = p.TenHh,
				TenAlias = p.TenAlias,
				MaPhan = p.MaPhan,
				MoTaDonVi = p.MoTaDonVi,
				DonGia = p.DonGia,
				Hinh = p.Hinh,
				NgaySx = p.NgaySx,
				GiamGia = p.GiamGia,
				SoLanXem = p.SoLanXem,
				MoTa = p.MoTa,
				MaNcc = p.MaNcc
			});
			return View(result);
		}
    }
}
