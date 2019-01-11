using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CrossRepoDependencies
{
    class Program
    {
        private static void Main(string[] args)
        {

            var pathToAllRepos = ExtractCommandLineArgument(args, 0, @"../../../../..");
            var searchPattern = ExtractCommandLineArgument(args, 1, @"CXO*.sln");

            var pathsToSolutions = Directory.GetFiles(pathToAllRepos, searchPattern, SearchOption.AllDirectories);
            var repositories = pathsToSolutions.Select(solutionFilename => new Repository()
            {
                Name = Path.GetFileNameWithoutExtension(solutionFilename),
                Projects = FindProjectsFor(solutionFilename)
            });

            foreach (var repository in repositories)
            {
                foreach (var repositoryProject in repository.Projects)
                {
                    Console.WriteLine("name="+repositoryProject.Name);
                    foreach (var y in repositoryProject.ReferredPackagesFromOtherRepositories)
                    {
                        Console.WriteLine(y);
                    }

                    foreach (var x in repositoryProject.ReferredPackagesInTheSameRepository)
                    {
                        Console.WriteLine(x);
                    }
                }
            }

            Console.ReadLine();
        }

        private static string ExtractCommandLineArgument(string[] args, int index, string defaultValue)
        {
            return (args.Length > index) ? args[index] : defaultValue;
        }

        private static IEnumerable<Package> FindProjectsFor(string pathToSolution)
        {
            var pathsToProjects = Directory.GetFiles(Path.GetDirectoryName(pathToSolution), @"*.csproj", SearchOption.AllDirectories);
            return pathsToProjects.Select(pathToProject => new Package()
            {
                Name = Path.GetFileNameWithoutExtension(pathToProject),
                ReferredPackagesFromOtherRepositories = GetReferredItems(pathToProject, "ProjectReference"),
                ReferredPackagesInTheSameRepository = GetReferredItems(pathToProject, "PackageReference")
            });
        }

        private static IEnumerable<string> GetReferredItems(string pathToProject, string tagName)
        {
            var projectFile = new XmlDocument();
            projectFile.Load(pathToProject);

            var nodesList = projectFile.GetElementsByTagName(tagName);

            var referredItems = new List<string>();
            for (var i = 0; i < nodesList.Count; i++)
            {
                var includedItem = nodesList[i].Attributes["Include"].Value;
                if (includedItem.StartsWith("CXO"))
                {
                    referredItems.Add(Path.GetFileNameWithoutExtension(includedItem));
                }
            };

            return referredItems;
        }
    }
}
