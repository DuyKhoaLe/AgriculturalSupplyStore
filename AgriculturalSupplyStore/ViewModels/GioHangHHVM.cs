namespace AgriculturalSupplyStore.ViewModels
{
    public class GioHangHHVM
    {
            public string MaHh { get; set; }
            public string TenHh { get; set; }
            public string Hinh { get; set; }
            public double DonGia { get; set; }
            public int SoLuong { get; set; }
        public string TenPhan{ get; set; }
        public double ThanhTien => SoLuong * DonGia;       
   

    }
}
