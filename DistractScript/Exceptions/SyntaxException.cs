using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistractScript.Exceptions
{
    public class SyntaxException : Exception
    {
        public SyntaxException()
            : base("syntax exception message") { }
    }
}
