using System;
using System.IO;
using DistractScript.Core;
using DistractScript.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DistractScriptTests
{
    [TestClass]
    public class InterpreterTests
    {
        [TestMethod]
        public void Process_EmptyArguments()
        {
            var arguments = Array.Empty<string>();
            Action action = () => Interpreter.Process(arguments);

            Assert.ThrowsException<MissingFileNameException>(action);
        }

        [TestMethod]
        [DataRow(new string[] { "test.adhd", "test.adhd" })]
        [DataRow(new string[] { "test1", "test.adhd" })]
        [DataRow(new string[] { "test.adhd", "test1" })]
        [DataRow(new string[] { "test1", "test2", "test3" })]
        public void Process_MultipleArguments(string[] arguments)
        {
            Action action = () => Interpreter.Process(arguments);

            Assert.ThrowsException<TooManyArgumentsException>(action);
        }

        [TestMethod]
        [DataRow(".adhd")]
        [DataRow("test.adhdtest")]
        [DataRow("test.txt")]
        [DataRow("test123")]
        public void Process_InvalidFileName(string fileName)
        {
            var arguments = new string[] { fileName };
            Action action = () => Interpreter.Process(arguments);

            Assert.ThrowsException<FileFormatException>(action);
        }

        [TestMethod]
        public void Process_ValidFileName_FileDoesNotExist()
        {
            var arguments = new string[] { "doesnotexist.adhd" };
            Action action = () => Interpreter.Process(arguments);

            Assert.ThrowsException<FileNotFoundException>(action);
        }
    }
}
