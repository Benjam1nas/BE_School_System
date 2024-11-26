using System.ComponentModel.DataAnnotations;

namespace SchoolSystemBE.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<TeacherClass> TeacherClasses { get; set; }
        public Classroom Classroom { get; set; }
    }

    public class Classroom
    {
        public int Id { get; set; }
        public int TeacherId { set; get; }
        public string Name { get; set; }
        public string SchoolYear { get; set; }

        public Teacher Teacher { get; set; }
        public ICollection<TeacherClass> TeacherClasses { get; set; }
        public ICollection<StudentClass> StudentClasses { get; set; }
    }

    public class TeacherClass
    {
        public int Id { get; set; }
        public int TeacherId { set; get; }
        public int ClassId { get; set; }
        public string Subject { get; set; }

        public Teacher Teacher { get; set; }
        public Classroom Classroom { get; set; }
    }

    public class StudentClass
    {
        public int ClassId { set; get; }
        public int StudentId { get; set; }

        public Student Student { get; set; }
        public Classroom Classroom { get; set; }
    }
}
