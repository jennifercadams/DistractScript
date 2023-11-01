using System;
using System.Collections.Generic;
using DistractScript.Core;
using DistractScript.Data;
using DistractScript.Exceptions;
using DistractScript.Tokens;
using DistractScript.Tokens.TokenCollections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DistractScriptTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void BuildParseTree_DeclareEmptyVar_String()
        {
            var typeToken = new TypeToken(TypeCollection.StringType, 0, 0);
            var variableNameToken = new VariableName("emptyString", typeof(string), 0, 0);
            BuildParseTree_DeclareEmptyVar(typeToken, variableNameToken);
        }

        [TestMethod]
        public void BuildParseTree_DeclareEmptyVar_Boolean()
        {
            var typeToken = new TypeToken(TypeCollection.BoolType, 0, 0);
            var variableNameToken = new VariableName("emptyBoolean", typeof(bool), 0, 0);
            BuildParseTree_DeclareEmptyVar(typeToken, variableNameToken);
        }

        [TestMethod]
        public void BuildParseTree_DeclareEmptyVar_Integer()
        {
            var typeToken = new TypeToken(TypeCollection.IntType, 0, 0);
            var variableNameToken = new VariableName("emptyInteger", typeof(int), 0, 0);
            BuildParseTree_DeclareEmptyVar(typeToken, variableNameToken);
        }

        [TestMethod]
        public void BuildParseTree_DeclareEmptyVar_Decimal()
        {
            var typeToken = new TypeToken(TypeCollection.DecimalType, 0, 0);
            var variableNameToken = new VariableName("emptyDecimal", typeof(decimal), 0, 0);
            BuildParseTree_DeclareEmptyVar(typeToken, variableNameToken);
        }

        [TestMethod]
        public void BuildParseTree_DeclareEmptyVar_VarAlreadyExists()
        {
            var keywordToken = new KeywordToken(KeywordCollection.DeclareVar, 0, 0);
            var typeToken = new TypeToken(TypeCollection.StringType, 0, 0);
            var variableNameToken = new VariableName("emptyString", typeof(string), 0, 0);
            var separatorToken = new SeparatorToken(SeparatorCollection.EndStatement, 0, 0);
            var declareVarTokens = new List<Token> { keywordToken, typeToken, variableNameToken, separatorToken };

            var tokens = new List<Token>();
            tokens.AddRange(declareVarTokens);
            tokens.AddRange(declareVarTokens);

            var parser = new Parser(tokens);
            void action() => parser.BuildParseTree();

            Assert.ThrowsException<VarAlreadyExistsException>(action);
        }

        private void BuildParseTree_DeclareEmptyVar(TypeToken typeToken, VariableName variableNameToken)
        {
            var keywordToken = new KeywordToken(KeywordCollection.DeclareVar, 0, 0);
            var separatorToken = new SeparatorToken(SeparatorCollection.EndStatement, 0, 0);
            var tokens = new List<Token> { keywordToken, typeToken, variableNameToken, separatorToken };

            var parser = new Parser(tokens);
            var parseTree = parser.BuildParseTree();

            VerifyTokensListIsEmpty(tokens);

            var expectedBlockCount = 1;
            Assert.AreEqual(expectedBlockCount, parseTree.Nodes.Count);

            var blockModel = new BlockModel
            {
                ChildCount = 4,
                Keyword = Keyword.DeclareVar,
                Command = Command.DeclareEmptyVar,
                Token = keywordToken,
                TypeToken = typeToken,
                VariableNameToken = variableNameToken
            };
            var actualBlock = parseTree.Nodes[0];
            VerifyBlock(blockModel, actualBlock);
        }

        private void VerifyTokensListIsEmpty(List<Token> tokens)
        {
            Assert.IsTrue(tokens.Count == 0);
        }

        private void VerifyBlock(BlockModel expected, BlockNode actual)
        {
            Assert.AreEqual(expected.ChildCount, actual.Children.Count);
            Assert.AreEqual(expected.Keyword, actual.Keyword);
            Assert.AreEqual(expected.Command, actual.Command);
            Assert.AreEqual(expected.Token, actual.Token);
            Assert.AreEqual(expected.ExpressionTokens, actual.ExpressionTokens);
            Assert.AreEqual(expected.LiteralToken, actual.LiteralToken);
            Assert.AreEqual(expected.TypeToken, actual.TypeToken);
            Assert.AreEqual(expected.VariableNameToken, actual.VariableNameToken);
        }
    }

    public class BlockModel
    {
        public int ChildCount { get; set; }
        public Keyword Keyword { get; set; }
        public Command Command { get; set; }
        public Token Token { get; set; }
        public List<Token> ExpressionTokens { get; set; }
        public LiteralToken LiteralToken { get; set; }
        public TypeToken TypeToken { get; set; }
        public VariableName VariableNameToken { get; set; }
    }
}
