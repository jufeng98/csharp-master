using System.Diagnostics;
using System.Reflection;

namespace CsharpHardware
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            label4.Text = $"版本号:{version}";
            label1.Text = $"编译时间:{File.GetLastWriteTime(GetType().Assembly.Location)}";
            label2.Text = $"程序所在目录:{Application.StartupPath}";
            var sysBit = Environment.Is64BitOperatingSystem ? "64位" : "32位";
            label3.Text = $"当前系统位数:{sysBit}";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Process.Start("msinfo32.exe");
        }
    }
}