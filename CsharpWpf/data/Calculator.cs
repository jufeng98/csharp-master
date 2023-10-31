namespace WpfLearnApp.data
{
    public class Calculator
    {
        public string Add(string a, string b)
        {
            if (int.TryParse(a, out var x) && int.TryParse(b, out var y))
            {
                return (x + y) + "";
            }
            return "";
        }

    }
}
