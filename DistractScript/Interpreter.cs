using System;
using System.Diagnostics;

namespace DistractScript
{
    public static class Interpreter
    {
        public struct ErrorMessages
        {
            public static string MissingFilename = "You forgot the filename. It's okay, DistractScript forgets things, too.";
            public static string TooManyFilenames = "Sorry, DistractScript can't multitask. Enter exactly one filename.";
        }

        public static void Main(string[] args)
        {
            try
            {
                ValidateArgs(args);
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }

        private static void ValidateArgs(string[] args)
        {
            if (args.Length == 0)
            {
                throw new Exception(ErrorMessages.MissingFilename);
            }
            else if (args.Length > 1)
            {
                throw new Exception(ErrorMessages.TooManyFilenames);
            }
        }

        private static void ProcessException(Exception exception)
        {
            Console.WriteLine(exception.Message);
            Debug.WriteLine(exception.ToString());
        }
    }
}
