using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
	public class MenuLoaiMayVM
	{
        [Display(Name = "Mã loại máy")]
        [Required(ErrorMessage = "*")]
        public string MaLoaiMay { get; set; }

        [Display(Name = "Tên loại máy")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string? TenLoaiMay { get; set; }

        [Display(Name = "Tên loại máy Alias")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string TenLoaiAlias { get; set; }

        [Display(Name = "Mô tả ")]
        [Required(ErrorMessage = "*")]        
        public string MoTa {  get; set; }


        [Display(Name = "Hình ảnh")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng ảnh")]
        public string? Hinh { get; set; }

	}
}
