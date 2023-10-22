using System;

namespace DistractScript.Exceptions
{
    public class FileFormatException : Exception
    {
        private const string IncorrectFileFormat = "Incorrect file format. DistractScript expects text files with the extension \".adhd\".";

        public FileFormatException()
            : base(IncorrectFileFormat) { }
    }
}
