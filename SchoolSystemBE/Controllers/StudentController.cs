using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystemBE.Models;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public StudentsController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetStudentId(int id)
    {
        var student = await _context.Students.Where(s => s.UserId == id).FirstOrDefaultAsync();

        if (student == null) 
        {
            return NotFound();
        }

        return Ok(student.Id);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStudent", new { id = student.Id }, student);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchStudent(int id, UpdateStudentDto updateStudentDto)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        if (updateStudentDto.FirstName != null)
        {
            student.FirstName = updateStudentDto.FirstName;
        }
        if (updateStudentDto.LastName != null)
        {
            student.LastName = updateStudentDto.LastName;
        }
        if (updateStudentDto.Email != null)
        {
            student.Email = updateStudentDto.Email;
        }
        if (updateStudentDto.DateOfBirth != null)
        {
            student.DateOfBirth = (DateTime)updateStudentDto.DateOfBirth;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
