using System;
using System.Collections.Generic;

namespace AgriculturalSupplyStore.Data;

public partial class BoPhan
{
    public string TenBoPhan { get; set; } = null!;

    public string? TenBoPhanAlias { get; set; }

    public string? MaKieuMay { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public string MaBoPhan { get; set; } = null!;

    public virtual KieuMay? MaKieuMayNavigation { get; set; }

    public virtual ICollection<Phan> Phans { get; set; } = new List<Phan>();
}
