using System;
using System.Collections.Generic;
using DistractScript.Exceptions;
using DistractScript.Tokens;

namespace DistractScript.Variables
{
    public class VariableCollection
    {
        private List<string> VariableNames;
        private List<Variable<string>> Strings;
        private List<Variable<bool>> Booleans;
        private List<Variable<int>> Integers;
        private List<Variable<decimal>> Decimals;

        public VariableCollection()
        {
            VariableNames = new List<string>();
            Strings = new List<Variable<string>>();
            Booleans = new List<Variable<bool>>();
            Integers = new List<Variable<int>>();
            Decimals = new List<Variable<decimal>>();
        }

        public void AddEmpty(string name, Type type)
        {
            if (VariableExists(name))
                throw new VarAlreadyExistsException(name);

            if (type == typeof(string))
            {
                var variable = new Variable<string>(name);
                Strings.Add(variable);
            }
            else if (type == typeof(bool))
            {
                var variable = new Variable<bool>(name);
                Booleans.Add(variable);
            }
            else if (type == typeof(int))
            {
                var variable = new Variable<int>(name);
                Integers.Add(variable);
            }
            else if (type == typeof(decimal))
            {
                var variable = new Variable<decimal>(name);
                Decimals.Add(variable);
            }

            VariableNames.Add(name);
        }

        public void AddWithValue(string name, LiteralToken literalToken)
        {
            if (VariableExists(name))
                throw new VarAlreadyExistsException(name);

            var type = literalToken.Type;
            if (type == typeof(string))
            {
                var stringLiteral = literalToken as StringLiteral;
                var variable = new Variable<string>(name, stringLiteral.Value);
                Strings.Add(variable);
            }
            else if (type == typeof(bool))
            {
                var boolLiteral = literalToken as BoolLiteral;
                var variable = new Variable<bool>(name, boolLiteral.Value);
                Booleans.Add(variable);
            }
            else if (type == typeof(int))
            {
                var integerLiteral = literalToken as IntegerLiteral;
                var variable = new Variable<int>(name, integerLiteral.Value);
                Integers.Add(variable);
            }
            else if (type == typeof(decimal))
            {
                var decimalLiteral = literalToken as DecimalLiteral;
                var variable = new Variable<decimal>(name, decimalLiteral.Value);
                Decimals.Add(variable);
            }

            VariableNames.Add(name);
        }

        public void AssignValue(string name, LiteralToken literalToken)
        {
            if (!VariableExists(name))
                throw new VarDoesNotExistException(name);

            var type = literalToken.Type;
            if (type == typeof(string))
            {
                var stringLiteral = literalToken as StringLiteral;
                var variable = Strings.Find(v => v.Name == name);
                variable.Set(stringLiteral.Value);
            }
            else if (type == typeof(bool))
            {
                var boolLiteral = literalToken as BoolLiteral;
                var variable = Booleans.Find(v => v.Name == name);
                variable.Set(boolLiteral.Value);
            }
            else if (type == typeof(int))
            {
                var integerLiteral = literalToken as IntegerLiteral;
                var variable = Integers.Find(v => v.Name == name);
                variable.Set(integerLiteral.Value);
            }
            else if (type == typeof(decimal))
            {
                var decimalLiteral = literalToken as DecimalLiteral;
                var variable = Decimals.Find(v => v.Name == name);
                variable.Set(decimalLiteral.Value);
            }
        }

        private bool VariableExists(string name)
        {
            return VariableNames.Contains(name);
        }
    }
}
