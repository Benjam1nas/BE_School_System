using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystemBE.Models;

[Route("api/[controller]")]
[ApiController]
public class EcEnrollmentController : ControllerBase
{
    private readonly SchoolDbContext _context;
    public EcEnrollmentController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<EcEnrollment>> CreateEnrollment(EcEnrollment ecEnrollment)
    {

        _context.EcEnrollments.Add(ecEnrollment);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{studentId}")]
    public async Task<ActionResult<IEnumerable<EcEnrollment>>> GetEcEnrollmentsForStudent(int studentId)
    {
        var enrollments = await _context.EcEnrollments
                                         .Where(e => e.StudentId == studentId)
                                         .ToListAsync();

        return Ok(enrollments ?? new List<EcEnrollment>());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EcEnrollment>>> GetAllEnrollments()
    {
        return await _context.EcEnrollments.ToListAsync();
    }


    [ApiExplorerSettings(IgnoreApi = true)]  // Ignore this method in Swagger
    public async Task<string> GetClassroomNameByStudentIdAsync(int studentId)
    {
        var classroomName = await _context.StudentClasses
                                   .Where(sc => sc.StudentId == studentId)
                                   .Join(_context.Classrooms,
                                         sc => sc.ClassId,
                                         c => c.Id,
                                         (sc, c) => c.Name)
                                   .FirstOrDefaultAsync();

        return classroomName ?? "No Classroom Assigned";  // If no classroom is found, return this.
    }


    [HttpGet("students/{ecId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetEnrolledStudents(int ecId)
    {
        // Fetch the students first
        var students = await _context.EcEnrollments
            .Where(e => e.Extracurricular.Id == ecId)
            .Join(_context.Students,
                  enrollment => enrollment.StudentId,
                  student => student.Id,
                  (enrollment, student) => new
                  {
                      student.FirstName,
                      student.LastName,
                      enrollment.EnrollmentDate,
                      student.Id // Include student ID for classroom lookup
                  })
            .ToListAsync();

        if (students == null || students.Count == 0)
        {
            return NotFound("No students are enrolled in this extracurricular activity.");
        }

        // Prepare the student list and fetch classroom names for each student asynchronously
        var studentList = new List<object>();

        foreach (var student in students)
        {
            // Fetch the classroom name for the student
            var classroomName = await GetClassroomNameByStudentIdAsync(student.Id);

            studentList.Add(new
            {
                student.FirstName,
                student.LastName,
                EnrollmentDate = student.EnrollmentDate.ToString("yyyy-MM-dd"), // Format the enrollment date
                ClassroomName = classroomName ?? "N/A" // If no classroom is found, display "N/A"
            });
        }

        return Ok(studentList);
    }
}
