using System;

namespace DistractScript.Tokens.TokenCollections
{
    public class OperatorCollection : TokenCollection<OperatorCollection>
    {
        public const string Assignment = "=";
        public const string Sum = "+";
        public const string Difference = "-";
        public const string Product = "*";
        public const string Quotient = "/";
        public const string Modulo = "%";
    }
}
