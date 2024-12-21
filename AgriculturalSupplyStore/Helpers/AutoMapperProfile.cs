using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.ViewModels;
using AutoMapper;

namespace AgriculturalSupplyStore.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterVM, KhachHang>();
            CreateMap<MenuHangHoaVM, HangHoa>().ForMember(kh => kh.MoTaDonVi, option => option.MapFrom(MenuHangHoaVM => MenuHangHoaVM.MoTaNgan))
                .ForMember(kh => kh.MoTa, option => option.MapFrom(MenuHangHoaVM => MenuHangHoaVM.ChiTiet)).ReverseMap();
            CreateMap<MenuNCCVM, NhaCungCap>();
            CreateMap<MenuLoaiMayVM, LoaiMay>();
            CreateMap<MenuKieuMayVM, KieuMay>();
            CreateMap<MenuBoPhanVM, BoPhan>();
            CreateMap<MenuPhanVM, Phan>();
            //.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
            //.ReverseMap();
        }
    }
}
