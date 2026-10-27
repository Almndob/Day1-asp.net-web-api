
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
        /// <summary>
        /// Retrieves all courses from the database.
        /// </summary>
        /// <returns>
        /// Returns 200 OK with a list of courses.
        /// </returns>
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
        /// <summary>
        /// Retrieves a specific course by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the course to retrieve.</param>
        /// <returns>
        /// Returns 200 OK with the course details if found.
        /// Returns 400 Bad Request if the course ID is invalid.
        /// Returns 404 Not Found if the course does not exist.
        /// </returns>

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


        /// <summary>
        /// Creates a new course in the database.
        /// </summary>
        /// <param name="courseDto">
        /// The course creation data, including course name, credits,
        /// description, department, instructor, semester, and maximum enrollment.
        /// </param>
        /// <returns>
        /// Returns 201 Created with the newly created course and its location.
        /// Returns 400 Bad Request if the input data is invalid.
        /// </returns>
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





        /// <summary>
        /// Updates the details of an existing course.
        /// </summary>
        /// <param name="id">The unique ID of the course to update.</param>
        /// <param name="courseDto">The updated course information.</param>
        /// <returns>
        /// Returns 200 OK if the course is updated successfully.
        /// Returns 400 Bad Request if the input data is invalid.
        /// Returns 404 Not Found if the course does not exist.
        /// </returns>
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


        /// <summary>
        /// Deletes a course from the database.
        /// </summary>
        /// <param name="id">The unique ID of the course to delete.</param>
        /// <returns>
        /// Returns 204 No Content if the course is deleted successfully.
        /// Returns 400 Bad Request if the course ID is invalid.
        /// Returns 404 Not Found if the course does not exist.
        /// </returns>
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
