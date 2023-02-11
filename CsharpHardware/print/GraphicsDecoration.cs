namespace CsharpHardware.print
{
    public class GraphicsDecoration
    {
        public Graphics? Graphics { get; set; }
        public int LineCount { get; set; }

        public void DrawString(string s, Font font, Brush brush, PointF point)
        {
            LineCount++;
            Graphics?.DrawString(s, font, brush, point);
        }

        public int DrawStringWrap(string s, Font font, Brush brush, PointF point, ref int yShifting, int lineHeight, int lineCharCount)
        {
            var list = PrintUtil.CutString(s, lineCharCount);
            foreach (var t in list)
            {
                Graphics?.DrawString(t, font, brush, point);
                LineCount++;
                yShifting += lineHeight;
                point.Y = yShifting;
            }
            return yShifting;
        }

        public void FillRectangle(Brush brush, Rectangle rect)
        {
            LineCount++;
            Graphics?.FillRectangle(brush, rect);
        }

        public void DrawImage(Image image, PointF point)
        {
            Graphics?.DrawImage(image, point);
        }
    }
}