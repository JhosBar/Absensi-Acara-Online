using System;
using System.Collections.Generic;

namespace Absensi.EF.Data;

public partial class PRecap
{
    public long Id { get; set; }

    public DateTime? Created { get; set; }

    public string AttenderName { get; set; } = null!;

    public string AttenderPhone { get; set; } = null!;

    public string AttenderEmail { get; set; } = null!;

    public string ASignature { get; set; } = null!;

    public long EventId { get; set; }

    public bool FlgDeleted { get; set; }

    public virtual MtEvent Event { get; set; } = null!;
}
