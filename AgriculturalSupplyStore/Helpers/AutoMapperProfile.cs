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
                //.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
                //.ReverseMap();
        }
    }
}
