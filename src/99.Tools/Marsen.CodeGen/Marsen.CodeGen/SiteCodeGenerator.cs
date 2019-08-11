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

        public void GenerateDataStorageInterface(string entityName)
        {
            //// Check Path
            var outFilePath = GetOutFilePath(entityName,
                new ProjectInfo
                {
                    Path = DaProjectPath,
                    Folder = "Storage/Interface",
                    Suffix = "Storage",
                    Prefix = "I",
                });
            var model = new Dictionary<string, string>
            {
                {"Model.Entity", entityName},
            };
            var section = new Dictionary<string, string>();

            //// Generator
            GenerateCode(Path.Combine("Templates", "DataStorageInterface.txt"), outFilePath, model, section);
        }

        public void GenerateDataStorage(string entityName)
        {
            //// Check Path
            var outFilePath = GetOutFilePath(entityName, 
                new ProjectInfo
                {
                    Path = DaProjectPath,
                    Folder = "Storage",
                    Suffix = "Storage"
                });
            var model = new Dictionary<string, string>
            {
                {"Model.Entity", entityName},
            };
            var section = new Dictionary<string, string>();

            //// Generator
            GenerateCode(Path.Combine("Templates", "DataStorage.txt"), outFilePath, model, section);
        }

        private string GetOutFilePath(string entityName, ProjectInfo info)
        {
            var projectPath = Path.Combine(info.Path, info.Folder);
            var outFilePath = Path.Combine(SolutionPath, projectPath, $"{info.Prefix}{entityName}{info.Suffix}.cs");
            if (File.Exists(outFilePath))
            {
                Console.WriteLine($"File Already Exist! {info.Prefix}{entityName}{info.Suffix}.cs");
            }

            return outFilePath;
        }


        public void GenerateBlEntity(string entityName)
        {
            var outFilePath = GetOutFilePath(entityName,
                new ProjectInfo
                {
                    Path = BlProjectPath,
                    Folder = "Entities",
                    Suffix = "Entity"
                });
            var ormPath = Path.Combine(DaProjectPath, "Models");
            var sourceFile = Path.Combine(SolutionPath, ormPath, $"{entityName}.cs");

            var model = new Dictionary<string, string>
            {
                {"Model.NamespaceName", "Marsen.Business.Logic.Entities"},
                {"Model.Entity", entityName},
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
            var keyword = string.Empty;
            foreach (var p in typeInfos.GetProperties())
            {
                var columnName = $"{entityName}{regex.Replace(p.Name, string.Empty, 1)}";
                if (notGenProperties.Contains(columnName))
                {
                    continue;
                }

                if (TypeLookup.Keys.Contains(p.PropertyType))
                {
                    keyword = TypeLookup[p.PropertyType];
                }

                property +=
                    $"\t\t/// <summary>\n\t\t/// {regex.Replace(p.Name, string.Empty, 1)}\n\t\t/// </summary>\n\t\tpublic {keyword} {regex.Replace(p.Name, string.Empty, 1)} {{ get; set; }}\n\n";
            }

            var section = new Dictionary<string, string>
            {
                {"Section.Property", property},
            };

            //// Generator
            GenerateCode(Path.Combine("Templates", "BLEntity.txt"), outFilePath, model, section);
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

        public void GenerateCode(string templateName, string outFilePath, Dictionary<string, string> model,
            Dictionary<string, string> section = null)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templateName);

            //// Generator
            var codeGenerator = new CodeGenerator();
            var result = codeGenerator.Generator(filePath, model, section);

            //// Write File
            GenerateFile(outFilePath, result);
        }

        
    }

    public struct ProjectInfo
    {
        public string Path { get; set; }
        public string Folder { get; set; }
        public string Suffix { get; set; }
        public string Prefix { get; set; }
    }
}