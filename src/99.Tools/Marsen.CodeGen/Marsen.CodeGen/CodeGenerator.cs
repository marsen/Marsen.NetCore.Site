using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Marsen.CodeGen
{
    public class CodeGenerator
    {
        public string Generator(string templateFile, Dictionary<string, string> model, Dictionary<string, string> section)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var line in ReadLineAsync(templateFile))
            {
                var newLine = this.ConvertLine(line, model);
                sb.AppendLine(newLine);
            }

            if (section != null)
            {
                foreach (var key in section.Keys)
                {
                    var keyWord = $"@{key}@";
                    sb.Replace(keyWord, section[key]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Replace Key Word
        /// </summary>
        /// <param name="line"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private string ConvertLine(string line, Dictionary<string, string> model)
        {
            if (model == null) return line;

            foreach (var key in model.Keys)
            {
                var keyWorkd = $"@{key}@";
                if (line.Contains(keyWorkd))
                {
                    line = line.Replace(keyWorkd, model[key]);
                }
            }
            return line;
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

        public static IEnumerable<string> ReadLineAsync(string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}