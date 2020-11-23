using System;

namespace Banks.Exceptions
{
    public class UnknownMementoClassException : Exception
    {
        public UnknownMementoClassException(string className) : base($"Unknown memento class {className}.") {}
    }
}