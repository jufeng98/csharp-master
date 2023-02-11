using System.Drawing.Printing;

namespace CsharpHardware.print
{
    public interface ILabelPrint
    {
        void InitPrinterAndPaper();

        void Prepare();

        void PrepareAndPrint();

        void DrawModel(GraphicsDecoration graphics);

        void DrawModel(GraphicsDecoration graphics, PrintPageEventArgs e);

        string GetModel();
    }
}