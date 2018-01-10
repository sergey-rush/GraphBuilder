using System.ComponentModel;

namespace GraphBuilder.Core
{
    [DisplayName("Атрибут")]
    public class ListItem
    {
        public ListItem()
        {
            
        }

        public ListItem(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}