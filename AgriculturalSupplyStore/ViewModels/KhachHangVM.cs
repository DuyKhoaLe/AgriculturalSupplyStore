using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
    public class KhachHangVM
    {
        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        public string MaKh { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string HoTen { get; set; }

        [Display(Name = "Giới Tính ")]
        public bool GioiTinh { get; set; } = true;

        [DataType(DataType.Date)]
        [Display(Name = "Ngày Sinh")]
        public DateTime NgaySinh { get; set; }

        [MaxLength(60, ErrorMessage = "Tối đa 60 ký tự")]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [MaxLength(24, ErrorMessage = "Tối đa 24 ký tự")]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Chưa đúng định dạng di động việt nam")]
        [Display(Name = "Số Điện Thoại")]
        public string DienThoai { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Hình Ảnh")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng ảnh")]
        public string? Hinh { get; set; } = null!;

        public bool HieuLuc { get; set; }

        public int VaiTro { get; set; }

    }
}
