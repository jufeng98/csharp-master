using System.ComponentModel;
using CsharpWpf.chapter1;

namespace CsharpWpf.data
{
    [TypeConverter(typeof(StringToHuman))]
    public class Human
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Human Child { get; set; }

        public Human() { }
        public Human(int id, string name) { this.Id = id; this.Name = name; }

        //public override string ToString()
        //{
        //    return JsonConvert.SerializeObject(this);           
        //}
    }
}
