using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestUmkConstructor.Data.Models;

namespace TestUmkConstructor.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Specialty> Specialties { get; set; } = null!;
        public DbSet<BusinessRole> BusinessRoles { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Organization> Organizations { get; set; } = null!;
        public DbSet<AcademicYear> AcademicYears { get; set; } = null!;
        public DbSet<Curriculum> Curricula { get; set; } = null!;
        public DbSet<Semester> Semesters { get; set; } = null!;
        public DbSet<SemesterType> SemesterTypes { get; set; } = null!;
        public DbSet<StudyYear> StudyYears { get; set; } = null!;
        
        //public DbSet<Discipline> Disciplines { get; set; } = null!;
        //public DbSet<Theme> Themes { get; set; } = null!;
        //public DbSet<ThemeType> ThemeTypes { get; set; } = null!;
        //public DbSet<SoftSkill> SoftSkills { get; set; } = null!;
        //public DbSet<Section> Sections { get; set; } = null!;
        //public DbSet<ControlPoint> ControlPoints { get; set; } = null!;
        //public DbSet<ControlPointType> ControlPointTypes { get; set; } = null!;
        //public DbSet<EvaluationCriteria> EvaluationCriterias { get; set; } = null!;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}