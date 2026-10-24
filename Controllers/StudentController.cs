using Day1.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Day1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        SchoolContext db;
        public StudentController(SchoolContext db)
        {
            this.db = db;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var students = db.Students;
            if (students is null) return NoContent();
            return Ok(students);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var student = db.Students.FirstOrDefault(x => x.StudentID == id);
            if (student is null) return NotFound();
            return Ok(student);
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string firstName)
        {
            var student = db.Students.FirstOrDefault(s => s.FirstName == firstName);

            if (student is null) return NotFound();
            else return Ok(student);
        }

        [HttpPost]
        public IActionResult Add(Student s)
        {
            if (s == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            db.Students.Add(s);
            db.SaveChanges();
            return Created("Add Successfuly", s);
        }


        //This tells Entity Framework Core to treat the entire Student object as modified.

        //That can work, but there are some risks:
        //- Student doesn't exist: EF Core may attempt an update that affects zero rows without your code returning 404 Not Found.
        //- Unwanted changes: Every mapped property may be marked as modified, potentially overwriting existing values with defaults.
        //- Enrollments: Related enrollment records are not automatically updated just because you mark the student as modified.
        //The corrected version loads the existing student first and updates its fields explicitly.
        //[HttpPut("{id:int}")]
        //public IActionResult Edit(Student s, int id)
        //{
        //    if (s == null) return BadRequest();
        //    if (s.StudentID != id || id <= 0) return BadRequest();
        //    if (!ModelState.IsValid) return BadRequest();
        //    ///this line used for update an object of student in db
        //    /// db.Student.update(s);
        //    db.Entry(s).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    db.SaveChanges();
        //    return NoContent();

        //}


        [HttpPut("{id:int}")]
        public IActionResult Edit(Student s,int id)
        {
            if (id <= 0) return BadRequest("Invalid student ID.");

            if (s == null) return BadRequest();
            if (s.StudentID != id) return BadRequest("ID in URL does not match StudentID.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = db.Students.FirstOrDefault(x => x.StudentID == id);
            if (student == null) return NotFound();

            student.FirstName = s.FirstName;
            student.LastName = s.LastName;
            student.Age = s.Age;
            student.DateOfBirth = s.DateOfBirth;
            student.Email = s.Email;
            student.PhoneNumber = s.PhoneNumber;
            student.Address = s.Address;
            student.EnrollmentDate = s.EnrollmentDate;

            db.SaveChanges();
            return NoContent();



        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Student ID.");

            var s = db.Students.Find(id);

            if (s == null)
                return NotFound();

            db.Students.Remove(s);
            db.SaveChanges();

            return NoContent();
        }
    }
}