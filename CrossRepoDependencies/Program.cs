namespace CrossRepoDependencies
{
    class Program
    {
        private static void Main(string[] args)
        {

            var pathToAllRepos = ExtractCommandLineArgument(args, 0, @"../../../../..");
            var graphFilename = ExtractCommandLineArgument(args, 1, "allRepos");
            var domainPrefix = ExtractCommandLineArgument(args, 2, @"CXO");

            var repositories = new Parser(pathToAllRepos, domainPrefix).Parse();

            new Visualizer(repositories, @"graph-test.dgml", graphFilename).CreateGraph();
        }

        private static string ExtractCommandLineArgument(string[] args, int index, string defaultValue)
        {
            return (args.Length > index) ? args[index] : defaultValue;
        }


    }
}
