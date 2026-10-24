
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
        public IActionResult GetAll()
        {
            var courses = db.Courses.ToList();

            return Ok(courses);
        }

        // GET: api/Courses/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Course ID.");

            var course = db.Courses.Find(id);

            if (course == null)
                return NotFound();

            return Ok(course);
        }

        // POST: api/Courses
        [HttpPost]
        public IActionResult Add(Course course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Courses.Add(course);
            db.SaveChanges();

            return CreatedAtAction(
                nameof(GetById),
                new { id = course.CourseID },
                course
            );
        }

        // PUT: api/Courses/1
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Course course)
        {
            if (id <= 0)
                return BadRequest("Invalid Course ID.");

            if (course.CourseID != id)
                return BadRequest(
                    "URL ID does not match CourseID."
                );

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCourse = db.Courses.Find(id);

            if (existingCourse == null)
                return NotFound();

            existingCourse.CourseName = course.CourseName;
            existingCourse.Credits = course.Credits;
            existingCourse.Description = course.Description;
            existingCourse.Department = course.Department;
            existingCourse.Instructor = course.Instructor;
            existingCourse.Semester = course.Semester;
            existingCourse.MaxEnrollment = course.MaxEnrollment;

            db.SaveChanges();

            return NoContent();
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
