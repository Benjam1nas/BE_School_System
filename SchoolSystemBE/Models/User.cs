using System.ComponentModel;

namespace SchoolSystemBE.Models
{
    public enum RolesTipas
    {
        teacher,
        student,
        parent,
        administration
    }
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public RolesTipas Role { get; set; }

        public TemporaryLogin TemporaryLogin { get; set; }
        public Student Student { get; set; }
        public Parent Parent { get; set; }
        public Teacher Teacher { get; set; }
    }
}
