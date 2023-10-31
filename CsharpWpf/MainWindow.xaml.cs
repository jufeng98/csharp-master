using CsharpWpf.chapter1;
using CsharpWpf.data;
using CsharpWpf.util;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Toast;

namespace CsharpWpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ContextModel contextModel;

        public MainWindow()
        {
            InitializeComponent();
            contextModel = new ContextModel();
            DataContext = contextModel;

            gridMain.AddHandler(Button.ClickEvent, new RoutedEventHandler((s, e) =>
            {
                MessageBox.Show("你点击了" + e.OriginalSource);

            }));

            gridMain.AddHandler(MyRate.RateChangedEvent, new RoutedEventHandler((s, e) => { MessageBox.Show("你点击了" + e.OriginalSource); }));

            //为外层Grid添加路由事件侦听器
            ContextModel.AddNameChangedHandler(
            this.gridMain,
            new RoutedEventHandler(new RoutedEventHandler((s, e) =>
            {
                MessageBox.Show("ContextModel的name发生了变化,新name为:" + e.OriginalSource);
            })));
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)Content;
            var child = grid.Children[3];
            var human = FindResource("human") as Human;
            MessageBox.Show(human.Child.Name + " " + child.GetType() + " " + ((Slider)FindName("slider1")).Value);
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private void ButtonMess_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var level1 = VisualTreeHelper.GetParent(btn);
            var level2 = VisualTreeHelper.GetParent(level1);
            var level3 = VisualTreeHelper.GetParent(level2);
            MessageBox.Show(level3.GetType() + "");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)FindName("listBox1");
            MessageBox.Show(listBox.SelectedValue + " " + listBox.SelectedItem);
        }

        private void Button_Stack_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            StackPanel s = (StackPanel)LogicalTreeHelper.GetParent(b);
            s.Children.Remove(b);
        }

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            BindingWindow bindingWindow = new BindingWindow();
            bindingWindow.ShowDialog();
        }

        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            DependencyWindow dependencyWindow = new DependencyWindow();
            dependencyWindow.ShowDialog();
        }

        private void Button14_Click(object sender, RoutedEventArgs e)
        {
            contextModel.Name = "Jack";
            // 附加事件
            RoutedEventArgs args = new RoutedEventArgs(ContextModel.NameChangedEvent, contextModel.Name);
            (sender as Button).RaiseEvent(args);
        }
    }
}
