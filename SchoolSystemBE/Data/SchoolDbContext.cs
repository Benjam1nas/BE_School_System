using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SchoolSystemBE.Models;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; }
    public DbSet<Grades> Grades { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Extracurricular> Extracurriculars { get; set; }
    public DbSet<EcEnrollment> EcEnrollments { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<EmailDocument> EmailDocuments { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<StudentParent> StudentsParents { get; set; }
    public DbSet<TemporaryLogin> TemporaryLogins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //-------------------------------------------------------EXTRACURRICULAR

        modelBuilder.Entity<Extracurricular>()
            .HasKey(ex => ex.EcId);

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.EcId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.Name)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.Description)
            .IsRequired();

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.Coordinator)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.Spots)
            .IsRequired();

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.StartDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.EndDate)
            .HasColumnType("DATETIME");

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.Category)
            .IsRequired();

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.Price)
            .HasColumnType("decimal(5,2)");

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.Type)
            .IsRequired();

        //---------------------------------------------------EcEnrollment

        modelBuilder.Entity<EcEnrollment>()
          .HasKey(enr => enr.EcEnrollId);

        modelBuilder.Entity<EcEnrollment>()
            .Property(enr => enr.EcEnrollId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<EcEnrollment>()
            .Property(enr => enr.EcId)
            .IsRequired();

        modelBuilder.Entity<EcEnrollment>()
            .Property(enr => enr.StudentId)
            .IsRequired();

        modelBuilder.Entity<EcEnrollment>()
            .Property(enr => enr.EnrollmentDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        modelBuilder.Entity<EcEnrollment>()
         .Property(enr => enr.Status)
         .HasDefaultValue(true);


        //-------------------------------------------------------------EMAIL
        modelBuilder.Entity<Email>()
         .HasKey(em => em.EmailId);

        modelBuilder.Entity<Email>()
            .Property(em => em.EmailId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Email>()
            .Property(em => em.SenderId)
            .IsRequired();

        modelBuilder.Entity<Email>()
            .Property(em => em.ReceiverId)
            .IsRequired();

        modelBuilder.Entity<Email>()
            .Property(em => em.Date)
            .IsRequired()
            .HasColumnType("DATETIME");

        //-----------------------------------------------------------EMAIL DOCUMENT
        modelBuilder.Entity<EmailDocument>()
         .HasKey(emd => emd.DocId);

        modelBuilder.Entity<EmailDocument>()
            .Property(emd => emd.DocId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<EmailDocument>()
            .Property(emd => emd.EmailId)
            .IsRequired();

        modelBuilder.Entity<EmailDocument>()
            .Property(emd => emd.Name)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<EmailDocument>()
            .Property(emd => emd.Url)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<EmailDocument>()
            .Property(emd => emd.UploadedBy)
            .IsRequired();


        //----------------------------------------------------TEMPORARY LOGIN
        modelBuilder.Entity<TemporaryLogin>()
            .HasKey(tl => tl.TLId); 


        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.TLId)
            .ValueGeneratedOnAdd(); 

        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.UserId)
            .IsRequired();   

        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.Username)
            .IsRequired() 
            .HasMaxLength(255); 

        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.PasswordHash)
            .IsRequired() 
            .HasMaxLength(64); 

        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.Status)
            .HasDefaultValue(true);

        //---------------------------------------------------------PARENT
      
        modelBuilder.Entity<Parent>()
          .HasKey(p => p.ParentId);

        modelBuilder.Entity<Parent>()
            .Property(p => p.ParentId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Parent>()
            .Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Parent>()
            .Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Parent>()
            .Property(p => p.Email)
            .HasMaxLength(255);

        modelBuilder.Entity<Parent>()
            .Property(p => p.Phone)
            .HasMaxLength(12);

        modelBuilder.Entity<Parent>()
            .Property(p => p.UserId)
            .IsRequired();

        //------------------------------------------------STUDENT PARENTS
        modelBuilder.Entity<StudentParent>()
       .HasKey(sp => new { sp.StudentId, sp.ParentId }); // PK sudaro kombinacija

        modelBuilder.Entity<StudentParent>()
            .HasOne(sp => sp.Student) // Relationship to Student
            .WithMany(s => s.StudentParents)
            .HasForeignKey(sp => sp.StudentId);

        modelBuilder.Entity<StudentParent>()
            .HasOne(sp => sp.Parent) // Relationship to Parent
            .WithMany(p => p.StudentParents)
            .HasForeignKey(sp => sp.ParentId);

        
        //----------------------------------------------------------------STUDENT
        modelBuilder.Entity<Student>()
           .HasKey(s => s.Id);

        modelBuilder.Entity<Student>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Student>()
            .Property(s => s.FirstName)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Student>()
            .Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Student>()
            .Property(s => s.DateOfBirth)
            .HasColumnType("DATE");

        modelBuilder.Entity<Student>()
            .Property(s => s.Email)
            .HasMaxLength(255);

        modelBuilder.Entity<Student>()
            .Property(s => s.Phone)
            .HasMaxLength(12);

        modelBuilder.Entity<Student>()
            .Property(s => s.StudentIdNo)
            .HasMaxLength(20);

        modelBuilder.Entity<Student>()
            .Property(s => s.Status)
            .HasDefaultValue(true);

        modelBuilder.Entity<Student>()
            .Property(s => s.UserId)
            .IsRequired();

        //---------------------------------------------------------USER
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd(); 

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired() 
            .HasMaxLength(255); 

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique(); 

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired() 
            .HasMaxLength(64); 

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .IsRequired(); 
    }
}
