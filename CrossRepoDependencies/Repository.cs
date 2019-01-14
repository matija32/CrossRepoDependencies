using System;
using System.Collections.Generic;
using System.Text;

namespace CrossRepoDependencies
{
    public class Repository
    {
        public string Name { get; set; }
        public IEnumerable<Package> Projects { get; set; }

    }
}
