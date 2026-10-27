
using System.ComponentModel.DataAnnotations;

namespace Day1.DTOs.Course
{
    public class CourseEditDto
    {
        [Required]
        [StringLength(100)]
        public string CourseName { get; set; } = string.Empty;

        public int? Credits { get; set; }

        public string? Description { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [StringLength(100)]
        public string? Instructor { get; set; }

        [StringLength(20)]
        public string? Semester { get; set; }

        public int? MaxEnrollment { get; set; }
    }
}
