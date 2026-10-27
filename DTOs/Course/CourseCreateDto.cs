namespace Day1.DTOs.Course
{
    public class CourseCreateDto
    {
        public string CourseName { get; set; } = string.Empty;

        public int? Credits { get; set; }

        public string? Description { get; set; }

        public string? Department { get; set; }

        public string? Instructor { get; set; }

        public string? Semester { get; set; }

        public int? MaxEnrollment { get; set; }
    }
}
