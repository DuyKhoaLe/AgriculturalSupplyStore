using System;
using System.Collections.Generic;

namespace AgriculturalSupplyStore.Data;

public partial class KieuMay
{
    public int MaKieuMay { get; set; }

    public string TenKieuMay { get; set; } = null!;

    public string? TenKieuAlias { get; set; }

    public int MaLoaiMay { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public virtual ICollection<BoPhan> BoPhans { get; set; } = new List<BoPhan>();

    public virtual LoaiMay MaLoaiMayNavigation { get; set; } = null!;
}
