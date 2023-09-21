using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quiz.Repository.Configuration;
using Quiz.Repository.Model;

namespace Quiz.Repository
{
    public class QuizDbContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new MajorConfiguration());
            modelBuilder.ApplyConfiguration(new MajorSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new TestStructureConfiguration());
            modelBuilder.ApplyConfiguration(new TestSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new UserAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new UserTestConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins").HasKey(x => x.UserId);
            //modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");
            //modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserToken").HasKey(x => x.UserId);
            base.OnModelCreating(modelBuilder);
        }
        #region DbSet
        public DbSet<Department> Departments { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<MajorSubject> MajorSubjects { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TestStructure> TestStructures { get; set; }
        public DbSet<TestSubject> TestSubjects { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        //public DbSet<User> Users { get; set; }
        #endregion
    }
}
