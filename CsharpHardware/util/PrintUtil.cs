using System.Drawing.Printing;
using static System.Drawing.Printing.PrinterSettings;

namespace CsharpHardware
{
    internal class PrintUtil
    {
        /// <summary>
        /// 获取本地所有打印机
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalPrinters()
        {
            List<string> printer_names = new();

            foreach (string item in InstalledPrinters)
            {
                printer_names.Add(item);
            }
            return printer_names;
        }

        public static PaperSize GetPaperSizeByPaperName(string paperName, PaperSizeCollection paperSizes)
        {
            PaperSize? paperSize = null;
            foreach (var tmp in paperSizes)
            {
                PaperSize? paper = tmp as PaperSize;
                if (paper == null)
                {
                    continue;
                }
                if (paper.PaperName == paperName)
                {
                    paperSize = paper;
                    break;
                }
            }
            return paperSize!;
        }

        public static List<string> CutString(string txt, int oneRowNum)
        {
            var list = new List<string>();
            if (txt.Length < oneRowNum)
            {
                list.Add(txt);
                return list;
            }
            int num = (int)Math.Ceiling(1.0 * txt.Length / oneRowNum);
            for (int i = 0; i < num; i++)
            {
                var size = oneRowNum;
                if (oneRowNum * i + oneRowNum > txt.Length)
                {
                    size = txt.Length - oneRowNum * i;
                }
                list.Add(txt.Substring(oneRowNum * i, size));
            }
            return list;
        }

        public static double ConvertMmToInch(double mm)
        {
            return mm * 100 / 25.4;
        }

        public static double ConvertInchToMm(double inch)
        {
            return 25.4 * inch / 100;
        }
    }
}