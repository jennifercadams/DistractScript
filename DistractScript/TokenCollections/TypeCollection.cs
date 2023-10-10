using System;
using System.Linq;

namespace DistractScript.TokenCollections
{
    public static class TypeCollection
    {
        public const string StringType = "infodump";
        public const string BoolType = "george";
        public const string IntType = "round";
        public const string DecimalType = "pointy";

        public static bool Contains(string tokenString)
        {
            var type = typeof(TypeCollection);
            var fields = type.GetFields().Where(f => f.FieldType == typeof(string));
            foreach (var field in fields)
            {
                var fieldValue = field.GetValue(null).ToString();
                if (fieldValue == tokenString)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
