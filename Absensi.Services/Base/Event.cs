namespace Absensi.Services.Base
{
    public class EventCreate
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
    
    public class EData
    {
        public long Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedText { get; set; }
        public DateTime? Created { get; set; }
        public string Event { get; set; } = null!;
        public string Hour { get; set; } = null!;
        public DateOnly? Date { get; set; }
        public string? DateText { get; set; }
        public string Location { get; set; } = null!;
        public string? Note { get; set; }
    }
    
    public class EventData
    {
        public long Id { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public string Event { get; set; } = null!;
        public string Hour { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string? Note { get; set; }
    }

    public class EventFilterData
    {
        public string? qEvent { get; set; }
        public string? qLocation { get; set; }
        public string? qDateStart { get; set; }
        public string? qDateEnd { get; set; }
    }

	public class EventList
	{
		public long Id { get; set; }
		public string Event { get; set; } = null!;
		public DateOnly? Date { get; set; }
		public string? DateText { get; set; }
		public DateTime? Time { get; set; }
		public string TimeText { get; set; } = null!;
		public string? Location { get; set; }
		public string? Note { get; set; }

	}
	public class AttenderList
    {
		public long Id { get; set; }
		public string Name { get; set; } = null!;
        public DateTime? SubmitTime { get; set; }
        //public string SubmitTime { get; set; } = null!;

	}
}
