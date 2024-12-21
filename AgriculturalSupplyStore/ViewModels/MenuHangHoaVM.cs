using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
	public class MenuHangHoaVM
	{
        [Display(Name = "Mã hàng hóa")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string MaHh { get; set; }

        [Display(Name = "Tên hàng hóa")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string TenHh { get; set; } = null!;

        [Display(Name = "Tên Alias")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string? TenAlias { get; set; }

        [Display(Name = "Mã vị trí")]
        [Required(ErrorMessage = "*")]
        public string MaPhan { get; set; }

        [Display(Name = "Mô tả ngắn")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string? MoTaNgan { get; set; }

        [Display(Name = "Đơn giá")]
        [Required(ErrorMessage = "*")]
        public double DonGia { get; set; }

        [Display(Name = "Hình ảnh")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng ảnh")]
        public string? Hinh { get; set; } = null!;

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sản xuất")]
        public DateTime NgaySx { get; set; }

        [Display(Name = "Giảm giá")]       
        public double GiamGia { get; set; }       
        public int SoLanXem { get; set; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "*")]
        public int SoLuong { get; set; }

        [Display(Name = "Chi tiết")]
        [Required(ErrorMessage = "*")]      
        public string? ChiTiet { get; set; }

        [Display(Name = "Mã nhà cung cấp")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string MaNcc { get; set; } = null!;
	}
}
