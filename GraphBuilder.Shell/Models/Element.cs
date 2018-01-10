using System;
using System.ComponentModel;

namespace GraphBuilder.Shell.Models
{
    public class Element
    {
        [Browsable(false)]
        public Guid UId { get; set; }
    }
}