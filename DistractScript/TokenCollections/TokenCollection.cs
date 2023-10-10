using System;
using System.Linq;

namespace DistractScript.TokenCollections
{
    public abstract class TokenCollection<T>
    {
        public static bool Contains(string tokenString)
        {
            var type = typeof(T);
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
