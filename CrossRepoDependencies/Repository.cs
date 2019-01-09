using System;
using System.Collections.Generic;
using System.Text;

namespace CrossRepoDependencies
{
    class Repository
    {
        public string Name { get; set; }
        public IEnumerable<Package> projects { get; set; }

    }
}
