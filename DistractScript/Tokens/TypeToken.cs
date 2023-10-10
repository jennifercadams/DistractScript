using System;
using DistractScript.TokenCollections;

namespace DistractScript.Tokens
{
    public enum CustomType
    {
        StringType,
        BoolType,
        IntType,
        DecimalType
    }

    public class TypeToken : Token
    {
        public CustomType Type { get; private set; }

        public TypeToken(string value)
            : base(value)
        {
            switch (value)
            {
                case TypeCollection.StringType:
                    Type = CustomType.StringType;
                    break;
                case TypeCollection.BoolType:
                    Type = CustomType.BoolType;
                    break;
                case TypeCollection.IntType:
                    Type = CustomType.IntType;
                    break;
                case TypeCollection.DecimalType:
                    Type = CustomType.DecimalType;
                    break;
            }
        }
    }
}
