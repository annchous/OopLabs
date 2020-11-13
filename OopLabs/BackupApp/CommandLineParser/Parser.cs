using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackupApp.CommandLineParser.Options;
using BackupApp.Core.Abstractions;
using BackupApp.Exception;

namespace BackupApp.CommandLineParser
{
    class Parser : IParseable
    {
        private readonly IEnumerable<string> _arguments;
        public Parser(IEnumerable<string> arguments)
        {
            this._arguments = arguments;
        }
        public ParsedData Parse()
        {
            var actionType = StaticParser.ParseAction(_arguments.ElementAt(0) 
                                                      ?? throw new WrongArgumentAmountException(0, 1));
            return actionType.ConvertToParseable(_arguments).Parse();
        }
    }
}