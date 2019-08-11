using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Marsen.CodeGen
{
    class Program
    {
        static void Main(string[] args)
        {
            var entityName = "Product";            
            Console.WriteLine($"Entity: {entityName}");
            Console.WriteLine("=== Start Processing ===");
            var siteGen = new SiteCodeGenerator();
            siteGen.GenerateBlEntity(entityName);
            siteGen.GenerateDataStorage(entityName);
        }
    }
}
