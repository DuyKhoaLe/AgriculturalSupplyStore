namespace AgriculturalSupplyStore.ViewModels
{
    public class ChiTietHDVM
    {
        public int MaCt { get; set; }

        public int? MaHd { get; set; }

        public string MaHh { get; set; }

        public double DonGia { get; set; }

        public string TenPhan { get; set; }
        public int MaTrangThai { get; set; }
        public int SoLuong { get; set; }

        public double GiamGia { get; set; }
        public string TenHh { get; set; }
        public double ThanhTien => SoLuong * DonGia;

        public string Hinh { get; set; }
    }
}
