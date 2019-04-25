using System;

namespace Marsen.CodeGen
{
    class Program
    {
        private const string SolutionPath = @"D:\Repo\Marsen\Marsen.NetCore.Site\src\";
        private const string DaProject = @"Marsen.NetCore.DA";

        static void Main(string[] args)
        {
            var entityName = "Shop";
            Console.WriteLine($"SolutionPath: {SolutionPath}");
            Console.WriteLine($"Entity: {entityName}");
            Console.WriteLine("=== Start Processing ===");
        }
    }
}
