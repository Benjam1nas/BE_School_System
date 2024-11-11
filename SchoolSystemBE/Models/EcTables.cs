namespace SchoolSystemBE.Models
{
    public enum Kategorijos
    {
        menai,
        sportas
    }

    public enum Tipai
    {
        mokamas,
        nemokamas
    }

    public class Extracurricular
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Coordinator { get; set; }
        public int Spots { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Kategorijos Category { get; set; }
        public decimal? Price { get; set; }
        public Tipai Type { get; set; }

        public ICollection<EcEnrollment> EcEnrollments { get; set; }
    }

    public class EcEnrollment
    {
        public int Id { get; set; }
        public int EcId { get; set; }
        public int StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool Status { get; set; }

        public Student Student { get; set; }
        public Extracurricular Extracurricular { get; set; }
    }
}