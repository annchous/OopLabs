using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser
{
    class Option
    {
        protected IEnumerable<string> arguments;
        protected IEnumerator<string> enumerator;

        public Option(IEnumerable<string> arguments)
        {
            this.arguments = arguments;
            enumerator = this.arguments.GetEnumerator();
            enumerator.MoveNext();
        }

        protected string ParseDataFile()
        {
            var result = enumerator.Current;
            enumerator.MoveNext();
            if (File.Exists(result))
                throw new FileAlreadyExists(result);
            return result;
        }
    }
}
