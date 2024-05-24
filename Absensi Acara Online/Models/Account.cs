using System.ComponentModel.DataAnnotations;

namespace Absensi.Models
{
	public class LoginVM
	{
		[Required]
		public string Username { get; set; } = null!;
		[Required]
		public string Password { get; set; } = null!;
	}
	public class AccountPassVM
	{
		[Required]
		public long Id { get; set; }
		[Required]
		public string Current_Password { get; set; } = null!;
		[Required]
		public string Password { get; set; } = null!;
	}
}
