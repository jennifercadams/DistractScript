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
        private const string TestDeclareString = "forget infodump bestGirl = \"Wednesday\";";

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

        [TestMethod]
        public void GenerateTokens_Type_String()
        {
            var stringValue = TypeCollection.StringType;
            var type = typeof(string);
            VerifyTypeToken(stringValue, type);
        }

        [TestMethod]
        public void GenerateTokens_Type_Boolean()
        {
            var stringValue = TypeCollection.BoolType;
            var type = typeof(bool);
            VerifyTypeToken(stringValue, type);
        }

        [TestMethod]
        public void GenerateTokens_Type_Integer()
        {
            var stringValue = TypeCollection.IntType;
            var type = typeof(int);
            VerifyTypeToken(stringValue, type);
        }

        [TestMethod]
        public void GenerateTokens_Type_Decimal()
        {
            var stringValue = TypeCollection.DecimalType;
            var type = typeof(decimal);
            VerifyTypeToken(stringValue, type);
        }

        [TestMethod]
        public void GenerateTokens_Literal_String()
        {
            var stringValue = "\"Here's a test string!\"";
            var value = "Here's a test string!";
            var type = typeof(string);

            var tokenizer = new Tokenizer(stringValue);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is StringLiteral);

            var stringLiteral = tokens[0] as StringLiteral;
            Assert.AreEqual(stringValue, stringLiteral.StringValue);
            Assert.AreEqual(value, stringLiteral.Value);
            Assert.AreEqual(type, stringLiteral.Type);
        }

        [TestMethod]
        [DataRow(LiteralCollection.True, true)]
        [DataRow(LiteralCollection.False, false)]
        public void GenerateTokens_Literal_Boolean(string stringValue, bool value)
        {
            var type = typeof(bool);

            var tokenizer = new Tokenizer(stringValue);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is BoolLiteral);

            var boolLiteral = tokens[0] as BoolLiteral;
            Assert.AreEqual(stringValue, boolLiteral.StringValue);
            Assert.AreEqual(value, boolLiteral.Value);
            Assert.AreEqual(type, boolLiteral.Type);
        }

        [TestMethod]
        [DataRow("1", 1)]
        [DataRow("0", 0)]
        [DataRow("-1", -1)]
        [DataRow("42", 42)]
        public void GenerateTokens_Literal_Integer(string stringValue, int value)
        {
            var type = typeof(int);

            var tokenizer = new Tokenizer(stringValue);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is IntegerLiteral);

            var integerLiteral = tokens[0] as IntegerLiteral;
            Assert.AreEqual(stringValue, integerLiteral.StringValue);
            Assert.AreEqual(value, integerLiteral.Value);
            Assert.AreEqual(type, integerLiteral.Type);
        }

        [TestMethod]
        [DataRow("3.14159", 3.14159)]
        [DataRow("1.0", 1.0)]
        [DataRow("0.0", 0.0)]
        [DataRow("-1.0", -1.0)]
        [DataRow("-1.61803", -1.61803)]
        [DataRow("10.0", 10.0)]
        [DataRow("400.5", 400.5)]
        public void GenerateTokens_Literal_Decimal(string stringValue, double doubleValue)
        {
            var value = Convert.ToDecimal(doubleValue);
            var type = typeof(decimal);

            var tokenizer = new Tokenizer(stringValue);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is DecimalLiteral);

            var decimalLiteral = tokens[0] as DecimalLiteral;
            Assert.AreEqual(stringValue, decimalLiteral.StringValue);
            Assert.AreEqual(value, decimalLiteral.Value);
            Assert.AreEqual(type, decimalLiteral.Type);
        }

        [TestMethod]
        public void GenerateTokens_Separator_EndStatement()
        {
            var stringValue = SeparatorCollection.EndStatement;
            VerifySeparatorToken(stringValue);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(10)]
        public void GenerateTokens_WhiteSpace_MultiLineBreak(int lineBreaks)
        {
            var text = TestDeclareString + new string('\n', lineBreaks) + TestDeclareString;

            var tokenizer = new Tokenizer(text);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 12);

            var tokensLine1 = tokens.GetRange(0, 6);
            foreach (var token in tokensLine1)
            {
                var lineNumber = 1;
                Assert.AreEqual(lineNumber, token.Line);
            }

            var tokensLineN = tokens.GetRange(6, 6);
            foreach (var token in tokensLineN)
            {
                var lineNumber = 1 + lineBreaks;
                Assert.AreEqual(lineNumber, token.Line);
            }

        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(10)]
        public void GenerateTokens_WhiteSpace_LeadingLineBreaks(int lineBreaks)
        {
            var text = new string('\n', lineBreaks) + TestDeclareString;
            var lineNumber = lineBreaks + 1;

            var tokenizer = new Tokenizer(text);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 6);

            foreach (var token in tokens)
            {
                Assert.AreEqual(lineNumber, token.Line);
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(10)]
        public void GenerateTokens_WhiteSpace_TrailingLineBreaks(int lineBreaks)
        {
            var text = TestDeclareString + new string('\n', lineBreaks);

            var tokenizer = new Tokenizer(text);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 6);

            foreach (var token in tokens)
            {
                Assert.AreEqual(1, token.Line);
            }
        }

        private void VerifyKeywordToken(string stringValue, Keyword keyword)
        {
            var tokenizer = new Tokenizer(stringValue);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is KeywordToken);

            var keywordToken = tokens[0] as KeywordToken;
            Assert.AreEqual(stringValue, keywordToken.StringValue);
            Assert.AreEqual(keyword, keywordToken.Keyword);
        }

        private void VerifyOperatorToken(string stringValue, Operator operatorType)
        {
            var tokenizer = new Tokenizer(stringValue);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is OperatorToken);

            var operatorToken = tokens[0] as OperatorToken;
            Assert.AreEqual(stringValue, operatorToken.StringValue);
            Assert.AreEqual(operatorType, operatorToken.Operator);
        }

        private void VerifyTypeToken(string stringValue, Type type)
        {
            var tokenizer = new Tokenizer(stringValue);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is TypeToken);

            var typeToken = tokens[0] as TypeToken;
            Assert.AreEqual(stringValue, typeToken.StringValue);
            Assert.AreEqual(type, typeToken.Type);
        }

        private void VerifySeparatorToken(string stringValue)
        {
            var tokenizer = new Tokenizer(stringValue);
            var tokens = tokenizer.GenerateTokens();

            Assert.IsTrue(tokens.Count == 1);
            Assert.IsTrue(tokens[0] is SeparatorToken);

            var separatorToken = tokens[0] as SeparatorToken;
            Assert.AreEqual(stringValue, separatorToken.StringValue);
        }
    }
}
