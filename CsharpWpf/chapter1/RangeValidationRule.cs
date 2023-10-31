using System.Globalization;
using System.Windows.Controls;

namespace CsharpWpf.chapter1
{
    internal class RangeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int x))
            {
                if (x < 0 || x > 100)
                {
                    return new ValidationResult(false, "内容有误");
                }
            }
            return new ValidationResult(true, "");
        }
    }
}
