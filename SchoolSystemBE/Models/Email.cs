namespace SchoolSystemBE.Models
{
    public class Email
    {
        public int EmailId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int References { get; set; }
    }

    public class EmailDocument
    {
        public int DocId { get; set; }
        public int EmailId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int UploadedBy { get; set; }
    }
}