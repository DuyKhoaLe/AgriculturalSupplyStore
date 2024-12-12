using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        public string UserName { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
