using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SchoolSystemBE.Models;
using System.Runtime.InteropServices;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet <StudentParent> StudentParents { get; set; }
    public DbSet<TemporaryLogin> TemporaryLogins { get; set; }
    public DbSet<Extracurricular> Extracurriculars { get; set; }
    public DbSet<EcEnrollment> EcEnrollments { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<TeacherClass> TeacherClasses { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

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
            .HasMaxLength(255); 

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();    

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .HasMaxLength(64); 

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .IsRequired();

        //---------------------------------------------------------PARENT

        modelBuilder.Entity<Parent>()
          .HasKey(p => p.Id); 

        modelBuilder.Entity<Parent>()
            .Property(p => p.Id)
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

        //----------------------------------------------------TEMPORARY LOGIN
        modelBuilder.Entity<TemporaryLogin>()
            .HasKey(tl => tl.Id); 


        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.UserId)
            .IsRequired(); 

        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.Username)
            .IsRequired()
            .HasMaxLength(255); 

        modelBuilder.Entity<TemporaryLogin>()
            .HasIndex(tl => tl.Username)
            .IsUnique();  

        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.PasswordHash)
            .IsRequired()
            .HasMaxLength(64); 

        modelBuilder.Entity<TemporaryLogin>()
            .Property(tl => tl.Status)
            .HasDefaultValue(true);


        //-------------------------------------------------------EXTRACURRICULAR

        modelBuilder.Entity<Extracurricular>()
            .HasKey(ex => ex.Id);   

        modelBuilder.Entity<Extracurricular>()
            .Property(ex => ex.Id)
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

        //---------------------------------------------------ECENROLLMENT

        modelBuilder.Entity<EcEnrollment>()
          .HasKey(enr => enr.Id); 

        modelBuilder.Entity<EcEnrollment>()
            .Property(enr => enr.Id)
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

        //---------------------------------------------------TEACHER

        modelBuilder.Entity<Teacher>()
          .HasKey(t => t.Id);

        modelBuilder.Entity<Teacher>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Teacher>()
            .Property(t => t.FirstName)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Teacher>()
            .Property(t => t.LastName)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Teacher>()
            .Property(t => t.Email)
            .HasMaxLength(255);

        modelBuilder.Entity<Teacher>()
            .Property(t => t.Phone)
            .HasMaxLength(12);

        modelBuilder.Entity<Teacher>()
            .Property(t => t.UserId)
            .IsRequired();

        modelBuilder.Entity<Teacher>()
            .Property(t => t.Status)
            .HasDefaultValue(true);

        //---------------------------------------------------CLASSROOM
        modelBuilder.Entity<Classroom>()
          .HasKey(c => c.Id);

        modelBuilder.Entity<Classroom>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Classroom>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(2);

        modelBuilder.Entity<Classroom>()
            .Property(c => c.SchoolYear)
            .IsRequired()
            .HasMaxLength(9);

        //---------------------------------------------------------TEACHER CLASS

        modelBuilder.Entity<TeacherClass>()
          .HasKey(tc => tc.Id);

        modelBuilder.Entity<TeacherClass>()
            .Property(tc => tc.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TeacherClass>()
            .Property(tc => tc.TeacherId)
            .IsRequired();

        modelBuilder.Entity<TeacherClass>()
            .Property(tc => tc.ClassId)
            .IsRequired();

        modelBuilder.Entity<TeacherClass>()
            .Property(tc => tc.Subject)
            .IsRequired()
            .HasMaxLength(255);

        //------------------------------------------------STUDENT CLASS
        modelBuilder.Entity<StudentClass>()
        .HasKey(sc => new { sc.StudentId, sc.ClassId }); // PK sudaro kombinacija

        //-----------------------------------------------------------RELATIONSHIPS

        modelBuilder.Entity<TemporaryLogin>() //temporary login + user
            .HasOne(t => t.User) 
            .WithOne(u => u.TemporaryLogin) 
            .HasForeignKey<TemporaryLogin>(t => t.UserId) 
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<Student>() //user + student
            .HasOne(t => t.User) 
            .WithOne(u => u.Student) 
            .HasForeignKey<Student>(t => t.UserId);

        modelBuilder.Entity<Parent>()
            .HasOne(t => t.User) //parent + user
            .WithOne(u => u.Parent) 
            .HasForeignKey<Parent>(t => t.UserId);

        modelBuilder.Entity<StudentParent>() //student + studentparents
            .HasOne(sp => sp.Student)
            .WithMany(s => s.StudentParents)
            .HasForeignKey(sp => sp.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentParent>() //parent + studentparents
            .HasOne(sp => sp.Parent)
            .WithMany(p => p.StudentParents) 
            .HasForeignKey(sp => sp.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EcEnrollment>() //student + ecenrollments
            .HasOne(sp => sp.Student)  
            .WithMany(p => p.EcEnrollments)
            .HasForeignKey(sp => sp.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EcEnrollment>() //extracurricular + ecenrollments
            .HasOne(sp => sp.Extracurricular) 
            .WithMany(p => p.EcEnrollments)
            .HasForeignKey(sp => sp.EcId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Teacher>() //teacher + user
            .HasOne(t => t.User)
            .WithOne(u => u.Teacher)
            .HasForeignKey<Teacher>(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Classroom>() // class + teacher
            .HasOne(c => c.Teacher)  
            .WithOne(t => t.Classroom) 
            .HasForeignKey<Classroom>(c => c.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeacherClass>() //teacher + teacherclass
            .HasOne(tc => tc.Teacher)
            .WithMany(t => t.TeacherClasses)
            .HasForeignKey(tc => tc.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TeacherClass>() //class + teacherclass
            .HasOne(tc => tc.Classroom)
            .WithMany(c => c.TeacherClasses)
            .HasForeignKey(tc => tc.ClassId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentClass>() //student + studentclass
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentClasses)
            .HasForeignKey(sc => sc.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentClass>() //class + studentclass
            .HasOne(sc => sc.Classroom)
            .WithMany(c => c.StudentClasses)
            .HasForeignKey(sc => sc.ClassId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
