using System;

namespace GraphBuilder.Shell.Models
{
    public class CompoundFile
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public byte[] FileBytes { get; set; }
    }
}