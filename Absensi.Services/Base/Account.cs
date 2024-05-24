namespace Absensi.Services.Base
{
	public class AccountLogin
	{
		public long Id { get; set; }
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public long RoleId { get; set; }
		public string Role { get; set; } = null!;
	}

	public class AccountPass
	{
		public string Current_Password { get; set; } = null!;
		public long Id { get; set; }
		public string Password { get; set; } = null!;
		public string UpdatedBy { get; set; } = null!;
	}
}
