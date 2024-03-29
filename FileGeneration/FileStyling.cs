using OfficeOpenXml.Style;

namespace TestUmkConstructor.FileGeneration
{
    public static class FileStyling
    {
        public abstract class TextProperty { }

        public abstract class TextSize : TextProperty
        {
            protected int _fontSize;
            protected TextSize(int fontSize) => _fontSize = fontSize;
            public int FontSize => _fontSize;

            public readonly static TextSize Biggest = new BiggestSize(18);
            public readonly static TextSize Big = new BigSize(14);
            public readonly static TextSize Medium = new MediumSize(12);
            public readonly static TextSize Small = new SmallSize(11);
            public readonly static TextSize Smallest = new SmallestSize(10);

            private sealed class BiggestSize(int fontSize) : TextSize(fontSize) { }
            private sealed class BigSize(int fontSize) : TextSize(fontSize) { }
            private sealed class MediumSize(int fontSize) : TextSize(fontSize) { }
            private sealed class SmallSize(int fontSize) : TextSize(fontSize) { }
            private sealed class SmallestSize(int fontSize) : TextSize(fontSize) { }
        }

        public abstract class TextStyle : TextProperty
        {
            protected string _style;
            protected TextStyle(string style) => _style = style;
            public string Style => _style;

            public readonly static TextStyle Bold = new BoldStyle("Bold");
            public readonly static TextStyle Italic = new ItalicStyle("Italic");
            public readonly static TextStyle Underlined = new UnderlinedStyle("Underlined");
            public readonly static TextStyle DoubleUnderlined = new DoubleUnderlinedStyle("DoubleUnderlined");
            public readonly static TextStyle Crossed = new CrossedStyle("Strike");

            private sealed class BoldStyle(string style) : TextStyle(style) { }
            private sealed class ItalicStyle(string style) : TextStyle(style) { }
            private sealed class UnderlinedStyle(string style) : TextStyle(style) { }
            private sealed class DoubleUnderlinedStyle(string style) : TextStyle(style) { }
            private sealed class CrossedStyle(string style) : TextStyle(style) { }
        }

        public abstract class VerticalAlignment : TextProperty
        {
            protected ExcelVerticalAlignment _placement;
            protected VerticalAlignment(ExcelVerticalAlignment placement) => _placement = placement;
            public ExcelVerticalAlignment Placement => _placement;

            public readonly static VerticalAlignment Top = new TopVerticalAlign(ExcelVerticalAlignment.Top);
            public readonly static VerticalAlignment Center = new CenterVerticalAlign(ExcelVerticalAlignment.Center);
            public readonly static VerticalAlignment Bottom = new BottomVerticalAlign(ExcelVerticalAlignment.Bottom);

            private sealed class TopVerticalAlign(ExcelVerticalAlignment placement) : VerticalAlignment(placement) { }
            private sealed class CenterVerticalAlign(ExcelVerticalAlignment placement) : VerticalAlignment(placement) { }
            private sealed class BottomVerticalAlign(ExcelVerticalAlignment placement) : VerticalAlignment(placement) { }
        }

        public abstract class HorizontalAlignment : TextProperty
        {
            protected ExcelHorizontalAlignment _placement;
            protected HorizontalAlignment(ExcelHorizontalAlignment placement) => _placement = placement;
            public ExcelHorizontalAlignment Placement => _placement;

            public readonly static HorizontalAlignment Left = new LeftHorizontalAlign(ExcelHorizontalAlignment.Left);
            public readonly static HorizontalAlignment Center = new CenterHorizontalAlign(ExcelHorizontalAlignment.Center);
            public readonly static HorizontalAlignment Right = new RightHorizontalAlign(ExcelHorizontalAlignment.Right);

            private sealed class LeftHorizontalAlign(ExcelHorizontalAlignment placement) : HorizontalAlignment(placement) { }
            private sealed class CenterHorizontalAlign(ExcelHorizontalAlignment placement) : HorizontalAlignment(placement) { }
            private sealed class RightHorizontalAlign(ExcelHorizontalAlignment placement) : HorizontalAlignment(placement) { }
        }
    }
}
