using System;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Tokens
{
    public class TypeToken : Token
    {
        public Type Type { get; private set; }

        public TypeToken(string value, int line, int column)
            : base(value, line, column)
        {
            switch (value)
            {
                case TypeCollection.StringType:
                    Type = typeof(string);
                    break;
                case TypeCollection.BoolType:
                    Type = typeof(bool);
                    break;
                case TypeCollection.IntType:
                    Type = typeof(int);
                    break;
                case TypeCollection.DecimalType:
                    Type = typeof(decimal);
                    break;
            }
        }

        public TypeToken(Type type, int line, int column)
            : base("", line, column)
        {
            Type = type;
            if (type == typeof(string))
                StringValue = TypeCollection.StringType;
            if (type == typeof(bool))
                StringValue = TypeCollection.BoolType;
            if (type == typeof(int))
                StringValue = TypeCollection.IntType;
            if (type == typeof(decimal))
                StringValue = TypeCollection.DecimalType;
        }
    }
}
