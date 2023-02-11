using System.Drawing.Printing;

namespace CsharpHardware.print
{
    internal abstract class LabelPrintBase : ILabelPrint
    {
        protected PrintDocument pd;
        protected PrintModel printModel;

        protected LabelPrintBase(PrintDocument pd, string jsonData)
        {
            this.pd = pd;
            printModel = InitPrintModel(jsonData);
            InitPrinterAndPaper();
        }

        public abstract PrintModel InitPrintModel(string jsonData);

        public virtual void InitPrinterAndPaper()
        {
            var printerName = PrintUtil.GetLocalPrinters()[printModel.printerIndex];
            pd.PrinterSettings.PrinterName = printerName;
            PaperSize paperSize = PrintUtil.GetPaperSizeByPaperName(printModel.paperName!, pd.PrinterSettings.PaperSizes);
            Console.WriteLine("当前选择纸张:" + paperSize);
            pd.DefaultPageSettings.PaperSize = paperSize;
        }

        public void PrepareAndPrint()
        {
            Prepare();
            pd.PrintPage += (sender, e) =>
            {
                try
                {
                    GraphicsDecoration graphicsDecoration = new()
                    {
                        Graphics = e.Graphics
                    };
                    DrawModel(graphicsDecoration, e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
        }

        public abstract void Prepare();

        public virtual void DrawModel(GraphicsDecoration graphicsDecoration, PrintPageEventArgs e)
        {
            DrawModel(graphicsDecoration);
        }

        public abstract void DrawModel(GraphicsDecoration graphicsDecoration);

        public static ILabelPrint GetInstance(string source, PrintDocument pd, string jsonData)
        {
            PrintSource printSource = (PrintSource)Enum.Parse(typeof(PrintSource), source);
            ILabelPrint labelPrint = printSource switch
            {
                PrintSource.SCHEME_LABEL => new SchemeLabelPrintImpl(pd, jsonData),
                _ => throw new Exception("wrong source:" + source),
            };
            return labelPrint;
        }

        public string GetModel()
        {
            return printModel.model!;
        }
    }
}