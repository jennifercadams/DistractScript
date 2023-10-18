using System;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Tokens
{
    public enum Operator
    {
        Assignment,
        Sum,
        Difference,
        Product,
        Quotient,
        Modulo
    }

    public class OperatorToken : Token
    {
        public Operator Operator { get; private set; }

        public OperatorToken(string value, int line, int column)
            : base(value, line, column)
        {
            switch (value)
            {
                case OperatorCollection.Assignment:
                    Operator = Operator.Assignment;
                    break;
                case OperatorCollection.Sum:
                    Operator = Operator.Sum;
                    break;
                case OperatorCollection.Difference:
                    Operator = Operator.Difference;
                    break;
                case OperatorCollection.Product:
                    Operator = Operator.Product;
                    break;
                case OperatorCollection.Quotient:
                    Operator = Operator.Quotient;
                    break;
                case OperatorCollection.Modulo:
                    Operator = Operator.Modulo;
                    break;
            }
        }
    }
}
