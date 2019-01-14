using OpenSoftware.DgmlTools.Model;

namespace CrossRepoDependencies
{
    class Program
    {
        private static void Main(string[] args)
        {

            var pathToAllRepos = ExtractCommandLineArgument(args, 0, @"../../../../..");
            var graphFilename = ExtractCommandLineArgument(args, 1, "../../../../../all-repos-graph.dgml");
            var domainPrefix = ExtractCommandLineArgument(args, 2, @"CXO");

            var repositories = new Parser(pathToAllRepos, domainPrefix).Parse();
            var graph = new Visualizer(repositories, graphFilename, domainPrefix).CreateGraph();

            graph.WriteToFile(graphFilename);
        }

        private static string ExtractCommandLineArgument(string[] args, int index, string defaultValue)
        {
            return (args.Length > index) ? args[index] : defaultValue;
        }


    }
}
