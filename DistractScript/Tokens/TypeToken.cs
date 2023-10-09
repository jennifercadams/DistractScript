using System;

namespace DistractScript.Tokens
{
    public enum CustomType
    {
        StringType,
        BoolType,
        IntType,
        DecimalType
    }

    class TypeToken : Token
    {
        public CustomType Type { get; private set; }

        public TypeToken(string value)
            : base(value)
        {
            switch (value)
            {
                case "infodump":
                    Type = CustomType.StringType;
                    break;
                case "george":
                    Type = CustomType.BoolType;
                    break;
                case "round":
                    Type = CustomType.IntType;
                    break;
                case "pointy":
                    Type = CustomType.DecimalType;
                    break;
            }
        }
    }
}
