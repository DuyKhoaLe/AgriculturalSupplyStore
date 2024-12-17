using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.Helpers;
using AgriculturalSupplyStore.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AgriculturalSupplyStore.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly AgriculturalsupplystoreDbContext db;
        private readonly IMapper _mapper;

        public KhachHangController(AgriculturalsupplystoreDbContext context, IMapper mapper) 
        {
            db = context;
            _mapper = mapper;
        }
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = MyUtil.GenerateRandomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    khachHang.HieuLuc = true;
                    khachHang.VaiTro = 0;

                    if (Hinh != null)
                    {
                        khachHang.Hinh = MyUtil.UploadHinh(Hinh, "khachhang_images");
                    }
                    db.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Login", "KhachHang");

                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }
        #endregion
        #region Login in 
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
                if (khachHang == null)
                {
                    ModelState.AddModelError("Lỗi", "Không tồn tại khách hàng này.");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("Lỗi", "Tài khoản đã bị khóa.");
                    }
                    else
                    {
                        if (khachHang.MatKhau == model.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập.");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, khachHang.Email),
                                 new Claim(ClaimTypes.Name, khachHang.HoTen),
                                  new Claim(MySetting.CLAIM_CUSTOMERID, khachHang.MaKh),

                                  new Claim(ClaimTypes.Role, "Customer")
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction(nameof(Profile));
                            }

                        }
                    }
                }
            }
            return View();
        }
        #endregion
        [Authorize]
        public IActionResult Profile()
        { 
            return View(); 
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");      
        }

        /// AdminKhachHang sẽ hiển thị toàn bộ thông tin bảng khách hàng
        public IActionResult AdminKhachHang()
        {
            var data = db.KhachHangs.Select(p => new KhachHangVM
            {
                MaKh = p.MaKh,
                HoTen = p.HoTen,
                MatKhau = p.MatKhau,
                GioiTinh = p.GioiTinh,
                NgaySinh = p.NgaySinh,
                DiaChi = p.DiaChi,
                Email = p.Email,
                Hinh = p.Hinh,
                HieuLuc = p.HieuLuc,
                VaiTro = p.VaiTro,
            });

            return View(data);
        }

        //AdminDeleteKhachHang là Xóa Tài khoản khách hành
        [HttpPost, ActionName("AdminDeleteKhachHang")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteKhachHang(string makh)
        {
            var user = await db.KhachHangs.FindAsync(makh);
            if (user != null)
            {
                db.KhachHangs.Remove(user);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AdminKhachHang));
        }

        //AdminDetailKhachHang là chi tiết khách hàng
        public IActionResult AdminDetailKhachHang(string? makh)
        {
            var data = db.KhachHangs
                .SingleOrDefault(p => p.MaKh == makh);
            if (data == null)
            {
                TempData["Message"] = $"Không tìm thấy Nhà cung cấp có mã {makh}";
                return Redirect("/Found/404");
            }
            var result = new KhachHangVM
            {
                MaKh = data.MaKh,
                HoTen = data.HoTen,
                MatKhau = data.MatKhau,
                GioiTinh = data.GioiTinh,
                NgaySinh = data.NgaySinh,
                DienThoai = data.DienThoai,
                DiaChi = data.DiaChi,
                Email = data.Email,
                Hinh = data.Hinh,
                HieuLuc = data.HieuLuc,
                VaiTro = data.VaiTro,
            };
            return View(result);
        }

    }
}
