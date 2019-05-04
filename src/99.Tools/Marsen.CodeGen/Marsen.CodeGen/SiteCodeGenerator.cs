using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Marsen.CodeGen
{
    public class SiteCodeGenerator
    {
        private const string SolutionPath = @"D:\Repo\Marsen\Marsen.NetCore.Site\src\";
        private const string DaProjectPath = @"03.Data\Marsen.NetCore.DA";
        private const string BlProjectPath = @"02.Logic\Marsen.Business.Logic";

        private static readonly Dictionary<Type, string> TypeLookup = new Dictionary<Type, string>
        {
            {typeof(int), "int"},
            {typeof(int?), "int?"},
            {typeof(long), "long"},
            {typeof(long?), "long?"},
            {typeof(decimal), "decimal"},
            {typeof(decimal?), "decimal?"},
            {typeof(string), "string"},
            {typeof(byte), "byte"},
            {typeof(byte?), "byte?"},
            {typeof(bool), "bool"},
            {typeof(bool?), "bool?"},
            {typeof(DateTime), "DateTime"},
            {typeof(DateTime?), "DateTime?"},
            {typeof(TimeSpan), "TimeSpan"},
            {typeof(TimeSpan?), "TimeSpan?"},
        };

        public void GenerateDataStorage(string entityName)
        {
            //// Check Path
            var templateName = Path.Combine("Templates", "DataStorage.txt");
            var outFileName = $"{entityName}Storage.cs";
            var projectPath = Path.Combine(DaProjectPath, "Storage");
            var outFilePath = Path.Combine(SolutionPath, projectPath, outFileName);
            //// Check File
            if (File.Exists(outFilePath))
            {
                Console.WriteLine($"File Already Exist! {outFileName}");
            }

            //// Prepare Data
            var model = new Dictionary<string, string>
            {
                {"Model.Entity" , entityName},
            };
            var section = new Dictionary<string, string>();

            //// Generator
            GenerateCode(templateName, outFilePath, model, section);
        }

        public void GenerateBlEntity(string entityName)
        {
            var templateName = Path.Combine("Templates", "BLEntity.txt");
            var projectPath = Path.Combine(BlProjectPath, "Entities");
            var outFilePath = Path.Combine(SolutionPath, projectPath, $"{entityName}Entity.cs");
            var ormPath = Path.Combine(DaProjectPath, "Models");
            var sourceFile = Path.Combine(SolutionPath, ormPath, $"{entityName}.cs");
            if (File.Exists(outFilePath))
            {
                Console.WriteLine($"File Already Exist! {entityName}Entity.cs");
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

                var keyword = string.Empty;
                if (TypeLookup.Keys.Contains(p.PropertyType))
                {
                    keyword = TypeLookup[p.PropertyType];
                }

                property += $"\t\t/// <summary>\n\t\t/// {regex.Replace(p.Name, string.Empty, 1)}\n\t\t/// </summary>\n\t\tpublic {keyword} {regex.Replace(p.Name, string.Empty, 1)} {{ get; set; }}\n\n";
            }

            var section = new Dictionary<string, string>
            {
                {"Section.Property",property },
            };

            //// Generator
            GenerateCode(templateName, outFilePath, model, section);
        }

        private string GetColumnDesc(string tableName)
        {
            ////TODO:取得DB的描述(僅確定MsSQL適用)
            throw new NotImplementedException();
        }

        /// <summary>
        /// 寫入檔案
        /// </summary>
        /// <param name="outFilePath"></param>
        /// <param name="content"></param>
        private void GenerateFile(string outFilePath, string content)
        {
            //// Create Folder
            new FileInfo(outFilePath).Directory?.Create();
            //// Create File
            File.WriteAllText(outFilePath, content, Encoding.UTF8);
        }

        public void GenerateCode(string templateName, string outFilePath, Dictionary<string, string> model, Dictionary<string, string> section = null)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templateName);

            //// Generator
            var codeGenerator = new CodeGenerator();
            var result = codeGenerator.Generator(filePath, model, section);

            //// Write File
            GenerateFile(outFilePath, result);

        }
    }
}
