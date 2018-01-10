using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;
using GraphBuilder.Core;

namespace GraphBuilder.Shell.Models
{
    [Serializable()]
    [XmlType(AnonymousType = true)]
    [XmlRootAttribute(ElementName = "Document", Namespace = "", IsNullable = false)]
    public class Document
    {
        /// <summary>
        /// Used as file name for the document
        /// </summary>
        public string Title { get; set; }
        public string Name { get; set; }
        public string Header { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }
        public string Comments { get; set; }
        public List<ListItem> Notes { get; set; }
        public DateTime Created { get; set; }

        [XmlElementAttribute("Nodes", Form = XmlSchemaForm.Unqualified)]
        public List<Node> Nodes { get; set; }

        [XmlElementAttribute("Links", Form = XmlSchemaForm.Unqualified)]
        public List<Link> Links { get; set; }

        public DocumentMode DocumentMode { get; set; }

        /// <summary>
        /// Collection attached documents 
        /// </summary>
        public List<CompoundFile> Embedded { get; set; }

        public Document()
        {

            Notes = new List<ListItem>();
            Header = "Новый документ";
            Subject = "Новая история";
            Author = "Автор документа";
            Keywords = "Документ";
            Comments = "Документ сохранен без ограничений";
            Notes.Add(new ListItem("Дело", ""));
            Notes.Add(new ListItem("Классификация", ""));
            Created = DateTime.Now;
        }

        public Document(string name) : this()
        {
            Name = name;
        }
    }
}