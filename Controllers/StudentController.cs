using Day1.DTOs.Student;
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


        /// <summary>
        /// Get All Students Information
        /// </summary>
        /// <returns>
        /// List of Students
        /// </returns>
        /// <remarks>
        /// Request Example:
        /// /api/student
        /// </remarks>
        [HttpGet]
        public IActionResult GetStudents()
        {
        
            var students = db.Students.Select(
                s => new StudentReadDto
                {
                    ID = s.StudentID,
                    FullName = $"{s.FirstName} {s.LastName}",
                    Age = s.Age,
                    DateOfBirth = s.DateOfBirth,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    EnrollmentDate = s.EnrollmentDate
                }
                ).ToList();
            return Ok(students);
        }

        
        /// <summary>
        /// Retrieves a specific Student by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the Student to retrieve.</param>
        /// <returns>
        /// Returns 200 OK with the student details if found.
        /// Returns 404 Not Found if the student does not exist.
        /// </returns>
        [HttpGet("{id:int}")]
        public IActionResult GetStudentById(int id)
        {
            var student = db.Students.Find(id);
            if (student == null) return NotFound();
            else
            {
                StudentReadDto studentReadDto = new StudentReadDto()
                {
                    ID = student.StudentID,
                    FullName = $"{student.FirstName} {student.LastName}",
                    Age = student.Age,
                    DateOfBirth = student.DateOfBirth,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    Address = student.Address,
                    EnrollmentDate = student.EnrollmentDate

                };
                return Ok(studentReadDto);
            }
        }


        /// <summary>
        /// Get Student By Name
        /// </summary>
        /// <param name="firstName">FirstName</param>
        /// <returns>a Student info</returns>
        /// <remarks>
        /// Requist Example: api/student/GetByName?name=ali
        /// </remarks>
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string firstName)
        {
            var student = db.Students.FirstOrDefault(s => s.FirstName == firstName);

            if (student is null) return NotFound();
            else
            {
                StudentReadDto studentReadDto = new StudentReadDto()
                {
                    ID = student.StudentID,
                    FullName = $"{student.FirstName} {student.LastName}",
                    Age = student.Age,
                    DateOfBirth = student.DateOfBirth,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    Address = student.Address,
                    EnrollmentDate = student.EnrollmentDate

                };
                return Ok(studentReadDto);
            }
        }


        /// <summary>
        /// Creates a new student and saves the student information to the database.
        /// </summary>
        /// <param name="dto">
        /// The student creation data, including first name, last name, age,
        /// date of birth, email, phone number, and address.
        /// </param>
        /// <returns>
        /// Returns 200 OK with a success message and the ID of the created student.
        /// </returns>
        [HttpPost]
        public IActionResult CreateStudent(StudentCreateDto dto)
        {
            var student = new Student()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                EnrollmentDate = DateOnly.FromDateTime(DateTime.Today)
            };
            db.Students.Add(student);
            db.SaveChanges();
            return Ok(new
            {
                message = "Student created successfully",
                studentId = student.StudentID
            });

        }



        /// <summary>
        /// Updates the information of an existing student.
        /// </summary>
        /// <param name="dto">
        /// The updated student information, including first name, last name,
        /// age, date of birth, email, phone number, and address.
        /// </param>
        /// <param name="id">The unique ID of the student to update.</param>
        /// <returns>
        /// Returns 200 OK with a success message and student ID if the update succeeds.
        /// Returns 400 Bad Request if the student ID or request data is invalid.
        /// Returns 404 Not Found if the student does not exist.
        /// </returns>

        [HttpPut("{id:int}")]
        public IActionResult Edit(StudentEditDto dto,int id)
        {
            if (id <= 0) return BadRequest("Invalid student ID.");

            if (dto == null) return BadRequest();
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = db.Students.Find(id);
            if (student == null)
                return NotFound(new
                {
                    message = "Student Not Found"
                });
            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Age = dto.Age;
            student.DateOfBirth = dto.DateOfBirth;
            student.Email = dto.Email;
            student.PhoneNumber = dto.PhoneNumber;
            student.Address = dto.Address;

            db.SaveChanges();
            return Ok(new
            {
                message = "Student Update successfully",
                studentId = student.StudentID
            });

        }


        /// <summary>
        /// Deletes an existing student from the database.
        /// </summary>
        /// <param name="id">The unique ID of the student to delete.</param>
        /// <returns>
        /// Returns 204 No Content if the student is deleted successfully.
        /// Returns 400 Bad Request if the student ID is invalid.
        /// Returns 404 Not Found if the student does not exist.
        /// </returns>
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