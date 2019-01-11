using System;
using System.Collections.Generic;
using System.Text;

namespace CrossRepoDependencies
{
    class Package
    {
        public string Name { get; set; }

        public IEnumerable<string> ReferredPackagesInTheSameRepository { get; set; }

        public IEnumerable<string> ReferredPackagesFromOtherRepositories { get; set; }
    }
}
