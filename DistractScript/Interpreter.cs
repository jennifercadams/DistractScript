using System;
using System.Diagnostics;
using DistractScript.Exceptions;

namespace DistractScript
{
    public static class Interpreter
    {
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
                throw new MissingFileNameException();
            }
            else if (args.Length > 1)
            {
                throw new TooManyArgumentsException();
            }
        }

        private static void ProcessException(Exception exception)
        {
            Console.WriteLine(exception.Message);
            Debug.WriteLine(exception.ToString());
        }
    }
}
