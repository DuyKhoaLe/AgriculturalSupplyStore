namespace AgriculturalSupplyStore.ViewModels
{
	public class MenuHangHoaVM
	{
		public int MaHh { get; set; }

		public string TenHh { get; set; } = null!;

		public string? TenAlias { get; set; }

		public int MaPhan { get; set; }

		public string? MoTaDonVi { get; set; }

		public double? DonGia { get; set; }

		public string? Hinh { get; set; }

		public DateTime NgaySx { get; set; }

		public double GiamGia { get; set; }

		public int SoLanXem { get; set; }

		public string? MoTa { get; set; }

		public string MaNcc { get; set; } = null!;
	}
}
