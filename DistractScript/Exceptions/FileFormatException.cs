using System;
using DistractScript.Resources;

namespace DistractScript.Exceptions
{
    class FileFormatException : Exception
    {
        public FileFormatException()
            : base(ErrorMessages.IncorrectFileFormat) { }
    }
}
