using System;
using System.Collections.Generic;

namespace AgriculturalSupplyStore.Data;

public partial class Phan
{
    public string TenPhan { get; set; } 

    public string? TenPhanAlias { get; set; }

    public string? MaBoPhan { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public string MaPhan { get; set; } 

    public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();

    public virtual BoPhan? MaBoPhanNavigation { get; set; }
}
