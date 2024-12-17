namespace AgriculturalSupplyStore.ViewModels
{
    public class ChiTietHHVM
    {
        public string MaHh { get; set; }

        public string TenHh { get; set; }
        public int SoLuong { get; set; }
        public string MaNcc { get; set; }
        public string TenCongTy { get; set; }

        public string? MoTaNgan { get; set; }

        public double? DonGia { get; set; }

        public string? Hinh { get; set; }
        public string MaPhan { get; set; }
        public DateTime NgaySx { get; set; }
        public string? TenAlias { get; set; }
        public string? TenPhan{ get; set; }       

        public string?  ChiTiet { get; set; }     

        public int DiemDanhGia { get; set; }

        public int SoLuongTon { get; set; } 
    }
}
