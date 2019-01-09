using System;
using System.IO;

namespace CrossRepoDependencies
{
    class Program
    {
        private static void Main(string[] args)
        {
            var pathToAllRepos = args[0] ?? @"../../../../..";
            string[] filenames = Directory.GetFiles(pathToAllRepos, "*.", SearchOption.AllDirectories);
        }
    }
}
