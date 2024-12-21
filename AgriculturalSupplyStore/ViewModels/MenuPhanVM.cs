using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
	public class MenuPhanVM
	{
        [Display(Name = "Mã vị trí của sản phẩm")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string MaPhan { get; set; }


        [Display(Name = "Tên vị trí của sản phẩm")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]

        public string TenPhan { get; set; } = null!;

        [Display(Name = "Tên vị trí Alias")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]

        public string? TenPhanAlias { get; set; }

        [Display(Name = "Mã bộ phận máy")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string MaBoPhan { get; set; }

        [Display(Name = "Mô tả ")]
        [Required(ErrorMessage = "*")]
        public string? MoTa { get; set; }


        [Display(Name = "Hình ảnh")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng ảnh")]
        public string? Hinh { get; set; }
	}
}
