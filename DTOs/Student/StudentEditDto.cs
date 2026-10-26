
using System.ComponentModel.DataAnnotations;

namespace Day1.DTOs.Student
{
    public class StudentEditDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        public int? Age { get; set; }

        public DateOnly DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }
    }
}
