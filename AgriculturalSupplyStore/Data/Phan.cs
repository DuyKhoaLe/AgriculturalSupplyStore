using System;
using System.Collections.Generic;

namespace AgriculturalSupplyStore.Data;

public partial class Phan
{
    public int MaPhan { get; set; }

    public string TenPhan { get; set; } = null!;

    public string? TenPhanAlias { get; set; }

    public int MaBoPhan { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();

    public virtual BoPhan MaBoPhanNavigation { get; set; } = null!;
}
