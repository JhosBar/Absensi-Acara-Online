namespace Absensi.Services.Base
{
    public class RecapData
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public string Event { get; set; } = null!;
        public string? CreatedText { get; set; }
        public DateTime? Created { get; set; }
        public string attenderName { get; set; } = null!;
        public string attenderPhone { get; set; } = null!;
        public string attenderEmail { get; set; } = null!;
        public string aSignature { get; set; } = null!;
    }

    public class RecapFilterData
    {
        public string? qEvent { get; set; }
        public string? qAttender { get; set; }
    }
}
