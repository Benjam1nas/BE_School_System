public class ExtracurricularDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Coordinator { get; set; }
    public int? Spots { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Category { get; set; }  // Category as string
    public decimal? Price { get; set; }
    public string Type { get; set; }  // Type as string
    public int FilledSpots { get; set; }
}

