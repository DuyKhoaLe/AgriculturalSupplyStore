using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AgriculturalSupplyStore.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult AddToCart(string id, int quantity = 1)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = db.HangHoas
                     .Include(p => p.MaPhanNavigation)
                    .SingleOrDefault(p => p.MaHh == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                item = new GioHangHHVM
                {
                    MaHh = hangHoa.MaHh,
                    TenHh = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia,
                    TenPhan = hangHoa.MaPhanNavigation.TenPhan,
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
        public IActionResult RemoveCart(string id)
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
        [HttpGet]
        public IActionResult Checkout()
        {
            if (Cart.Count == 0)
            {
                return Redirect("/");
            }
            return View(Cart);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;
                var khachHang = new KhachHang();
                if (model.GiongKhachHang)
                {
                    khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == customerId);
                }
                var hoadon = new HoaDon
                {
                    MaKh = customerId,
                    HoTen = model.HoTen ?? khachHang?.HoTen,
                    DiaChi = model.DiaChi ?? khachHang?.DiaChi,
                    DienThoai = model.DienThoai ?? khachHang?.DienThoai,
                    NgayDat = DateTime.Now,
                    CachThanhToan = "COD",
                    CachVanChuyen = "Express",
                    MaTrangThai = 0,
                    GhiChu = model.GhiChu                    
                };

                db.Database.BeginTransaction();
                try
                {
                    db.Add(hoadon);
                    db.SaveChanges();

                    var cthds = new List<ChiTietHd>();
                    foreach (var item in Cart)
                    {
                        cthds.Add(new ChiTietHd
                        {
                            MaHd = hoadon.MaHd,
                            SoLuong = item.SoLuong,
                            DonGia = item.DonGia,
                            MaHh = item.MaHh,
                            GiamGia = 0
                        });
                    }
                    db.AddRange(cthds);
                    db.SaveChanges();
                    foreach (var item in Cart)
                    {
                        var hangHoa = db.HangHoas.SingleOrDefault(hh => hh.MaHh == item.MaHh);
                        if (hangHoa != null)
                        {
                            if (hangHoa.SoLuong >= item.SoLuong)
                            {
                                hangHoa.SoLuong -= item.SoLuong; 
                                db.Update(hangHoa);
                            }
                            else
                            {
                                throw new Exception($"Hàng hóa {hangHoa.TenHh} không đủ số lượng trong kho.");
                            }
                        }
                    }

                    db.SaveChanges(); 

                    db.Database.CommitTransaction();
                    HttpContext.Session.Set<List<GioHangHHVM>>(MySetting.CART_KEY, new List<GioHangHHVM>());
                    return View("Success");
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    ModelState.AddModelError("", "Có lỗi xảy ra khi đặt hàng: " + ex.Message);
                }
            }
            return View(Cart);
        }


        ///AdminHoaDon là trang hiển thị thông tin các đơn hàng
        public IActionResult AdminHoaDon()
        {
            var data = db.HoaDons               
                 .Include(p => p.MaTrangThaiNavigation)     
                .Select(p => new HoaDonVM
            {
                MaHd = p.MaHd,
                MaKh = p.MaKh,
                HoTen = p.HoTen,
                CachThanhToan = p.CachThanhToan,
                CachVanChuyen = p.CachVanChuyen,
                TenTrangThai = p.MaTrangThaiNavigation.TenTrangThai,
                MaTrangThai = p.MaTrangThai,                
            });

            return View(data);
        }


        ///CustomerHoaDon là trang hiển thị thông tin các đơn hàng, tên khách hàng, địa chỉ
        public IActionResult CustomerHoaDon(int mahd)
        {
            var hoaDons = db.HoaDons
               .Include(p => p.MaTrangThaiNavigation)
               .Include(p => p.MaKhNavigation)
               .AsQueryable();
            if (mahd != null)
            {
                hoaDons = hoaDons.Where(p => p.MaHd == mahd);
            }
            var result = hoaDons.Select(p => new HoaDonVM
            {                
                MaHd = p.MaHd,
                MaKh = p.MaKh,
                HoTen = p.HoTen,
                CachThanhToan = p.CachThanhToan,
                CachVanChuyen = p.CachVanChuyen,
                TenTrangThai = p.MaTrangThaiNavigation.TenTrangThai,
                MaTrangThai = p.MaTrangThai,
                NgayDat = p.NgayDat,
                NgayCan = p.NgayCan,
                NgayGiao = p.NgayGiao,
                DiaChi = p.DiaChi,
                PhiVanChuyen = p.PhiVanChuyen,
                MaNv = p.MaNv,
                TenNv = p.MaNvNavigation.HoTen,
                GhiChu = p.GhiChu,
                DienThoai = p.DienThoai,
            });
            return View(result);



        }

        ///CustomerCTHD là trang hiển thị thông tin chi tiết các đơn hàng
        public IActionResult CustomerCTHD()
        {           
            var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;

            if (customerId == null)
            {
                return RedirectToAction("Login", "KhachHang");
            }
            var data = db.ChiTietHds
                .Include(p => p.MaHhNavigation)
                 .ThenInclude(hh => hh.MaPhanNavigation)
                .Include(p => p.MaHdNavigation)
                .Where(p => p.MaHdNavigation.MaKh == customerId)               
                .Select(p => new ChiTietHDVM
                {
                    MaCt = p.MaCt,
                    MaHd = p.MaHd,
                    TenHh = p.MaHhNavigation.TenHh,                
                    DonGia = p.DonGia,
                    SoLuong = p.SoLuong,
                    GiamGia = p.GiamGia,
                    TenPhan = p.MaHhNavigation.MaPhanNavigation.TenPhan,
                    Hinh = p.MaHhNavigation.Hinh,
                    MaTrangThai = p.MaHdNavigation.MaTrangThai
                    
                })
                .ToList();
            if (!data.Any())
            {
                TempData["Message"] = $"Bạn chưa mua đơn hàng nào";
                return Redirect("/404");
            }

            return View(data);
        }

        public IActionResult AdminDetailHoaDon(int? mahd)
        {
            var hoaDons = db.HoaDons
                 .Include(p => p.MaNvNavigation)
                .AsQueryable();
            if (mahd.HasValue)
            {
                hoaDons = hoaDons.Where(p => p.MaHd == mahd.Value);
            }
            var result = hoaDons.Select(p => new HoaDonVM
            {
                MaHd = p.MaHd,
                MaKh = p.MaKh,
                HoTen = p.HoTen,
                CachThanhToan = p.CachThanhToan,
                CachVanChuyen = p.CachVanChuyen,
                TenTrangThai = p.MaTrangThaiNavigation.TenTrangThai,
                MaTrangThai = p.MaTrangThai,
                NgayDat = p.NgayDat,
                NgayCan = p.NgayCan,
                NgayGiao = p.NgayGiao,
                DiaChi = p.DiaChi,
                PhiVanChuyen = p.PhiVanChuyen,
                MaNv = p.MaNv,
                TenNv = p.MaNvNavigation.HoTen,
                GhiChu = p.GhiChu,
                DienThoai = p.DienThoai,
            });
            return View(result);
        }

        ///AdminChiTietHD là trang hiển thị thông tin chi tiết đơn hàng
        public IActionResult AdminChiTietHD(int? mahd)
        {
            var CThoaDons = db.ChiTietHds    
                .Include(p => p.MaHdNavigation)
                .AsQueryable();
            if (mahd.HasValue)
            {
                CThoaDons = CThoaDons.Where(p => p.MaHd == mahd.Value);
            }
            var result = CThoaDons.Select(p => new ChiTietHDVM
            {
                MaCt = p.MaCt,
                MaHd = p.MaHd,
                TenHh = p.MaHhNavigation.TenHh,
                MaHh = p.MaHh,
               DonGia = p.DonGia,
               SoLuong = p.SoLuong,
               GiamGia = p.GiamGia,   
               Hinh = p.MaHhNavigation.Hinh,
            });
            return View(result);
        }
    }
}
