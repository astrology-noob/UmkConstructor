using static TestUmkConstructor.FileGeneration.FileStyling;

namespace TestUmkConstructor.FileGeneration
{
    public static class FileGeneratingTools
    {
        // TO DO: не забыть прописать логику определения цвета ячейки
        // + хранить используемые не системные цвета
        public class cellBackgroundColor
        {
            public string HexColor;
            // найти как хранить RGB
        }

        // названия ролей диапазонов ячеек, чтобы контролировать все созданные роли
        public class cellRangeRoleName
        {
            private static readonly Dictionary<string, cellRangeRoleName> createdNames = [];

            private cellRangeRoleName(string name)
            {
                Name = name;
            }

            public static cellRangeRoleName Create(string name)
            {
                if (createdNames.ContainsKey(name))
                    return createdNames[name];
                var newName = new cellRangeRoleName(name);
                createdNames.Add(name, newName);
                return newName;
            }

            public string Name { get; private set; }

            public static implicit operator cellRangeRoleName(string name) => Create(name);
        }

        // роль диапазона ячеек
        // TO DO: скорее всего придётся изменить просто на cellRole
        public class cellRangeRole
        {
            public cellRangeRoleName Name;
            public List<TextProperty> TextProperties;
            public string? BackgroundColor;

            public cellRangeRole(string name, IEnumerable<TextProperty> textProperties, string backgroundColor)
            {
                Name = name;
                TextProperties = [.. textProperties];
                BackgroundColor = backgroundColor;
            }

            public cellRangeRole(string name, IEnumerable<TextProperty> textProperties)
            {
                Name = name;
                TextProperties = [.. textProperties];
            }
        }

        // создание словаря с названиями ролей и ролями
        public static Dictionary<cellRangeRoleName, cellRangeRole> DefinecellRangeRoles(IEnumerable<cellRangeRole> roles)
        {
            var result = new Dictionary<cellRangeRoleName, cellRangeRole>();
            foreach (var role in roles)
            {
                result.Add(role.Name, role);
            }
            return result;
        }

        // MAYBE: перегрузить оператор индексирования, чтобы индексировать по названиям ролей
        // + мне не нравится как выглядит объявление списка проперти
        public static readonly Dictionary<cellRangeRoleName, cellRangeRole> definedcellRangeRoles = DefinecellRangeRoles(
            roles: [
                new("Default", new List<TextProperty>(){ TextSize.Medium, HorizontalAlignment.Left }),
                new("ATSpecialty", new List<TextProperty>(){ TextSize.Medium, HorizontalAlignment.Right }),
                new("ATHeader", new List<TextProperty>(){ TextSize.Biggest, TextStyle.Bold, HorizontalAlignment.Center}),
                new("ATSectionHeading", new List<TextProperty>(){ TextSize.Medium, TextStyle.Bold }),
                new("ATSection", new List<TextProperty>(){ TextSize.Medium }),
                new("KTPTopColumnHeaderDocumentName", new List<TextProperty>(){ TextSize.Big }),
                new("KTPRowColumnNumber", new List<TextProperty>(){ HorizontalAlignment.Center, VerticalAlignment.Center, TextSize.Big }),
                new("cell", new List<TextProperty>(){ HorizontalAlignment.Center, VerticalAlignment.Center, TextSize.Big }),
                new("KTPSectionName", new List<TextProperty>(){ TextSize.Big }),
                new("KTPSectionSum", new List<TextProperty>(){ TextSize.Big }),
            ]);
    }
}
