
using Day1.DTOs.Course;
using Day1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly SchoolContext db;

        public CoursesController(SchoolContext db)
        {
            this.db = db;
        }

        // GET: api/Courses
        
        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = db.Courses.Select(
                c => new CourseReadDto
                {
                    CourseID = c.CourseID,
                    CourseName = c.CourseName,
                    Credits = c.Credits,
                    Description = c.Description,
                    Department = c.Department,
                    Instructor = c.Instructor,
                    Semester = c.Semester,
                    MaxEnrollment = c.MaxEnrollment
                }).ToList();
            return Ok(courses);
        }

        // GET: api/Courses/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new
                {
                    message = "Invalid Course ID."
                });

            var course = db.Courses.Find(id);

            if (course == null)
                return NotFound(new
                {
                    message = "Course Not Found"
                });
            CourseReadDto dto = new CourseReadDto()
            {
                CourseID = course.CourseID,
                CourseName = course.CourseName,
                Credits = course.Credits,
                Description = course.Description,
                Department = course.Department,
                Instructor = course.Instructor,
                Semester = course.Semester,
                MaxEnrollment = course.MaxEnrollment
            };


            return Ok(dto);
        }

        // POST: api/Courses
        [HttpPost]

        public IActionResult AddCourse(CourseCreateDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = new Course()
            {
                CourseName = courseDto.CourseName,
                Credits = courseDto.Credits,
                Description = courseDto.Description,
                Department = courseDto.Department,
                Instructor = courseDto.Instructor,
                Semester = courseDto.Semester,
                MaxEnrollment = courseDto.MaxEnrollment
            };

            db.Courses.Add(course);
            db.SaveChanges();

            var courseReadDto = new CourseReadDto()
            {
                CourseID = course.CourseID,
                CourseName = course.CourseName,
                Credits = course.Credits,
                Description = course.Description,
                Department = course.Department,
                Instructor = course.Instructor,
                Semester = course.Semester,
                MaxEnrollment = course.MaxEnrollment
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = course.CourseID },
                courseReadDto
            );
        }


       
      

        // PUT: api/Courses/1
        [HttpPut("{id:int}")]
        public IActionResult EditCourse(int id, CourseEditDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = db.Courses.Find(id);

            if (course == null)
                return NotFound(new
                {
                    message = "Course not found"
                });

            course.CourseName = courseDto.CourseName;
            course.Credits = courseDto.Credits;
            course.Description = courseDto.Description;
            course.Department = courseDto.Department;
            course.Instructor = courseDto.Instructor;
            course.Semester = courseDto.Semester;
            course.MaxEnrollment = courseDto.MaxEnrollment;

            db.SaveChanges();

            return Ok(new
            {
                message = "Course updated successfully",
                courseId = course.CourseID
            });
        }


        // DELETE: api/Courses/1
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Course ID.");

            var course = db.Courses.Find(id);

            if (course == null)
                return NotFound();

            db.Courses.Remove(course);
            db.SaveChanges();

            return NoContent();
        }
    }
}
