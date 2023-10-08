using System;

namespace DistractScript.Exceptions
{
    class FileFormatException : Exception
    {
        public FileFormatException()
            : base(Resources.ErrorMessages.IncorrectFileFormat) { }
    }
}
