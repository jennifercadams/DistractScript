using System;
using System.Collections.Generic;

namespace DistractScript.Variables
{
    public class VariableCollection
    {
        private List<Variable<string>> Strings;
        private List<Variable<bool>> Booleans;
        private List<Variable<int>> Integers;
        private List<Variable<decimal>> Decimals;

        public VariableCollection()
        {
            Strings = new List<Variable<string>>();
            Booleans = new List<Variable<bool>>();
            Integers = new List<Variable<int>>();
            Decimals = new List<Variable<decimal>>();
        }

        public void Add<T>(Variable<T> variable)
        {
            var list = GetList<T>();
            list.Add(variable);
        }

        public Variable<T> Find<T>(string name)
        {
            var list = GetList<T>();
            return list.Find(v => v.Name == name);
        }

        public void AssignValue<T>(Variable<T> variable, T value)
        {
            var found = Find<T>(variable.Name);
            found.Set(value);
        }

        private List<Variable<T>> GetList<T>()
        {
            if (typeof(T) == typeof(string))
            {
                return Strings as List<Variable<T>>;
            }
            else if (typeof(T) == typeof(bool))
            {
                return Booleans as List<Variable<T>>;
            }
            else if (typeof(T) == typeof(int))
            {
                return Integers as List<Variable<T>>;
            }
            else if (typeof(T) == typeof(decimal))
            {
                return Decimals as List<Variable<T>>;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
