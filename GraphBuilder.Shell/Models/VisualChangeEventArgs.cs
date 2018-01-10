using System;

namespace GraphBuilder.Shell.Models
{
    public class VisualChangeEventArgs : EventArgs
    {
        public int Added { get; set; }
        public int Removed { get; set; }
        public VisualChangeEventArgs(int added, int removed)
        {
            Added = added;
            Removed = removed;
        }
    }
}