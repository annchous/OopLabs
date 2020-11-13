using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupApp.Core.Implementations.Logger;
using BackupApp.Exception;

namespace BackupApp.CommandLineParser.Options
{
    class Option
    {
        protected readonly IEnumerable<string> arguments;
        protected readonly IEnumerator<string> enumerator;

        protected Option(IEnumerable<string> arguments)
        {
            this.arguments = arguments;
            enumerator = this.arguments.GetEnumerator();
            enumerator.MoveNext();
        }

        protected virtual void CheckArguments(int expected)
        {
            if (arguments.Count() == expected) return;
            var exception = new WrongArgumentAmountException(arguments.Count() + 1, expected + 1);
            BackupLogger.GetInstance().Error(exception.Message);
            throw exception;
        }

        protected string ParseDataFile()
        {
            var result = enumerator.Current;
            enumerator.MoveNext();
            //if (!File.Exists(result + ".dat"))
            //    throw new DataFileNotFoundException(result + ".dat");
            return result;
        }
    }
}
