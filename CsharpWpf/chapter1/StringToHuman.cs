using System.ComponentModel;
using System.Globalization;
using CsharpWpf.data;

namespace CsharpWpf.chapter1
{
    public class StringToHuman:TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if(value is string v)
            {
                Human human = new Human
                {
                    Name = v
                };
                return human;
            }
            throw GetConvertFromException(value);
        }
    }
}
