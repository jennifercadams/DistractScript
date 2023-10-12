using System;
using DistractScript.Collections.TokenCollections;

namespace DistractScript.Tokens
{
    public enum Keyword
    {
        DeclareVar
    }

    public class KeywordToken : Token
    {
        public Keyword Keyword { get; private set; }

        public KeywordToken(string value, int line)
            : base(value, line)
        {
            switch (value)
            {
                case KeywordCollection.DeclareVar:
                    Keyword = Keyword.DeclareVar;
                    break;
            }
        }
    }
}
