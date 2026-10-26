using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Day1.DTOs.Student
{
    public class StudentReadDto
    {
        public int ID { get; set; }
        [Required]
        [StringLength(60)]
        [Unicode(false)]
        public string FullName { get; set; } = string.Empty;

        public int? Age { get; set; }

        public DateOnly DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Email { get; set; }

        [StringLength(15)]
        [Unicode(false)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string Address { get; set; }

        public DateOnly EnrollmentDate { get; set; }
    }
}
