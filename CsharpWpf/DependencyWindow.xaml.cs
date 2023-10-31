using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CsharpWpf.data;

namespace CsharpWpf
{
    /// <summary>
    /// DependencyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DependencyWindow : Window
    {
        private readonly Student student = new Student();
        public DependencyWindow()
        {
            InitializeComponent();
            DataContext = student;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student student = (Student)DataContext;
            School.SetGrade(student, "六年级");
            MessageBox.Show(School.GetGrade(student));
        }
    }
}
