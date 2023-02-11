using System.Drawing.Printing;
using System.Text.Json;

namespace CsharpHardware.print
{
    internal class SchemeLabelPrintImpl : LabelPrintBase
    {
        private readonly GraphicsDecoration graphicsDecoration = new();

        public SchemeLabelPrintImpl(PrintDocument pd, string jsonData) : base(pd, jsonData)
        {
        }

        public override PrintModel InitPrintModel(string jsonData) => JsonSerializer.Deserialize<WashSchemePrintModel>(jsonData)!;

        public override void Prepare()
        {
            pd.DocumentName = "方案标签打印";
        }

        public override void InitPrinterAndPaper()
        {
            var printerName = PrintUtil.GetLocalPrinters()[printModel.printerIndex];
            pd.PrinterSettings.PrinterName = printerName;
            CalcLines();
            var height = lineHeight * graphicsDecoration.LineCount - 50;
            var newPaperSize = new PaperSize("程序自定义纸张", 135, height)
            {
                RawKind = (int)PaperKind.Custom
            };
            pd.DefaultPageSettings.Margins = new Margins(2, 2, 5, 5);
            pd.DefaultPageSettings.PaperSize = newPaperSize;
            Console.WriteLine("新纸张:" + newPaperSize);
        }

        public override void DrawModel(GraphicsDecoration graphicsDecoration)
        {
            Console.WriteLine("graphics开始绘制");
            DrawModelReal(graphicsDecoration);
            Console.WriteLine("graphics绘制完成");
        }

        private void CalcLines()
        {
            DrawModel(graphicsDecoration);
        }

        private static readonly int lineHeight = 20;
        private static readonly int lineCharCount = 10;

        private void DrawModelReal(GraphicsDecoration graphicsDecoration)
        {
            WashSchemePrintModel washSchemePrintModel = (WashSchemePrintModel)printModel;
            int xShifting = 2;
            int yShifting = 20;
            Font fontTitle = new("宋体", 8, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);

            graphicsDecoration.DrawString("测试可变高度打印", fontTitle, brush, new Point(xShifting + 2, yShifting));
            yShifting += lineHeight + 10;

            int recWidth = 2;
            int recHeight = 10;

            graphicsDecoration.FillRectangle(brush, new Rectangle(xShifting, yShifting, recWidth, recHeight));
            graphicsDecoration.DrawString("名称------------------", fontTitle, brush, new PointF(xShifting + 3, yShifting));
            yShifting += lineHeight;

            graphicsDecoration.DrawStringWrap(washSchemePrintModel.productionLineName!, fontTitle, brush, new PointF(xShifting, yShifting),
                ref yShifting, lineHeight, lineCharCount);

            graphicsDecoration.FillRectangle(brush, new Rectangle(xShifting, yShifting, recWidth, recHeight));
            graphicsDecoration.DrawString("方案------------------", fontTitle, brush, new PointF(xShifting + 3, yShifting));
            yShifting += lineHeight;

            foreach (var plan in washSchemePrintModel.currentPlanList!)
            {
                graphicsDecoration.DrawStringWrap(plan.processName + "：" + plan.processItemName, fontTitle, brush, new PointF(xShifting, yShifting),
                              ref yShifting, lineHeight, lineCharCount);
            }

            graphicsDecoration.DrawStringWrap("备注：" + washSchemePrintModel.remark, fontTitle, brush, new PointF(xShifting, yShifting),
                ref yShifting, lineHeight, lineCharCount);

            graphicsDecoration.FillRectangle(brush, new Rectangle(xShifting, yShifting, recWidth, recHeight));
            graphicsDecoration.DrawString("方式------------------", fontTitle, brush, new PointF(xShifting + 3, yShifting));
            yShifting += lineHeight;

            graphicsDecoration.DrawString(washSchemePrintModel.packageType!, fontTitle, brush, new PointF(xShifting, yShifting));
            yShifting += lineHeight;

            graphicsDecoration.FillRectangle(brush, new Rectangle(xShifting, yShifting, recWidth, recHeight));
            graphicsDecoration.DrawString("服务------------------", fontTitle, brush, new PointF(xShifting + 3, yShifting));
            yShifting += lineHeight;

            if (washSchemePrintModel.additionalServiceInfo != null && washSchemePrintModel.additionalServiceInfo != "")
            {
                graphicsDecoration.DrawStringWrap(washSchemePrintModel.additionalServiceInfo, fontTitle, brush, new PointF(xShifting, yShifting),
                    ref yShifting, lineHeight, lineCharCount);
            }

            graphicsDecoration.FillRectangle(brush, new Rectangle(xShifting, yShifting, recWidth, recHeight));
            graphicsDecoration.DrawString("预计时间----------------", fontTitle, brush, new PointF(xShifting + 3, yShifting));
            yShifting += lineHeight;

            graphicsDecoration.DrawString(washSchemePrintModel.appointmentBackTime!, fontTitle, brush, new PointF(xShifting, yShifting));
        }
    }
}