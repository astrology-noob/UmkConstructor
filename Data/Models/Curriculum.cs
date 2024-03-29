using System.ComponentModel.DataAnnotations;

namespace TestUmkConstructor.Data.Models
{
    // специальность
    public class Specialty(string name)
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = name;

        // один-ко-многим
        public List<BusinessRole> BusinessRoles { get; } = [];
    }

    /// <summary>
    /// фиксированный набор бизнес-ролей, они (не? - вообще, должны наверное, они не должны быть одни и те же у всех лет) создаются новые для каждого года
    /// </summary>
    /// <param name="name"></param>
    /// <param name="specialty"></param>
    public class BusinessRole(string name, Specialty specialty)
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = name;

        // один-ко-многим
        public int SpecialtyId;
        public Specialty Specialty { get; set; } = specialty;

        public List<BRoleCurriculumJoin> BRoleSemesterCurriculumJoins { get; set; } = null!;
    }

    /// <summary>
    /// Кафедра
    /// </summary>
    /// <param name="name"></param>
    public class Department(string name)
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = name;
    }

    /// <summary>
    /// Учебная организация
    /// </summary>
    /// <param name="name"></param>
    public class Organization(string name)
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = name;
    }

    // Учебный год
    public class AcademicYear
    {
        [Key]
        public int Id { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public List<Curriculum> Curricula { get; } = [];

        public AcademicYear(int start)
        {
            Start = start;
            End = start + 1;
        }

        public AcademicYear(int start, int end)
        {
            Start = start;
            End = end;
        }
    }

    // Учебный план
    public class Curriculum(AcademicYear academicYear, int weekCount, int hoursTotal, int individualWork)
    {
        [Key]
        public int Id { get; set; }
        public int WeekCount { get; set; } = weekCount;
        public int HoursTotal { get; set; } = hoursTotal;
        public int IndividualWork { get; set; } = individualWork;

        // один-ко-многим
        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; } = academicYear;

        // многие-ко-многим
        public List<Semester> Semesters { get; } = [];
        public List<SimpleJoinType> CurriculumSemester { get; } = [];

        public List<BRoleCurriculumJoin> BRoleSemesterCurriculumJoins { get; set; } = null!;
    }

    /// <summary>
    /// По факту это описание фиксированного количества семестров (1 семестр 1 курса после 9-го класса, 1 семестр 1 курса после 11-го класса и тд.)
    /// </summary>
    public class Semester
    {
        [Key]
        public int Id { get; }
        // как запрещать создавать экземпляры...
        public StudyYear StudyYear { get; } = null!;
        public SemesterType SemesterType { get; } = null!;

        public List<Curriculum> Curricula { get; } = [];
        public List<SimpleJoinType> CurriculumSemester { get; } = [];

        public List<BRoleCurriculumJoin> BRoleSemesterCurriculumJoins { get; set; } = null!;
    }

    // тип семестра ака осенний или весенний
    public class SemesterType(string name)
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = name;
    }

    // курс 1 2 и тд
    public class StudyYear(int order, bool isAfter11thGrade)
    {
        [Key]
        public int Id { get; set; }
        public int Order { get; set; } = order;
        public bool IsAfter11thGrade { get; set; } = isAfter11thGrade;
    }
}
