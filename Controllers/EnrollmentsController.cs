using Day1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        SchoolContext db;
        public EnrollmentsController(SchoolContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var enrollment = db.Enrollments.ToList();
            return Ok(enrollment);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest("Invalid Enrollment ID");
            var enrollment = db.Enrollments.Find(id);
            if (enrollment == null) return NotFound();
            return Ok(enrollment);
        }

        [HttpPost]
        public IActionResult Add(Enrollment enrollment)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if (enrollment == null) return BadRequest();
            db.Enrollments.Add(enrollment);
            db.SaveChanges();

            return CreatedAtAction(nameof(GetById),
                new { id = enrollment.EnrollmentID },
                enrollment
                );
        }


        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Enrollment enrollment)
        {
            if (id <= 0)
                return BadRequest("Invalid Enrollment ID.");

            if (enrollment == null)
                return BadRequest();

            if (enrollment.EnrollmentID != id)
                return BadRequest("URL ID does not match EnrollmentID.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var e = db.Enrollments.Find(id);

            if (e == null)
                return NotFound();

            e.StudentID = enrollment.StudentID;
            e.CourseID = enrollment.CourseID;
            e.EnrollmentDate = enrollment.EnrollmentDate;
            e.Grade = enrollment.Grade;
            e.Status = enrollment.Status;

            db.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Enrollment ID.");

            var enrollment = db.Enrollments.Find(id);

            if (enrollment == null)
                return NotFound();

            db.Enrollments.Remove(enrollment);
            db.SaveChanges();

            return NoContent();
        }



    }
}
