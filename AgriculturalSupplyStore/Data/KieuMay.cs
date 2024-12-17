using System;
using System.Collections.Generic;

namespace AgriculturalSupplyStore.Data;

public partial class KieuMay
{
    public string TenKieuMay { get; set; } = null!;

    public string? TenKieuAlias { get; set; }

    public string? MaLoaiMay { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public string MaKieuMay { get; set; } = null!;

    public virtual ICollection<BoPhan> BoPhans { get; set; } = new List<BoPhan>();

    public virtual LoaiMay? MaLoaiMayNavigation { get; set; }
}
