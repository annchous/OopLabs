using System;

namespace Banks.Exceptions
{
    public class UnknownMementoClassException : Exception
    {
        public UnknownMementoClassException(String className) : base($"Unknown memento class {className}.") 
        {}
    }
}