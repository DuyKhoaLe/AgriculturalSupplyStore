using System;
using System.Collections.Generic;

namespace AgriculturalSupplyStore.Data;

public partial class LoaiMay
{
    public int MaLoaiMay { get; set; }

    public string TenLoaiMay { get; set; } = null!;

    public string? TenLoaiAlias { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public virtual ICollection<KieuMay> KieuMays { get; set; } = new List<KieuMay>();
}
