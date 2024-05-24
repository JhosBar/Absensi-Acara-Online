namespace Absensi.Models
{
	public class DatatableVM
	{
		public int draw { get; set; }
		public int start { get; set; }
		public int length { get; set; }
		public List<Dictionary<string, string>> order { get; set; }
		public DTColumn[] columns { get; set; }

		//public List<Dictionary<string, string>> columns { get; set; }
		//public KeyValuePair<string, string> columnName { get; set; }
		//public DTSearch? search { get; set; }
	}


	public class DTColumn
	{
		public string data { get; set; }
		public string name { get; set; }
		public bool Searchable { get; set; }
		public bool Orderable { get; set; }
	}
	public class DTSearch
	{
		public string? value { get; set; }
		public string? regex { get; set; }
	}
	public class DTOrder
	{
		public int Order { get; set; }
	}
	
}
