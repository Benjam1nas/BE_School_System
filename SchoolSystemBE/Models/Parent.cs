namespace SchoolSystemBE.Models
{
    public class Parent
    {
        public int ParentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }

        public ICollection<StudentParent> StudentParents { get; set; }
    }


    public class StudentParent
    {
        public int StudentId { set; get; }
        public int ParentId { get; set; }

        public Student Student { get; set; }
        public Parent Parent { get; set; }
    }
}