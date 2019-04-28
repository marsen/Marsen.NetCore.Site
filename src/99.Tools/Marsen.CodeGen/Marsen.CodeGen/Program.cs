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
        private const string SolutionPath = @"D:\Repo\Marsen\Marsen.NetCore.Site\src\";
        private const string DAProjectPath = @"03.Data\Marsen.NetCore.DA";
        private const string BLProjectPath = @"02.Logic\Marsen.Business.Logic";

        private static readonly Dictionary<string, string> TypeLookup = new Dictionary<string, string>
        {
            {"System.Int32", "int"},
            {"System.Nullable`1[System.Int32]", "long?"},
            {"System.Int64", "long"},
            {"System.Nullable`1[System.Int64]", "long?"},
            {"System.String", "string"},
            {"System.Byte", "byte"},
            {"System.Nullable`1[System.Byte]", "byte?"},
            {"System.Boolean", "bool"},
            {"System.Nullable`1[System.Boolean]", "bool?"},
            {"System.DateTime", "DateTime"},
            {"System.Nullable`1[System.DateTime]", "DateTime?"},
            {"System.TimeSpan", "TimeSpan"},
            {"System.Nullable`1[System.TimeSpan]", "TimeSpan?"},
            {"System.Decimal", "decimal"},
            {"System.Nullable`1[System.Decimal]", "decimal?"},
        };

        static void Main(string[] args)
        {
            var entityName = "Shop";
            Console.WriteLine($"SolutionPath: {SolutionPath}");
            Console.WriteLine($"Entity: {entityName}");
            Console.WriteLine("=== Start Processing ===");
            GenerateEntity(entityName);
        }

        public static void GenerateEntity(string entityName)
        {            
            string templateName = Path.Combine("Templates", "BLEntity.txt");
            var outFileName = $"{entityName}Entity.cs";
            var projectPath = Path.Combine(BLProjectPath, "Entities");
            string outFilePath = Path.Combine(Program.SolutionPath, projectPath, outFileName);
            var ormPath = Path.Combine(DAProjectPath, "Models");            
            string sourceFile = Path.Combine(Program.SolutionPath, ormPath, $"{entityName}.cs");
            if (File.Exists(outFilePath))
            {
                Console.WriteLine($"File Already Exist! {outFileName}");                
            }

            var model = new Dictionary<string, string>
            {
                {"Model.NamespaceName" , "Marsen.Business.Logic.Entities" },
                {"Model.Entity" , entityName},
                
            };

            //// Parse
            var codeParse = new CodeParse();
            var typeInfos = codeParse.ParseType(sourceFile);
            var notGenProperties = new List<string>
            {
                $"{entityName}_ValidFlag",
                $"{entityName}_CreatedDateTime",
                $"{entityName}_CreatedUser",
                $"{entityName}_UpdatedTimes",
                $"{entityName}_UpdatedDateTime",
                $"{entityName}_UpdatedUser",
                $"{entityName}_RowVersion",
            };

            var property = string.Empty;
            var regex = new Regex(Regex.Escape(entityName));
            foreach (var p in typeInfos.GetProperties())
            {
                var columnName = $"{entityName}{regex.Replace(p.Name, string.Empty, 1)}";
                if (notGenProperties.Contains(columnName))
                {
                    continue;
                }
                var keyword = p.PropertyType.ToString();
                if (TypeLookup.Keys.Contains(keyword))
                {
                    keyword = TypeLookup[p.PropertyType.ToString()];
                }

                property += $"\t\t/// <summary>\n\t\t/// {regex.Replace(p.Name, string.Empty, 1)}\n\t\t/// </summary>\n\t\tpublic {keyword} {regex.Replace(p.Name, string.Empty, 1)} {{ get; set; }}\n\n";
            }

            var section = new Dictionary<string, string>
            {
                {"Section.Using" , "using Marsen.Business.Logic.Entities;" },
                {"Section.Property",property },
            };

            //// Generator
            GenerateCode(templateName, outFilePath, model, section);
        }

        private static string GetColumnDesc(string tableName)
        {
            ////TODO:取得DB的描述(僅確定MsSQL適用)
            throw new NotImplementedException();
        }

        /// <summary>
        /// 寫入檔案
        /// </summary>
        /// <param name="outFilePath"></param>
        /// <param name="content"></param>
        private static void GenerateFile(string outFilePath, string content)
        {
            //// Output
            var fileInfo = new FileInfo(outFilePath);
            //// Create Folder
            fileInfo.Directory.Create();
            //// Create File
            File.WriteAllText(outFilePath, content, Encoding.UTF8);
        }

        private static string GenerateCode(string templateName, string outFilePath, Dictionary<string, string> model, Dictionary<string, string> section = null)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templateName);

            //// Generator
            var codeGenerator = new CodeGenerator();
            var result = codeGenerator.Generator(filePath, model, section);

            //// Write File
            GenerateFile(outFilePath, result);

            return result;
        }
    }
}
