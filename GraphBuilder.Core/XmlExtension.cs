using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GraphBuilder.Core
{
    public static class XmlExtension
    {
        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }

        public static string Serialize<T>(this T value)
        {
            if (value == null) return string.Empty;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = false }))
                {
                    xmlSerializer.Serialize(xmlWriter, value, ns);
                    return stringWriter.ToString();
                }
            }
        }

        public static T Deserialize<T>(this string input)
        {
            using (var stringReader = new StringReader(input))
            {
                using (XmlReader reader = new XmlTextReader(stringReader))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }
            }
        }

        public static string SerializeToXml<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred SerializeToXml", ex);
            }
        }

        public static byte[] SerializeToXmlBytes<T>(this T value)
        {
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                using (var memStream = new MemoryStream())
                {
                    xmlserializer.Serialize(memStream, value);
                    return memStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred SerializeToXmlBytes", ex);
            }
        }
    }
}