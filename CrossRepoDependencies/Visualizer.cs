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
        private readonly IEnumerable<Repository> _repositories;
        private readonly string _graphFilename;
        private readonly string _domainPrefix;

        public Visualizer(IEnumerable<Repository> repositories, string graphFilename, string domainPrefix)
        {
            _repositories = repositories;
            _graphFilename = graphFilename;
            _domainPrefix = domainPrefix;
        }

        public void CreateGraph()
        {
            var builder = new DgmlBuilder(new HubNodeAnalysis())
            {
                NodeBuilders = new[]
                {
                    new NodeBuilder<Package>(ExternalAssembly2Node, x => !x.Name.StartsWith(_domainPrefix)),
                    new NodeBuilder<Package>(InternalAssembly2Node, x => x.Name.StartsWith(_domainPrefix))
                }
            };
            var graph = builder.Build(new []
            {
                new Package()
                {
                    Name = "hellossss"
                },
                new Package()
                {
                    Name = "CXO.hello.hello"
                }
            });
            graph.WriteToFile(_graphFilename);
        }

        private Node Repository2Node(Repository arg)
        {
            throw new NotImplementedException();
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
                Label = package.Name
            };
        }
    }
}
