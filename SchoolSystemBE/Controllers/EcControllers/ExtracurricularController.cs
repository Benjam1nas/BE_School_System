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
        var extracurriculars = await _context.Extracurriculars.ToListAsync();

        // Map the enum values to their string equivalents after fetching data
        var result = extracurriculars.Select(ex => new ExtracurricularDto
        {
            Id = ex.Id,
            Name = ex.Name,
            Description = ex.Description,
            Coordinator = ex.Coordinator,
            Spots = ex.Spots,
            StartDate = ex.StartDate,
            EndDate = ex.EndDate,
            // Map enum values to string after fetching the data
            Category = Enum.GetName(typeof(Kategorijos), ex.Category),
            Price = ex.Price,
            Type = Enum.GetName(typeof(Tipai), ex.Type)
        }).ToList();

        return result;
    }
}
