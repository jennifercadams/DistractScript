using System;

namespace DistractScript.Variables
{
    public class Variable<T>
    {
        public string Name { get; private set; }
        private T Value { get; set; }

        public Variable(string name) 
        {
            Name = name;
            Value = default(T);
        }

        public Variable(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public T Get()
        {
            return Value;
        }

        public void Set(T newValue)
        {
            Value = newValue;
        }
    }
}
