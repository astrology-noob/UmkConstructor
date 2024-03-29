using System.ComponentModel.DataAnnotations;

namespace TestUmkConstructor.Data.Models
{
    // может исправить на = null!;
    public class Discipline(string code, string name, int hours)
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } = code;
        public string Name { get; set; } = name;
        public int Hours { get; set; } = hours;
        //public ICollection<Book> Books { get; set; }
    }

    public class Theme
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Hours { get; set; }
        public string ContentOfEducationMaterial { get; set; } = null!;

        // один-ко-многим
        public int ThemeTypeId { get; set; }
        public ThemeType ThemeType { get; set; } = null!;

        // многие-ко-многим
        public List<SoftSkill> SoftSkills { get; set; } = null!;
    }

    public class ThemeType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class SoftSkill
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        // многие-ко-многим
        public List<Theme> Themes { get; set; } = null!;
    }

    public class Section
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Theme> Themes { get; set; } = [];
    }

    public class ControlPoint
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Points { get; set; }
        public string ShortDescription { get; set; } = null!;
        public string LinkToLongDescription { get; set; } = null!;
        public ControlPointType ControlPointType { get; set; } = null!;

        public List<EvaluationCriteria> evaluationCriteria { get; set; } = [];
    }

    public class ControlPointType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class EvaluationCriteria
    {
        [Key]
        public int Id { get; set; }
        public int Points { get; set; }
        public string Description { get; set; } = null!;

        // а точно тут подойдёт один-ко-многим?
        public int ControlPointId { get; }
        public ControlPoint ControlPoint { get; } = null!;
    }
}
