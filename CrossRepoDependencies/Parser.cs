using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace CrossRepoDependencies
{
    public class Parser
    {
        string RootDir { get; }
        string DomainPrefix { get; }

        public Parser(string rootDir, string domainPrefix)
        {
            RootDir = rootDir;
            DomainPrefix = domainPrefix;
        }

        public IEnumerable<Repository> Parse()
        {
            var pathsToSolutions = Directory.GetFiles(RootDir, DomainPrefix + @"*.sln", SearchOption.AllDirectories);
            var repositories = pathsToSolutions.Select(solutionFilename => new Repository()
            {
                Name = Path.GetFileNameWithoutExtension(solutionFilename),
                Projects = FindProjectsFor(solutionFilename)
            });
            return repositories;
        }

       
        private static IEnumerable<Package> FindProjectsFor(string pathToSolution)
        {
            var pathsToProjects = Directory.GetFiles(Path.GetDirectoryName(pathToSolution), @"*.csproj", SearchOption.AllDirectories);
            return pathsToProjects.Select(pathToProject => new Package()
            {
                Name = Path.GetFileNameWithoutExtension(pathToProject),
                ReferredAssembliesFromOtherRepositories = GetProjectReferences(pathToProject, "ProjectReference", true),
                ReferredAssembliesInTheSameRepository = GetProjectReferences(pathToProject, "PackageReference", false)
            });
        }

        private static IEnumerable<string> GetProjectReferences(
            string pathToProject, 
            string tagName,
            bool referringByFilePath)
        {
            var projectFile = new XmlDocument();
            projectFile.Load(pathToProject);
            var nodesList = projectFile.GetElementsByTagName(tagName);

            for (var i = 0; i < nodesList.Count; i++)
            {
                var includedItemValueInXml = nodesList[i].Attributes["Include"].Value;
                var includedItem = referringByFilePath
                    ? Path.GetFileNameWithoutExtension(includedItemValueInXml)
                    : includedItemValueInXml;

                yield return includedItem;

            }
        }

    }
}
