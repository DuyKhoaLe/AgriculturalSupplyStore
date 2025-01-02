using System.ComponentModel.DataAnnotations;

namespace AgriculturalSupplyStore.ViewModels
{
    public class MessageModel
    {       
        public string MaGy { get; set; }
        public string NoiDung { get; set; }
        public string? HoTen { get; set; }   
        public DateTime NgayGy {  get; set; }
        

    }
}
