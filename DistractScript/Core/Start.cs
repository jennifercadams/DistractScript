using System;
using System.Diagnostics;

namespace DistractScript.Core
{
    public class Start
    {
        public static void Main(string[] args)
        {
            try
            {
                Interpreter.Process(args);
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }
        }

        private static void HandleException(Exception exception)
        {
            Console.WriteLine(exception.Message);
            Debug.WriteLine(exception.ToString());
        }
    }
}
