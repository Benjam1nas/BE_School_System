using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystemBE.Models;

[Route("api/[controller]")]
[ApiController]
public class ExtracurricularController : ControllerBase
{
    private readonly SchoolDbContext _context;
    public ExtracurricularController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExtracurricularDto>>> GetExtracurriculars()
    {
        // Fetch the data from the database first
        var extracurriculars = await _context.Extracurriculars
        .Select(ex => new
        {
            Extracurricular = ex,
            FilledSpots = _context.EcEnrollments.Count(e => e.EcId == ex.Id)
        })
        .ToListAsync();

        // Map the enum values to their string equivalents after fetching data
        var result = extracurriculars.Select(ex => new ExtracurricularDto
        {
            Id = ex.Extracurricular.Id,
            Name = ex.Extracurricular.Name,
            Description = ex.Extracurricular.Description,
            Coordinator = ex.Extracurricular.Coordinator,
            Spots = ex.Extracurricular.Spots,
            StartDate = ex.Extracurricular.StartDate,
            EndDate = ex.Extracurricular.EndDate,
            Category = Enum.GetName(typeof(Kategorijos), ex.Extracurricular.Category),
            Price = ex.Extracurricular.Price,
            Type = Enum.GetName(typeof(Tipai), ex.Extracurricular.Type),
            FilledSpots = ex.FilledSpots // New property
        }).ToList();

        return result;
    }
    
    [HttpPost]
    public async Task<ActionResult<ExtracurricularDto>> CreateExtracurricular([FromBody] ExtracurricularDto dto)
    {
        // Validate input
        if (dto == null)
        {
            return BadRequest("Extracurricular data is null.");
        }

        if (!Enum.TryParse<Kategorijos>(dto.Category, out var categoryEnum))
        {
            return BadRequest("Invalid Category value.");
        }

        if (!Enum.TryParse<Tipai>(dto.Type, out var typeEnum))
        {
            return BadRequest("Invalid Type value.");
        }

        // Map DTO to Model
        var extracurricular = new Extracurricular
        {
            Name = dto.Name,
            Description = dto.Description,
            Coordinator = dto.Coordinator,
            Spots = dto.Spots,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Category = categoryEnum,
            Price = dto.Price,
            Type = typeEnum
        };

        // Add and save to database
        _context.Extracurriculars.Add(extracurricular);
        await _context.SaveChangesAsync();

        // Return the created object with 201 status
        return CreatedAtAction(nameof(GetExtracurriculars), new { id = extracurricular.Id }, dto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditExtracurricular(int id, [FromBody] ExtracurricularDto dto)
    {
        if (id <= 0 || dto == null)
        {
            return BadRequest("Invalid request.");
        }

        var extracurricular = await _context.Extracurriculars.FindAsync(id);

        if (extracurricular == null)
        {
            return NotFound($"Extracurricular with ID {id} not found.");
        }

        if (!Enum.TryParse<Kategorijos>(dto.Category, out var categoryEnum))
        {
            return BadRequest("Invalid Category value.");
        }

        if (!Enum.TryParse<Tipai>(dto.Type, out var typeEnum))
        {
            return BadRequest("Invalid Type value.");
        }

        // Update the entity properties
        extracurricular.Name = dto.Name;
        extracurricular.Description = dto.Description;
        extracurricular.Coordinator = dto.Coordinator;
        extracurricular.Spots = dto.Spots;
        extracurricular.StartDate = dto.StartDate;
        extracurricular.EndDate = dto.EndDate;
        extracurricular.Category = categoryEnum;
        extracurricular.Price = dto.Price;
        extracurricular.Type = typeEnum;

        _context.Entry(extracurricular).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExtracurricular(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid ID.");
        }

        // Fetch the extracurricular along with related enrollments
        var extracurricular = await _context.Extracurriculars
            .Include(e => e.EcEnrollments) // Ensure enrollments are loaded
            .FirstOrDefaultAsync(e => e.Id == id);

        if (extracurricular == null)
        {
            return NotFound($"Extracurricular with ID {id} not found.");
        }

        // Remove related enrollments first
        _context.EcEnrollments.RemoveRange(extracurricular.EcEnrollments);

        // Remove the extracurricular
        _context.Extracurriculars.Remove(extracurricular);

        // Save changes to the database
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
