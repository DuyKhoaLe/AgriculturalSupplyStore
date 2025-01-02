using Microsoft.AspNetCore.Mvc;
using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AgriculturalSupplyStore.Helpers;
using AutoMapper;

namespace AgriculturalSupplyStore.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly AgriculturalsupplystoreDbContext db;
        private readonly IMapper _mapper;
        public HangHoaController(AgriculturalsupplystoreDbContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
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
        public async Task<IActionResult> AdminDeleteLoaiMay(string malm)
        {
            var product = await db.LoaiMays.FindAsync(malm);
            if (product != null)
            {
                db.LoaiMays.Remove(product);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminLoaiMay));
        }

        //AdminCreateLoaiMay là tạo loại máy 1
        [HttpGet]
        public IActionResult AdminCreateLoaiMay()
        {
            return View();
        }

        // POST: Tạo loại máy
        [HttpPost]
        public IActionResult AdminCreateLoaiMay(MenuLoaiMayVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loaiMay = _mapper.Map<LoaiMay>(model);

                    if (Hinh != null)
                    {
                        loaiMay.Hinh = MyUtil.UploadHinh(Hinh, "loaimay_images");
                    }
                    db.Add(loaiMay);
                    db.SaveChanges();
                    return RedirectToAction("AdminLoaiMay", "HangHoa");

                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }

        //Kiểu Máy là các sản phẩm khác nhau về phiên bản nhưng giống về chức năng. Tìm kiếm theo mã 2
        public IActionResult KieuMay(string? loaimay)
        {           
            var kieuMays = db.KieuMays.AsQueryable();
            if (loaimay != null)
            {
                kieuMays = kieuMays.Where(p => p.MaLoaiMay == loaimay);
            }
            if (loaimay != null)
            {
                kieuMays = kieuMays.Where(p => p.MaLoaiMay == loaimay);
             
                var loaiMayName = db.LoaiMays
                                    .Where(lm => lm.MaLoaiMay == loaimay)
                                    .Select(lm => lm.TenLoaiMay)
                                    .FirstOrDefault();       
                ViewBag.Name = loaiMayName ?? "Không tìm thấy loại máy";
            }

            var result = kieuMays.Select(p => new MenuKieuMayVM
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
        public async Task<IActionResult> AdminDeleteKieuMay(string makm)
        {
            var product = await db.KieuMays.FindAsync(makm);
            if (product != null)
            {
                db.KieuMays.Remove(product);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminKieuMay));  
        }

        //AdminCreateKieuMay là tạo Kiểu máy 2
        [HttpGet]
        public IActionResult AdminCreateKieuMay()
        {
            return View();
        }

        // POST: Tạo kiểu máy
        [HttpPost]
        public IActionResult AdminCreateKieuMay(MenuKieuMayVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var kieuMay = _mapper.Map<KieuMay>(model);

                    if (Hinh != null)
                    {
                        kieuMay.Hinh = MyUtil.UploadHinh(Hinh, "kieumay_images");
                    }
                    db.Add(kieuMay);
                    db.SaveChanges();
                    return RedirectToAction("AdminKieuMay", "HangHoa");

                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }
        


        //Bộ Phận là vị trí của máy. tìm bộ phận theo mã 3
        public IActionResult BoPhan(string? kieumay)
        {
            var boPhans = db.BoPhans.AsQueryable();
            if (kieumay != null)
            {
                boPhans = boPhans.Where(p => p.MaKieuMay == kieumay);
            }
            if (kieumay != null)
            {
                boPhans = boPhans.Where(p => p.MaKieuMay == kieumay);

                var kieuMayName = db.KieuMays
                                    .Where(lm => lm.MaKieuMay == kieumay)
                                    .Select(lm => lm.TenKieuMay)
                                    .FirstOrDefault();
                ViewBag.Name = kieuMayName ?? "Không tìm thấy kiểu máy";
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
        public async Task<IActionResult> AdminDeleteBoPhan(string mabp)
        {
            var data = await db.BoPhans.FindAsync(mabp);
            if (data != null)
            {
                db.BoPhans.Remove(data);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminBoPhan));
        }

        //AdminCreateBoPhan là tạo bộ phận máy 3
        [HttpGet]
        public IActionResult AdminCreateBoPhan()
        {
            return View();
        }

        // POST: Tạo bộ phân máy máy
        [HttpPost]
        public IActionResult AdminCreateBoPhan(MenuBoPhanVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var boPhan = _mapper.Map<BoPhan>(model);

                    if (Hinh != null)
                    {
                        boPhan.Hinh = MyUtil.UploadHinh(Hinh, "bophan_images");
                    }
                    db.Add(boPhan);
                    db.SaveChanges();
                    return RedirectToAction("AdminBoPhan", "HangHoa");

                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }



        //Phận là vị trí của Hàng hóa. Theo mã  4
        public async Task<IActionResult>Phan(string? bophan)
        {
            var Phans = db.Phans.AsQueryable();
            if (bophan != null)
            {
                Phans = Phans.Where(p => p.MaBoPhan == bophan);
            }
            if (bophan != null)
            {
                Phans = Phans.Where(p => p.MaBoPhan == bophan);

                var BoPhanName = db.BoPhans
                                    .Where(lm => lm.MaBoPhan == bophan)
                                    .Select(lm => lm.TenBoPhan)
                                    .FirstOrDefault();
                ViewBag.Name = BoPhanName ?? "Không tìm thấy bộ phận";
            }

            var result = Phans.Select(p => new MenuPhanVM
            {
                MaPhan = p.MaPhan,
                TenPhan = p.TenPhan,
                TenPhanAlias = p.TenPhanAlias,
                Hinh = p.Hinh,
                MoTa = p.MoTa,
                MaBoPhan = p.MaBoPhan,
            });
            return View(result);
        }

        //AdminBoPhan là hiển thị phần của sản phẩm 4
        public IActionResult AdminPhan()
        {
            var data = db.Phans.Select(p => new MenuPhanVM
            {
                MaPhan = p.MaPhan,
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
        public async Task<IActionResult> AdminDeletePhan(string map)
        {
            var data = await db.Phans.FindAsync(map);
            if (data != null)
            {
                db.Phans.Remove(data);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminPhan));
        }

        //AdminCreatePhan là tạo thành phần máy 4
        [HttpGet]
        public IActionResult AdminCreatePhan()
        {
            return View();
        }

        // POST: Tạo thành phần 4
        [HttpPost]
        public IActionResult AdminCreatePhan(MenuPhanVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var phan = _mapper.Map<Phan>(model);

                    if (Hinh != null)
                    {
                        phan.Hinh = MyUtil.UploadHinh(Hinh, "phan_images");
                    }
                    db.Add(phan);
                    db.SaveChanges();
                    return RedirectToAction("AdminPhan", "HangHoa");

                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
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
                MoTaNgan = p.MoTaDonVi,
                DonGia = p.DonGia,
                Hinh = p.Hinh,
                NgaySx = p.NgaySx,
                GiamGia = p.GiamGia,
                SoLanXem = p.SoLanXem,
                ChiTiet = p.MoTa,
                MaNcc = p.MaNcc
            });
            return View(result);
        }

        // Trang sản phẩm
        public IActionResult ProductItem(string? map)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (map != null)
            {
                hangHoas = hangHoas.Where(p => p.MaPhan == map);
            }
            ViewBag.NameSearch = map;

            var result = hangHoas.Select(p => new MenuHangHoaVM
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
            });
            return View(result);
        }


        //Trang chi tiết Hàng hóa
        public IActionResult Detail(string mahh)
        {

            var data = db.HangHoas
                .Include(p => p.MaPhanNavigation)
                .Include(p => p.MaNccNavigation)
                .SingleOrDefault(p => p.MaHh == mahh);
            if (data == null)
            {
                TempData["Message"] = $"Không tìm thấy Sản phẩm có mã {mahh}";
                return Redirect("/404");
            }
            var result = new ChiTietHHVM
            {
                MaHh = data.MaHh,
                TenHh = data.TenHh,
                DonGia = data.DonGia,
                ChiTiet = data.MoTa ?? string.Empty,
                DiemDanhGia = 5,
                NgaySx = data.NgaySx,
                TenAlias = data.TenAlias ?? string.Empty,
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi,
                TenPhan = data.MaPhanNavigation.TenPhan,
                TenCongTy = data.MaNccNavigation.TenCongTy,
                SoLuongTon = data.SoLuong,

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
                MoTaNgan = p.MoTaDonVi,
                DonGia = p.DonGia,
                Hinh = p.Hinh,
                NgaySx = p.NgaySx,
                GiamGia = p.GiamGia,
                SoLanXem = p.SoLanXem,
                ChiTiet = p.MoTa,
                MaNcc = p.MaNcc
            });

            return View(data);
        }

        ///AdminCreateHangHoa là thêm hàng hóa vào database
        // GET: Tạo hàng hóa
        [HttpGet]
        public IActionResult AdminCreateHangHoa()
        {
            return View();
        }

        // POST: Tạo hàng hóa
        [HttpPost]
        public IActionResult AdminCreateHangHoa(MenuHangHoaVM model, IFormFile Hinh)
        {   
            if (ModelState.IsValid)
            {
                try
                {
                    var hangHoa = _mapper.Map<HangHoa>(model);                   
                    hangHoa.SoLanXem = 0;                   

                    if (Hinh != null)
                    {
                        hangHoa.Hinh = MyUtil.UploadHinh(Hinh, "hanghoa_images");
                    }
                    db.Add(hangHoa);
                    db.SaveChanges();
                    return RedirectToAction("AdminHangHoa", "HangHoa");

                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }

        //AdminEditHangHoa là trang lấy hàng hóa
        [HttpGet]
        public async Task<IActionResult> AdminEditHangHoa(string mahh)
        {
            if (mahh == null)
            {
                TempData["Message"] = $"Không tìm thấy Sản phẩm có mã {mahh}";
                return Redirect("Found/404");
            }

            var hangHoa = await db.HangHoas.FindAsync(mahh);
            if (hangHoa == null)
            {
                TempData["Message"] = $"Không tìm thấy Sản phẩm có mã {mahh}";
                return Redirect("Found/404");
            }
            var model = _mapper.Map<MenuHangHoaVM>(hangHoa);
            return View(model);
        }
        /// AdminEditHangHoa là thực hiện chỉnh sửa hàng hóa
        [HttpPost]
        public IActionResult AdminEditHangHoa(MenuHangHoaVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var hangHoa = db.HangHoas.Find(model.MaHh);
                    if (hangHoa == null)
                    {
                        return Redirect("/Found/404");
                    }
                    _mapper.Map(model, hangHoa);

                    if (Hinh != null)
                    {
                        hangHoa.Hinh = MyUtil.UploadHinh(Hinh, "hanghoa_images");
                    }

                    db.Update(hangHoa); 
                    db.SaveChanges(); 
                    return RedirectToAction("AdminHangHoa", "HangHoa");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";           
                }
            }
            return View(model); 
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
        public IActionResult AdminDetailHangHoa(string mahh)
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
                DonGia = data.DonGia,
                ChiTiet = data.MoTa ?? string.Empty,
                DiemDanhGia = 5,
                Hinh = data.Hinh ?? string.Empty,
                NgaySx = data.NgaySx,
                TenAlias = data.TenAlias ?? string.Empty,
                MoTaNgan = data.MoTaDonVi,                
                TenPhan = data.MaPhanNavigation.TenPhan,
                TenCongTy = data.MaNccNavigation.TenCongTy,
                SoLuongTon = data.SoLuong,
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
        ///AdminCreateNCC là thêm nhà cung cấp vào database
        // GET: Tạo nhà cung cấp
        [HttpGet]
        public IActionResult AdminCreateNCC()
        {
            return View();
        }

        // POST: Tạo nhà cung cấp
        [HttpPost]
        public IActionResult AdminCreateNCC(MenuNCCVM model, IFormFile logo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nhaCc = _mapper.Map<NhaCungCap>(model);
                   

                    if (logo != null)
                    {
                        nhaCc.Logo = MyUtil.UploadHinh(logo, "nhacungcap_images");
                    }
                    db.Add(nhaCc);
                    db.SaveChanges();
                    return RedirectToAction("AdminNCC", "HangHoa");

                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }
    }
}
