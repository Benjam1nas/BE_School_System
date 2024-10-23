using System.ComponentModel.DataAnnotations;

namespace SchoolSystemBE.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? StudentIdNo { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }

        public ICollection<StudentParent> StudentParents { get; set; }
    }
}
