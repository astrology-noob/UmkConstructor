using System.ComponentModel.DataAnnotations;

namespace TestUmkConstructor.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Email { get; set; } = null!;
        // типа вместо пароля..?
        public string Token { get; set; } = null!;
    }

    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    // должны быть таблицы связей ролей и разрешений + пользователей и ролей

    // как нахуй с этим работать.... Может хранить какие у какой роли разрешения прямо в коде?
    public class Permissions
    {

    }
}
