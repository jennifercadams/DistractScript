using System;
using DistractScript.Collections.TokenCollections;

namespace DistractScript.Tokens
{
    public enum Operator
    {
        Assignment
    }

    public class OperatorToken : Token
    {
        public Operator Operator { get; private set; }

        public OperatorToken(string value)
            : base(value)
        {
            switch (value)
            {
                case OperatorCollection.Assignment:
                    Operator = Operator.Assignment;
                    break;
            }
        }
    }
}
