using System;
using System.Collections.Generic;
using System.Text;

namespace CrossRepoDependencies
{
    public class Package
    {
        public string Solution { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> ReferredAssemblies { get; set; }
    }
}
