﻿using System;
using System.Collections.Generic;
using System.Linq;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    class HybridAlgorithm : Algorithm
    {
        private readonly List<Algorithm> _algorithms;
        private readonly CombinationType _combinationType;
        public HybridAlgorithm(List<Algorithm> algorithms, CombinationType combinationType) : base(AlgorithmType.Hybrid)
        {
            _algorithms = algorithms;
            _combinationType = combinationType;
        }

        public override int Calculate(Backup backup)
        {
            var values = new List<int>();
            _algorithms.ForEach(x => values.Add(x.Calculate(backup)));
            return _combinationType switch
            {
                CombinationType.Max => values.Max(),
                CombinationType.Min => values.Min(),
                CombinationType.Unknown => throw new ArgumentException(),
                _ => 0
            };
        }
    }
}
