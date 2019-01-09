using System;
using System.Collections.Generic;
using System.Text;

namespace CrossRepoDependencies
{
    class Package
    {
        public string Name { get; set; }

        public IEnumerable<Package> referredPackagesInTheSameRepository { get; set; }

        public IEnumerable<Package> referredPackagesFromOtherRepositories { get; set; }
    }
}
