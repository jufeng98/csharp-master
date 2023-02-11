using CsharpHardware.print;
using System.Drawing.Printing;
using System.Text.Json;
using TouchSocket.Http;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace CsharpHardware
{
    public partial class FormPrintPreview : PrintPreviewDialog
    {
        private readonly HttpSocketClient? client;
        private readonly string model;

        public static void GetPrinters(ITcpClientBase clientBase)
        {
            var list = PrintUtil.GetLocalPrinters();
            var jsonData = JsonSerializer.Serialize(list);
            (clientBase as HttpSocketClient).SendWithWS(jsonData);
            clientBase?.Close("close ws");
        }

        public static void GetPapers(ITcpClientBase clientBase, string wsData)
        {
            PrintDocument pd = new();
            var printerName = PrintUtil.GetLocalPrinters()[int.Parse(wsData.Split(':')[1])];
            pd.PrinterSettings.PrinterName = printerName;
            var list = pd.PrinterSettings.PaperSizes;
            var jsonData = JsonSerializer.Serialize(list);
            (clientBase as HttpSocketClient).SendWithWS(jsonData);
            clientBase?.Close("close ws");
        }

        public static void ShowDialog(ITcpClientBase clientBase, string wsData)
        {
            var dialog = new FormPrintPreview(clientBase, wsData);
            if (dialog.model == "PREVIEW")
            {
                dialog.ShowDialog();
            }
            else if (dialog.model == "PRINT")
            {
                dialog.pd.Print();
            }
            dialog.Dispose();
        }

        public FormPrintPreview(ITcpClientBase clientBase, string wsData)
        {
            InitializeComponent();
            Load += (sender, e) =>
            {
                Activate();
            };
            client = clientBase as HttpSocketClient;
            var index = wsData.IndexOf('~');
            string source = wsData[..index].Split(':')[1];
            string jsonData = wsData[(index + 1)..];
            ILabelPrint labelPrint = LabelPrintBase.GetInstance(source, pd, jsonData);
            model = labelPrint.GetModel();
            RefreshFormPrintParam();
            labelPrint.PrepareAndPrint();
            pd.BeginPrint += new PrintEventHandler((object sender, PrintEventArgs e) =>
            {
                if (e.PrintAction == PrintAction.PrintToPrinter || e.PrintAction == PrintAction.PrintToFile)
                {
                    Console.WriteLine("开始打印");
                }
            });
            pd.EndPrint += new PrintEventHandler((object sender, PrintEventArgs e) =>
            {
                if (e.PrintAction == PrintAction.PrintToPrinter || e.PrintAction == PrintAction.PrintToFile)
                {
                    Console.WriteLine("完成打印");
                    client?.SendWithWS("success");
                    client?.Close("close ws");
                    Close();
                }
            });
            Document = pd;
        }

        private void PrinterSettingsButton_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new()
            {
                Document = pd
            };
            var result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Document = pd;
                RefreshFormPrintParam();
            }
        }

        private void PageSettingsButton_Click(object sender, EventArgs e)
        {
            PageSetupDialog pageSetupDialog = new()
            {
                Document = pd
            };
            var result = pageSetupDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Document = pd;
                RefreshFormPrintParam();
            }
        }

        private void RefreshFormPrintParam()
        {
            var paperSize = pd.DefaultPageSettings.PaperSize;
            paperSizeLabel.Text = $"纸张:{PrintUtil.ConvertInchToMm(paperSize.Width):f1}mm * {PrintUtil.ConvertInchToMm(paperSize.Height):f1}mm";
            var printableArea = pd.DefaultPageSettings.PrintableArea;
            printAreaLabel.Text = $"可打印区域:{PrintUtil.ConvertInchToMm(printableArea.Width):f1}mm * {PrintUtil.ConvertInchToMm(printableArea.Height):f1}mm";
            var printerResolution = pd.DefaultPageSettings.PrinterResolution;
            printResolutionLabel.Text = $"打印分辨率:{printerResolution.X} * {printerResolution.Y}";
        }
    }
}