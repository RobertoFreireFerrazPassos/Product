namespace Product.Models
{
    public enum FieldType
    {
        Text = 1,
        Int = 2
    }

    public abstract class Field
    {
        public string Name { get; set; }

        public FieldType Type { get; set; }
    }

    public class TextField : Field
    {
        public TextField(string name)
        {
            Name = name;
            Type = FieldType.Text;
        }
    }

    public class IntField : Field
    {
        public int? Min { get; set; }
        public int? Max { get; set; }

        public IntField(string name, int? min = null, int? max = null)
        {
            Min = min;
            Max = max;
            Name = name;
            Type = FieldType.Int;
        }
    }

    public class FormViewModel
    {
        public int CurrentIndex = 0;
        public IEnumerable<Field> Fields { get; set; }
    }
}
