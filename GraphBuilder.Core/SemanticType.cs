using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphBuilder.Core
{
    public class SemanticType:ViewModelBase
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        
    }
}
