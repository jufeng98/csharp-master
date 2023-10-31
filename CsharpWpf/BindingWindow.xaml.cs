using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CsharpWpf.chapter1;
using CsharpWpf.data;
using WpfLearnApp.data;

namespace CsharpWpf
{
    /// <summary>
    /// BindingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BindingWindow : Window
    {
        public static ObservableCollection<Human> HumanList { get; set; } = new ObservableCollection<Human>()
        {
            new Human(1,"Rose"),
            new Human(2,"Jack"),
            new Human(3,"John"),
        };

        public BindingWindow()
        {
            InitializeComponent();
            var binding = new Binding("Value")
            {
                ElementName = "slider",
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            var rvr = new RangeValidationRule()
            {
                ValidatesOnTargetUpdated = true,
            };
            binding.ValidationRules.Add(rvr);
            binding.NotifyOnValidationError = true;
            textBox.SetBinding(TextBox.TextProperty, binding);
            textBox.AddHandler(Validation.ErrorEvent, new RoutedEventHandler((s, e) =>
            {
                textBox.ToolTip = e.RoutedEvent.ToString();
            }));

            ObjectDataProvider objectDataProvider = new ObjectDataProvider
            {
                ObjectInstance = new Calculator(),
                MethodName = "Add",
            };
            objectDataProvider.MethodParameters.Add("100");
            objectDataProvider.MethodParameters.Add("200");
            textBox5.SetBinding(TextBox.TextProperty, new Binding("MethodParameters[0]") { Source = objectDataProvider, BindsDirectlyToSource = true, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            textBox6.SetBinding(TextBox.TextProperty, new Binding("MethodParameters[1]") { Source = objectDataProvider, BindsDirectlyToSource = true, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            label.SetBinding(ContentProperty, new Binding(".") { Source = objectDataProvider });

            RelativeSource relativeSource = new RelativeSource(RelativeSourceMode.FindAncestor)
            {
                AncestorType = typeof(Grid),
                AncestorLevel = 1
            };
            label1.SetBinding(ContentProperty, new Binding("Name") { RelativeSource = relativeSource });

            MultiBinding multiBinding = new MultiBinding();
            multiBinding.Bindings.Add(new Binding("Text") { Source = textBox8 });
            multiBinding.Bindings.Add(new Binding("Text") { Source = textBox9 });
            multiBinding.Bindings.Add(new Binding("Text") { Source = textBox10 });
            multiBinding.Bindings.Add(new Binding("Text") { Source = textBox11 });
            multiBinding.Converter = new MyMultiValueConverter();
            button.SetBinding(IsEnabledProperty, multiBinding);
        }
    }

    public class MyMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Cast<string>().All(it => !string.IsNullOrEmpty(it))
                && values[0].ToString() == values[1].ToString() && values[2].ToString() == values[3].ToString())
            {
                return true;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
