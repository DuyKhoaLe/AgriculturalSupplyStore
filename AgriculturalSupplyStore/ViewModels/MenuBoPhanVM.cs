using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
	public class MenuBoPhanVM
	{
        [Display(Name = "Mã bộ phận máy")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string MaBoPhan { get; set; }

        [Display(Name = "Tên bộ phận máy")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string? TenBoPhan { get; set; }

        [Display(Name = "Tên bộ phận Alias")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string TenBoPhanAlias { get; set; }

        [Display(Name = "Mã kiểu máy")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string? MaKieuMay { get; set; }

        [Display(Name = "Mô tả ")]
        [Required(ErrorMessage = "*")]
        public string MoTa { get; set; }

        [Display(Name = "Hình ảnh")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng ảnh")]
        public string? Hinh { get; set; }

	}
}
