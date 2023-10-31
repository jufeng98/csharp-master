using System.Windows;

namespace CsharpWpf.data
{
    internal class School:DependencyObject
    {
        public static string GetGrade(DependencyObject obj)
        {
            return (string)obj.GetValue(GradeProperty);
        }

        public static void SetGrade(DependencyObject obj, string value)
        {
            obj.SetValue(GradeProperty, value);
        }

        public static readonly DependencyProperty GradeProperty =
            DependencyProperty.RegisterAttached("Grade", typeof(string), typeof(School), new PropertyMetadata(""));


    }
}
