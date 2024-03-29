namespace TestUmkConstructor.Data.Models
{
    public class SimpleJoinType
    {
        public int Id1 { get; set; }
        public int Id2 { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class BRoleCurriculumJoin
    {
        public int BusinessRoleId { get; set; }
        public int SemesterId { get; set; }
        public int CurriculumId { get; set; }

        public BusinessRole BusinessRole { get; set; } = null!;
        public Semester Semester { get; set; } = null!;
        public Curriculum Curriculum { get; set; } = null!;
    }

    public class DisciplineCurriculumJoin
    {
        public int BRoleCurriculumJoinId { get; set; }
        public int DisciplineId { get; set; }

        public BRoleCurriculumJoin BRoleCurriculumJoin { get; } = null!;
        public Discipline Discipline { get; set; } = null!;

        // добавить ссылку на рабочую программу
    }

    public class SectionThemeJoin
    {
        public int SectionId { get; }
        public int ThemeId { get; }
        public int ThemeOrderNumber { get; }

        public Section Section { get; set; } = null!;
        public Theme Theme { get; set; } = null!;
    }
}
