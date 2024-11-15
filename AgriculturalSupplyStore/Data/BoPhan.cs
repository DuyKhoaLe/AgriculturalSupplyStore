using System;
using System.Collections.Generic;

namespace AgriculturalSupplyStore.Data;

public partial class BoPhan
{
    public int MaBoPhan { get; set; }

    public string TenBoPhan { get; set; } = null!;

    public string? TenBoPhanAlias { get; set; }

    public int MaKieuMay { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public virtual KieuMay MaKieuMayNavigation { get; set; } = null!;

    public virtual ICollection<Phan> Phans { get; set; } = new List<Phan>();
}
