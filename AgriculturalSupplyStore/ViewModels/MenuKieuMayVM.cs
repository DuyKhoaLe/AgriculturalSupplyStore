using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
	public class MenuKieuMayVM
	{
        [Display(Name = "Mã kiểu máy")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string MaKieuMay { get; set; }

        [Display(Name = "Tên kiểu máy")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string? TenKieuMay { get; set; }

        [Display(Name = "Mã loại máy")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string? MaLoaiMay { get; set; }

        [Display(Name = "Tên kiểu Alias")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string TenKieuAlias {  get; set; }

        [Display(Name = "Mô tả ")]
        [Required(ErrorMessage = "*")]
        public string MoTa { get; set; }

        [Display(Name = "Hình ảnh")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng ảnh")]
        public string? Hinh { get; set; }
	}
}
