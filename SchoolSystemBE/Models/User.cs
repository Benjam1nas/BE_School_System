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
        public string Username { get; set; }
        public string Password { get; set; }
        public RolesTipas Role { get; set; }

    }
}
