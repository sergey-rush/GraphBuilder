using System;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace GraphBuilder.Shell.Models
{
    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class Thumb
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        [XmlIgnore]
        public BitmapImage Image { get; set; }
        public string Path { get; set; }
    }
}