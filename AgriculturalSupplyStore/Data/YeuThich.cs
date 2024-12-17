using System;
using System.Collections.Generic;

namespace AgriculturalSupplyStore.Data;

public partial class YeuThich
{
    public int MaYt { get; set; }

    public string? MaHh { get; set; }

    public string? MaKh { get; set; }

    public DateTime? NgayChon { get; set; }

    public string? MoTa { get; set; }

    public virtual HangHoa? MaHhNavigation { get; set; }
}
