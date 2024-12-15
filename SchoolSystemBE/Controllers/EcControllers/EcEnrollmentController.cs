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
}
