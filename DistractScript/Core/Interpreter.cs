﻿using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using DistractScript.Exceptions;

namespace DistractScript.Core
{
    public static class Interpreter
    {
        public static void Process(string[] args)
        {
            var fileName = ParseArgs(args);
            var text = GetFileText(fileName);

            var tokenizer = new Tokenizer(text);
            var tokens = tokenizer.GenerateTokens();

            var parser = new Parser(tokens);
            var parseTree = parser.BuildParseTree();

            var evaluator = new Evaluator(parseTree);
            evaluator.Process();
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
                var errorMessage = "File not found. Where did you last have it? Maybe try retracing your steps?";
                throw new FileNotFoundException(errorMessage);
            }

            return File.ReadAllText(filePath);
        }
    }
}
