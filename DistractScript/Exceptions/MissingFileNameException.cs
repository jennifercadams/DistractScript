using System;

namespace DistractScript.Exceptions
{
    public class MissingFileNameException : Exception
    {
        private const string MissingFileName = "You forgot the filename. It's okay, DistractScript forgets things, too.";

        public MissingFileNameException()
            : base(MissingFileName) { }
    }
}
