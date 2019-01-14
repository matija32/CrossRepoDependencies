using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using OpenSoftware.DgmlTools;
using OpenSoftware.DgmlTools.Analyses;
using OpenSoftware.DgmlTools.Builders;
using OpenSoftware.DgmlTools.Model;

namespace CrossRepoDependencies
{
    class Visualizer
    {
        private readonly IEnumerable<Package> _packages;
        private readonly string _graphFilename;
        private readonly string _domainPrefix;

        public Visualizer(IEnumerable<Package> packages, string graphFilename, string domainPrefix)
        {
            _packages = packages;
            _graphFilename = graphFilename;
            _domainPrefix = domainPrefix;
        }

        public DirectedGraph CreateGraph()
        {
            var builder = new DgmlBuilder(new CategoryColorAnalysis())
            {
                NodeBuilders = new []
                {
                    new NodeBuilder<Package>(ExternalAssembly2Node, x => !x.Name.StartsWith(_domainPrefix)),
                    new NodeBuilder<Package>(InternalAssembly2Node, x => x.Name.StartsWith(_domainPrefix))
                },
                LinkBuilders = new []
                {
                    new LinksBuilder<Package>(ReferredPackage2Link), 
                }
            };
            return builder.Build(_packages);
        }

        private IEnumerable<Link> ReferredPackage2Link(Package package)
        {
            return package.ReferredAssemblies.Where(x => x.StartsWith(_domainPrefix))
                .Select(x => new Link { Source = package.Name, Target = x, Category = "References", IsContainment = false})
                .Append(new Link{ Source = "Solution " + package.Solution, Target = package.Name, Category = "Contains", IsContainment = true});
        }

        private Node ExternalAssembly2Node(Package package)
        {
            return new Node
            {
                Category = "ExternalAssembly",
                Id = package.Name,
                Label = package.Name
            };
        }

        private Node InternalAssembly2Node(Package package)
        {
            return new Node
            {
                Category = "InternalAssembly",
                Id = package.Name,
                Label = package.Name,
                Description = package.Solution
            };
        }
    }
}
