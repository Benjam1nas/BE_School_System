using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystemBE.Models;
using SchoolSystemBE.Contracts.ExtracurricularDto_s;

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

    [HttpGet("{extracurricularId}/EnrolledChildren")]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetEnrolledChildren(int extracurricularId)
    {
        var enrolledChildren = await _context.EcEnrollments
            .Where(e => e.EcId == extracurricularId && e.Status)
            .Include(e => e.Student)
            .Select(e => new StudentDto
            {
                Id = e.Student.Id,
                FirstName = e.Student.FirstName,
                LastName = e.Student.LastName
            })
            .ToListAsync();

        return Ok(enrolledChildren);
    }


    [HttpGet("Parents/{parentId}/Children")]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetParentChildren(int parentId)
    {
        var parent = await _context.Parents.Where(p => p.UserId == parentId).FirstOrDefaultAsync();

        if (parent == null)
        {
            return NotFound();
        }

        var children = await _context.StudentParents
            .Where(sp => sp.ParentId == parent.Id)
            .Include(sp => sp.Student)
            .Select(sp => new StudentDto
            {
                Id = sp.Student.Id,
                FirstName = sp.Student.FirstName,
                LastName = sp.Student.LastName
            })
            .ToListAsync();

        if (children == null)
        {
            return NotFound();
        }

        var result = children.Select(child => new StudentDto
        {
            Id = child.Id,
            FirstName = child.FirstName,
            LastName = child.LastName
        });

        return Ok(result);
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

    [HttpDelete]
    public async Task<ActionResult> DeleteEnrollment([FromQuery] int ecId, [FromQuery] int studentId)
    {
        var enrollment = await _context.EcEnrollments
                                   .FirstOrDefaultAsync(e => e.EcId == ecId && e.StudentId == studentId);

        if (enrollment == null)
        {
            return NotFound();
        }

        _context.EcEnrollments.Remove(enrollment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
