using System;
using DistractScript.Tokens.TokenCollections;

namespace DistractScript.Tokens
{
    public enum Keyword
    {
        None,
        DeclareVar
    }

    public class KeywordToken : Token
    {
        public Keyword Keyword { get; private set; }

        public KeywordToken(string value, int line, int column)
            : base(value, line, column)
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
