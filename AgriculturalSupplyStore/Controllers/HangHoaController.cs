using Microsoft.AspNetCore.Mvc;
using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        //AdminLoaiMay là các sản phẩm loại máy 1
        public IActionResult AdminLoaiMay()
        {
            var data = db.LoaiMays.Select(p => new MenuLoaiMayVM
            {
                MaLoaiMay = p.MaLoaiMay,
                TenLoaiMay = p.TenLoaiMay,
                TenLoaiAlias = p.TenLoaiAlias,
                Hinh = p.Hinh,
                MoTa = p.MoTa
            });

            return View(data);
        }
        //AdminDeleteLoaiMay là xóa sản phẩm loại máy 1 
        [HttpPost, ActionName("AdminDeleteLoaiMay")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteLoaiMay(int malm)
        {
            var product = await db.LoaiMays.FindAsync(malm);
            if (product != null)
            {
                db.LoaiMays.Remove(product);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminLoaiMay));
        }

        //Kiểu Máy là các sản phẩm khác nhau về phiên bản nhưng giống về chức năng. Tìm kiếm theo mã 2
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

        //AdminKieuMay là các sản phẩm loại máy 2
        public IActionResult AdminKieuMay()
        {
            var data = db.KieuMays.Select(p => new MenuKieuMayVM
            {
                MaKieuMay = p.MaKieuMay,                
                TenKieuMay = p.TenKieuMay,
                TenKieuAlias = p.TenKieuAlias,
                MaLoaiMay = p.MaLoaiMay,
                Hinh = p.Hinh,
                MoTa = p.MoTa
            });

            return View(data);
        }

        //AdminDeleteKieuMay là xóa sản phẩm kiểu máy 2 
        [HttpPost, ActionName("AdminDeleteKieuMay")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteKieuMay(int makm)
        {
            var product = await db.KieuMays.FindAsync(makm);
            if (product != null)
            {
                db.KieuMays.Remove(product);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminKieuMay));
        }


        //Bộ Phận là vị trí của máy. tìm bộ phận theo mã 3
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

        //AdminBoPhan là các bộ phần của máy 3
        public IActionResult AdminBoPhan()
        {
            var data = db.BoPhans.Select(p => new MenuBoPhanVM
            {
                MaBoPhan = p.MaBoPhan,
                TenBoPhan = p.TenBoPhan,
                TenBoPhanAlias = p.TenBoPhanAlias,
                Hinh = p.Hinh,
                MoTa = p.MoTa,
                MaKieuMay = p.MaKieuMay
            });

            return View(data);
        }

        //AdminDeleteBoPhan là xóa sản Bộ Phận 3
        [HttpPost, ActionName("AdminDeleteBoPhan")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteBoPhan(int mabp)
        {
            var data = await db.BoPhans.FindAsync(mabp);
            if (data != null)
            {
                db.BoPhans.Remove(data);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminBoPhan));
        }

        //Phận là vị trí của Hàng hóa. Theo mã  4
        public async Task<IActionResult> Phan(int? bophan)
        {
            var Phans = db.Phans.AsQueryable();
            if (bophan.HasValue)
            {
                Phans = Phans.Where(p => p.MaBoPhan == bophan.Value);
            }

            var result = await Phans.Select(p => new MenuPhanVM
            {
                MaPhan = p.MaPhan,
                TenPhan = p.TenPhan,
                TenPhanAlias = p.TenPhanAlias,
                Hinh = p.Hinh,
                MoTa = p.MoTa,
                MaBoPhan = p.MaBoPhan
            }).ToListAsync();

            return View(result);
        }
        //AdminBoPhan là hiển thị phần của sản phẩm 4
        public IActionResult AdminPhan()
        {
            var data = db.Phans.Select(p => new MenuPhanVM
            {
                MaPhan = p.MaBoPhan,
                TenPhan = p.TenPhan,
                TenPhanAlias = p.TenPhanAlias,
                Hinh = p.Hinh,
                MoTa = p.MoTa,
                MaBoPhan = p.MaBoPhan
            });

            return View(data);
        }

        //AdminDeletePhan là xóa phần của sản phẩm 4
        [HttpPost, ActionName("AdminDeletePhan")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeletePhan(int map)
        {
            var data = await db.Phans.FindAsync(map);
            if (data != null)
            {
                db.Phans.Remove(data);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminPhan));
        }


        //Tìm Kiếm Hàng hóa
        public IActionResult Search(string? query)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (query != null)
            {
                hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
            }
            ViewBag.NameSearch = query;

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


        //Trang chi tiết Hàng hóa
        public IActionResult Detail(int id)
        {

            var data = db.HangHoas
                .Include(p => p.MaPhanNavigation)
                .Include(p => p.MaNccNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if (data == null)
            {
                TempData["Message"] = $"Không tìm thấy Sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var result = new ChiTietHHVM
            {
                MaHh = data.MaHh,
                TenHh = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                DiemDanhGia = 5,
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi,
                TenPhan = data.MaPhanNavigation.TenPhan,
                TenCongTy = data.MaNccNavigation.TenCongTy,
                SoLuongTon = 10,

            };
            return View(result);
        }

        /// AdminHangHoa sẽ hiển thị toàn bộ thông tin bảng Hàng Hóa
        public IActionResult AdminHangHoa()
        {
            var data = db.HangHoas.Select(p => new MenuHangHoaVM
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

            return View(data);
        }

        //AdminDeleteHangHoa là Xóa Hàng Hóa 
        [HttpPost, ActionName("AdminDeleteHangHoa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteHangHoa(int mahh)
        {
            var product = await db.HangHoas.FindAsync(mahh);
            if (product != null)
            {
                db.HangHoas.Remove(product);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminHangHoa));
        }

        //AdminDetailHangHoa là chi tiết hàng hóa
        public IActionResult AdminDetailHangHoa(int mahh)
        {
            var data = db.HangHoas
                .Include(p => p.MaPhanNavigation)
                .Include(p => p.MaNccNavigation)
                .SingleOrDefault(p => p.MaHh == mahh);
            if (data == null)
            {
                TempData["Message"] = $"Không tìm thấy Sản phẩm có mã {mahh}";
                return Redirect("/Found/404");
            }
            var result = new ChiTietHHVM
            {
                MaHh = data.MaHh,
                TenHh = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                DiemDanhGia = 5,
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi,
                TenPhan = data.MaPhanNavigation.TenPhan,
                TenCongTy = data.MaNccNavigation.TenCongTy,
                SoLuongTon = 10,
                MaNcc = data.MaNcc,
                MaPhan = data.MaPhan,
            };
            return View(result);
        }
        //AdminNCC là chi tiết Nhà Cung Cấp
        public IActionResult AdminNCC()
        {
            var data = db.NhaCungCaps.Select(p => new MenuNCCVM
            {
                MaNCC = p.MaNcc,
                TenCongTy = p.TenCongTy,
                Logo = p.Logo,
                MoTa = p.MoTa,
                NguoiLienLac = p.NguoiLienLac,
                Email = p.Email,
                DienThoai = p.DienThoai,
                DiaChi = p.DiaChi,
            });

            return View(data);
        }
        //AdminDeleteNCC là Xóa nhà cung cấp
        [HttpPost, ActionName("AdminDeleteNCC")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteNCC(string mancc)
        {
            var maNcc = await db.NhaCungCaps.FindAsync(mancc);
            if (maNcc != null)
            {
                db.NhaCungCaps.Remove(maNcc);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminNCC));
        }
        //AdminDetailNCC là chi tiết nhà cung cấp
        public IActionResult AdminDetailNCC(string? mancc)
        {
            var data = db.NhaCungCaps    
                .SingleOrDefault(p => p.MaNcc == mancc);
            if (data == null)
            {
                TempData["Message"] = $"Không tìm thấy Nhà cung cấp có mã {mancc}";
                return Redirect("/Found/404");
            }
            var result = new MenuNCCVM
            {
                MaNCC = data.MaNcc,
                TenCongTy = data.TenCongTy,
                Logo = data.Logo,
                MoTa = data.MoTa,
                NguoiLienLac = data.NguoiLienLac,
                Email = data.Email,
                DienThoai = data.DienThoai,
                DiaChi = data.DiaChi,
            };
            return View(result);
        }
    
    }
}
