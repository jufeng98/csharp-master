namespace CsharpHardware.print
{
    internal class PrintModel
    {
        public int printerIndex { get; set; }
        public string? paperName { get; set; }
        public string? model { get; set; }
    }

    internal class WashSchemePrintModel : PrintModel
    {
        public string? productionLineName { get; set; }
        public List<WashPlan>? currentPlanList { get; set; }
        public string? remark { get; set; }
        public string? packageType { get; set; }
        public string? additionalServiceInfo { get; set; }
        public string? appointmentBackTime { get; set; }

        public class WashPlan
        {
            public string? processName { get; set; }
            public string? processItemName { get; set; }
        }
    }
}