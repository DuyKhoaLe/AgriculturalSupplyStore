using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
	public class MenuNCCVM
	{
        [Display(Name = "Mã nhà cung cấp")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string MaNCC { get; set; }

        [Display(Name = "Tên công ty")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string TenCongTy { get; set; }

        [Display(Name = "Hình ảnh")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng ảnh")]
        public string? Logo { get; set; }

        [Display(Name = "Tên người liên lạc")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string? NguoiLienLac { get; set; }


        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        public string Email { get; set; } = null!;

        [MaxLength(24, ErrorMessage = "Tối đa 24 ký tự")]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Chưa đúng định dạng di động việt nam")]
        [Display(Name = "Điện thoại")]
        public string? DienThoai { get; set; }

        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }
       
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
	}
}
