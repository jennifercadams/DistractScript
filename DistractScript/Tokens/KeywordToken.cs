using System;
using DistractScript.Collections.TokenCollections;

namespace DistractScript.Tokens
{
    public enum Keyword
    {
        DeclareVar
    }

    class KeywordToken : Token
    {
        public Keyword Keyword { get; private set; }

        public KeywordToken(string value)
            : base(value)
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
