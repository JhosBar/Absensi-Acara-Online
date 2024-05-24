using System.ComponentModel.DataAnnotations;

namespace Absensi.Models
{
    public class EventCreateVM
    {
        public string? CreatedBy { get; set; }
        public string Event { get; set; } = null!;
        //public DateTime Hour { get; set; }
        public string Hour { get; set; } = null!;
        //public DateOnly Date { get; set; }
        public string Date { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string? Note { get; set; }
    }

    public class EventUpdateVM
    {
        public long Id { get; set; }
        public string? UpdatedBy { get; set; }
        public string Event { get; set; } = null!;
        public string Hour { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string? Note { get; set; }
    }

    public class EventFilterVM
    {
        public string? qEvent { get; set; }
        public string? qLocation { get; set; }
        public string? qDateStart { get; set; }
        public string? qDateEnd { get; set; }
    }
}
