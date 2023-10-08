using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using DistractScript.Exceptions;

namespace DistractScript
{
    public static class Interpreter
    {
        public static void Main(string[] args)
        {
            try
            {
                var fileName = ParseArgs(args);
                var text = GetFileText(fileName);
                var tokens = Tokenizer.Split(text);

            }
            catch (Exception exception)
            {
                HandleException(exception);
            }
        }

        private static string ParseArgs(string[] args)
        {
            if (args.Length == 0)
            {
                throw new MissingFileNameException();
            }
            else if (args.Length > 1)
            {
                throw new TooManyArgumentsException();
            }

            var fileName = args[0];
            var fileNamePattern = @"^.+\.adhd$";
            if (!Regex.IsMatch(fileName, fileNamePattern))
            {
                throw new FileFormatException();
            }

            return fileName;
        }

        private static string GetFileText(string fileName)
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = $"{directory}\\{fileName}";
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(Resources.ErrorMessages.FileNotFound);
            }

            return File.ReadAllText(filePath);
        }

        private static void HandleException(Exception exception)
        {
            Console.WriteLine(exception.Message);
            Debug.WriteLine(exception.ToString());
        }
    }
}
