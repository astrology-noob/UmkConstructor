using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Xml;
using static TestUmkConstructor.FileGeneration.FileStyling;
using static TestUmkConstructor.FileGeneration.TemplateReader;
using static TestUmkConstructor.FileGeneration.FileGeneratingTools;

namespace TestUmkConstructor.FileGeneration
{
    public class FileBuilder
    {
        private ExcelPackage _package;
        private int _curRow;
        private int _curColumn;

        public FileBuilder()
        {
            _package = new ExcelPackage("template/Template.xlsx");
            _curRow = 0;
            _curColumn = 0;
        }

        public byte[] CreateFile()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            AddWorksheet("Annotation");
            ChangeFontInWorkbook();
            TestTextStyleInWorkbook();

            using var ms = new MemoryStream();
            _package.SaveAs(ms);
            ms.Seek(0, SeekOrigin.Begin);

            return ms.ToArray();
        }

        private void MoveToNextRow() => _curRow++;

        private void SetCurCell(int column, int row)
        {
            _curColumn = column;
            _curRow = row;
        }

        private ExcelWorksheet AddWorksheet(string name)
        {
            XmlNode template = getXmlTemplate(name);
            // пиздец просто нужно всё переписывать
            return _package.Workbook.Worksheets.Add(template.Attributes?["sheetName"]?.ToString());
        }

        //    excelStyle.Fill.SetBackground(Color.Fuchsia);

        private static void AssignTextStyleToRange(ExcelRange range, cellRangeRole role)
        {
            var rangeTextSettings = range.Style;
            foreach (TextProperty textProperty in role.TextProperties)
            {
                if (textProperty is TextSize textSize)
                {
                    rangeTextSettings.Font.Size = textSize.FontSize;
                }
                else if (textProperty is TextStyle style)
                {
                    switch (style.Style)
                    {
                        case "Bold": { rangeTextSettings.Font.Bold = true; break; }
                        case "Italic": { rangeTextSettings.Font.Italic = true; break; }
                        case "Underlined": { rangeTextSettings.Font.UnderLine = true; break; }
                        case "DoubleUnderlined": { rangeTextSettings.Font.UnderLineType = ExcelUnderLineType.Double; break; }
                        case "Strike": { rangeTextSettings.Font.Strike = true; break; }
                        default: { throw new NotImplementedException(); }
                    }
                }
                else if (textProperty is VerticalAlignment verticalPlacement)
                {
                    rangeTextSettings.VerticalAlignment = verticalPlacement.Placement;
                }
                else if (textProperty is HorizontalAlignment horizontalPlacement)
                {
                    rangeTextSettings.HorizontalAlignment = horizontalPlacement.Placement;
                }
            }
        }

        private void TestTextStyleInWorkbook()
        {
            var worksheet = _package.Workbook.Worksheets[0];
            SetCurCell(1, 1);
            List<string> styles = ["ATHeader", "ATSpecialty", "ATSectionHeading", "ATSection", "KTPRowColumnNumber", "cell", "KTPSectionName", "KTPSectionSum"];
            foreach (var style in styles)
            {
                AssignTextStyleToRange(worksheet.Cells[_curRow, _curColumn], definedcellRangeRoles[style]);
                worksheet.Cells[_curRow, _curColumn].Value = "Lalala";
                MoveToNextRow();
            }
        }

        // применить общий шрифт для всех листов
        // TO DO: изменить на применение шрифта к листу перед тем как добавлять остальные стили (тк не все листы создаются с самого начала создания файла)
        private void ChangeFontInWorkbook()
        {
            var worksheets = _package.Workbook.Worksheets;
            foreach (ExcelWorksheet worksheet in worksheets)
            {
                worksheet.Cells[1, 1, 50, 50].Style.Font.SetFromFont("Times New Roman", 12);
            }
        }

        internal abstract class FileBuildingBlocks { }

        // TO DO: добавить свойство "ширина" чтобы при проверке давать предупреждение что по ширине темплейты не совпадают?
        internal abstract class Template : FileBuildingBlocks
        {
            // изменить, чтобы убрать строку
            public string Name;
            public List<FileBuildingBlocks> Blocks { get; set; }

            public Template(string name) => Name = name;

            public readonly static Template AnnotationHeader = new AnnotationHeaderTemplate("Шапка Аннотации");

            public readonly static Template AnnotationBlock = new AnnotationBlockTemplate("Блок Аннотации")
            {
                Blocks = new List<FileBuildingBlocks> { 
                    new Row() { 
                        cells = new List<cell>() 
                        {
                            new cell()
                            {
                                // TO DO: а для этого мне нужно сделать или оператор индексации по словарю с ролями
                                // или перенести всёёёё в БД
                            }
                        } 
                    } 
                },
            };

            public readonly static Template Annotation = new AnnotationTemplate("Аннотация") 
            {
                Blocks = new List<FileBuildingBlocks>() { AnnotationHeader, AnnotationBlock,  AnnotationBlock },
            };

            public readonly static Template WorkProgram = new WorkProgramTemplate("Рабочая программа");

            private sealed class AnnotationHeaderTemplate(string name) : Template(name) { }
            private sealed class AnnotationBlockTemplate(string name) : Template(name) { }
            private sealed class AnnotationTemplate(string name) : Template(name) { }
            private sealed class WorkProgramTemplate(string name) : Template(name) { }
        }

        internal class Row : FileBuildingBlocks 
        {
            public List<cell> cells { get; set; }
        }

        // КАК ОБЪЕДИНЯТЬ ЯЧЕЙКИ
        // ws.cells["A1:C1"].Merge = true;

        internal class cell : FileBuildingBlocks
        {
            public cellRangeRole cellRangeRole { get; set; }
            public string? DefaultContent;
        }

        //private void CreateAnnotationTemplate()
        //{
        //    var sheet = AddWorksheet("Аннотация");
        //    SetCurcell(1, 1);
        //    ExcelStyle excelStyle = sheet.cells[_curRow, _curColumn].Style;
        //}

        //private void CreateKTPTemplate()
        //{
        //    var sheet = AddWorksheet("Тематический план");
        //    SetCurcell(1, 1);
        //    ExcelStyle excelStyle = sheet.cells[_curRow, _curColumn, _curColumn, _curRow + 1].Style;
        //}

        //private void CreateProgramTemplate()
        //{
        //    var sheet = AddWorksheet("Рабочая программа");
        //    SetCurcell(1, 1);
        //    ExcelStyle excelStyle = sheet.cells[_curRow, _curColumn, _curColumn, _curRow + 1].Style;
        //}
    }
}