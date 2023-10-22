using System;
using DistractScript.Core;
using DistractScript.Tokens;
using DistractScript.Tokens.TokenCollections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DistractScriptTests
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void GenerateTokens_Keyword_DeclareVar()
        {
            var stringValue = KeywordCollection.DeclareVar;
            var keyword = Keyword.DeclareVar;
            VerifyKeywordToken(stringValue, keyword);
        }

        [TestMethod]
        public void GenerateTokens_Operator_Assignment()
        {
            var stringValue = OperatorCollection.Assignment;
            var operatorType = Operator.Assignment;
            VerifyOperatorToken(stringValue, operatorType);
        }

        [TestMethod]
        public void GenerateTokens_Operator_Sum()
        {
            var stringValue = OperatorCollection.Sum;
            var operatorType = Operator.Sum;
            VerifyOperatorToken(stringValue, operatorType);
        }

        [TestMethod]
        public void GenerateTokens_Operator_Difference()
        {
            var stringValue = OperatorCollection.Difference;
            var operatorType = Operator.Difference;
            VerifyOperatorToken(stringValue, operatorType);
        }

        [TestMethod]
        public void GenerateTokens_Operator_Product()
        {
            var stringValue = OperatorCollection.Product;
            var operatorType = Operator.Product;
            VerifyOperatorToken(stringValue, operatorType);
        }

        [TestMethod]
        public void GenerateTokens_Operator_Quotient()
        {
            var stringValue = OperatorCollection.Quotient;
            var operatorType = Operator.Quotient;
            VerifyOperatorToken(stringValue, operatorType);
        }

        [TestMethod]
        public void GenerateTokens_Operator_Modulo()
        {
            var stringValue = OperatorCollection.Modulo;
            var operatorType = Operator.Modulo;
            VerifyOperatorToken(stringValue, operatorType);
        }

        private void VerifyKeywordToken(string stringValue, Keyword keyword)
        {
            var tokens = Tokenizer.GenerateTokens(stringValue);

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is KeywordToken);

            var keywordToken = tokens[0] as KeywordToken;
            Assert.IsTrue(keywordToken.StringValue == stringValue);
            Assert.IsTrue(keywordToken.Keyword == keyword);
        }

        private void VerifyOperatorToken(string stringValue, Operator operatorType)
        {
            var tokens = Tokenizer.GenerateTokens(stringValue);

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is OperatorToken);

            var operatorToken = tokens[0] as OperatorToken;
            Assert.IsTrue(operatorToken.StringValue == stringValue);
            Assert.IsTrue(operatorToken.Operator == operatorType);
        }

    }
}
