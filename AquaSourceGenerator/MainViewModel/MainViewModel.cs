﻿using System;
using Core;

namespace AquaSourceGenerator
{
    public class MainViewModel : IMainViewModel
    {
        ILogger Logger { get; }

        ICalculator Calculator { get; }
        
        public MainViewModel(ILogger logger, ICalculator calculator)
        {
            Logger = logger;
            Calculator = calculator;
        }

        public void Calculate(int x, int y)
        {
            var result = Calculator.Calculate(x, y);
            Console.WriteLine($"{x} + {y} = {result}");
        }
    }
}