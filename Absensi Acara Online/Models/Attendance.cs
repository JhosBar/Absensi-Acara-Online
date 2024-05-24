using System.ComponentModel.DataAnnotations;

namespace Absensi.Models
{
    public class AttendVM
    {
        [Required]
        public long EventId { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IFormFile? Signature { get; set; }

    }
}
