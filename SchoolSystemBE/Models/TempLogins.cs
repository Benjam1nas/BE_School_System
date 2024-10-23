namespace SchoolSystemBE.Models
{
    public class TemporaryLogin
    {
        public int TLId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
    }
}