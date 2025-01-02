using AgriculturalSupplyStore.Data;
using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
    public class GopYVM
    {
        [Display(Name = "Tên đăng nhập khách hàng")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string MaGy { get; set; }

        [Display(Name = "Mã chủ đề")]
        [Required(ErrorMessage = "*")]
        public int MaCd { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "*")]
        public string NoiDung { get; set; }

        public DateTime NgayGy { get; set; }

        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string? HoTen { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        public string? Email { get; set; }

        [MaxLength(24, ErrorMessage = "Tối đa 24 ký tự")]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Chưa đúng định dạng di động việt nam")]
        [Display(Name = "Số Điện Thoại")]
        public string? DienThoai { get; set; } 

        public bool CanTraLoi { get; set; } = false;

        public string? TraLoi { get; set; } = null;

        public DateTime? NgayTl { get; set; } = null;

        public virtual ChuDe? MaCdNavigation { get; set; }
    }
}
