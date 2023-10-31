using System.Windows;
using System.Windows.Data;

namespace CsharpWpf.data
{
    internal class Student : DependencyObject
    {

        public string NameProperty
        {
            get { return (string)GetValue(NamePropertyProperty); }
            set
            {
                SetValue(NamePropertyProperty, value);
            }
        }

        public static readonly DependencyProperty NamePropertyProperty =
            DependencyProperty.Register("NameProperty", typeof(string), typeof(Student), new PropertyMetadata("Jack"));

        public BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase binding)
        {
            return BindingOperations.SetBinding(this, dp, binding);
        }

    }
}
