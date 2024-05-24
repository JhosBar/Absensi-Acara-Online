using System;
using System.Collections.Generic;

namespace Absensi.EF.Data;

public partial class PTransaction
{
    public long Id { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? Created { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? Updated { get; set; }

    public bool FlgDeleted { get; set; }

    public DateTime Date { get; set; }

    public long BrandId { get; set; }

    public string? Result { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? AnalyzeTitle { get; set; }

    public string? AnalyzeData { get; set; }

    public string? PrimaryTitle { get; set; }

    public string? PrimaryData { get; set; }

    public string? SecondaryTitle { get; set; }

    public string? SecondaryData { get; set; }

    public virtual MtEvent Brand { get; set; } = null!;
}
