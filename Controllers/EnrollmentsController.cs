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

        /// <summary>
        /// Retrieves all enrollments from the database.
        /// </summary>
        /// <returns>
        /// Returns 200 OK with a list of all enrollments.
        /// </returns>
        /// <remarks>
        /// GET: api/Enrollments
        /// </remarks>
        [HttpGet]
        public IActionResult GetAll()
        {
            var enrollment = db.Enrollments.ToList();
            return Ok(enrollment);
        }

        /// <summary>
        /// Retrieves a specific enrollment by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the enrollment.</param>
        /// <returns>
        /// Returns 200 OK with the enrollment if found.
        /// Returns 400 Bad Request if the ID is invalid.
        /// Returns 404 Not Found if the enrollment does not exist.
        /// </returns>
        /// <remarks>
        /// GET: api/Enrollments/1
        ///  </remarks>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest("Invalid Enrollment ID");
            var enrollment = db.Enrollments.Find(id);
            if (enrollment == null) return NotFound();
            return Ok(enrollment);
        }



        /// <summary>
        /// Creates a new enrollment in the database.
        /// </summary>
        /// <param name="enrollment">
        /// The enrollment data, including StudentID, CourseID,
        /// EnrollmentDate, Grade, and Status.
        /// </param>
        /// <returns>
        /// Returns 201 Created with the newly created enrollment.
        /// Returns 400 Bad Request if the input data is invalid.
        /// </returns>
        /// <remarks>
        /// POST: api/Enrollments
        ///
        /// Example request:
        ///
        ///     {
        ///         "studentID": 1,
        ///         "courseID": 1,
        ///         "enrollmentDate": "2026-10-28",
        ///         "grade": "A",
        ///         "status": "Active"
        ///     }
        ///
        /// StudentID and CourseID must refer to existing records.
        /// </remarks>
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


        /// <summary>
        /// Updates an existing enrollment.
        /// </summary>
        /// <param name="id">The ID of the enrollment to update.</param>
        /// <param name="enrollment">The updated enrollment information.</param>
        /// <returns>
        /// Returns 204 No Content if the update succeeds.
        /// Returns 400 Bad Request if the ID or input data is invalid.
        /// Returns 404 Not Found if the enrollment does not exist.
        /// </returns>
        /// <remarks>
        /// PUT: api/Enrollments/1
        ///
        /// The ID in the URL must match EnrollmentID in the request body.
        /// StudentID and CourseID must refer to existing records.
        /// </remarks>

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

        /// <summary>
        /// Deletes an enrollment from the database.
        /// </summary>
        /// <param name="id">The unique ID of the enrollment to delete.</param>
        /// <returns>
        /// Returns 204 No Content if the enrollment is deleted successfully.
        /// Returns 400 Bad Request if the ID is invalid.
        /// Returns 404 Not Found if the enrollment does not exist.
        /// </returns>
        /// <remarks>
        /// DELETE: api/Enrollments/1
        /// </remarks>

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
