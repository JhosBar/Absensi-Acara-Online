using System;
using System.Collections.Generic;

namespace Absensi.EF.Data;

public partial class MtEvent
{
    public long Id { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? Created { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? Updated { get; set; }

    public bool FlgDeleted { get; set; }

    public string Name { get; set; } = null!;

    public string? ELocation { get; set; }

    public DateTime? EHour { get; set; }

    public DateOnly? EDate { get; set; }

    public string? ENote { get; set; }

    public virtual ICollection<PRecap> PRecaps { get; set; } = new List<PRecap>();

    public virtual ICollection<PTransaction> PTransactions { get; set; } = new List<PTransaction>();
}
