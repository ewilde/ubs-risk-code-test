// -----------------------------------------------------------------------
// <copyright file="CalculatorService.cs" company="UBS AG">
// Copyright (c) 2012.
// </copyright>
// -----------------------------------------------------------------------
namespace Ubs.Math.Core.Services
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Default implemenation of the <see cref="ICalculator"/> service.
    /// </summary>
    public class CalculatorService : ICalculator
    {
        /// <inheritdoc />
        public double Sum(double[] input)
        {
            Console.WriteLine("Calculating sum on thread {0}", Thread.CurrentThread.ManagedThreadId);
            
            double result = input.Sum(item => item);
            if (double.IsInfinity(result))
            {
                throw new OverflowException();
            }

            return result;
        }

        /// <inheritdoc />
        public double Mean(double[] input)
        {
            Console.WriteLine("Calculating mean on thread {0}", Thread.CurrentThread.ManagedThreadId);
            
            if (input.Length == 0)
            {
                return 0;
            }

            return input.Average(item => item);
        }

        /// <inheritdoc />
        public double Median(double[] input)
        {
            Console.WriteLine("Calculating median on thread {0}", Thread.CurrentThread.ManagedThreadId);
            
            if (input.Length == 0)
            {
                return 0;
            }

            var sortedList = input.OrderBy(number => number);

            int count = sortedList.Count();
            int itemIndex = count / 2;
            if (count % 2 == 0) // Even number of items. 
                return (sortedList.ElementAt(itemIndex) +
                        sortedList.ElementAt(itemIndex - 1)) / 2;

            // Odd number of items. 
            return sortedList.ElementAt(itemIndex); 
        }

        /// <inheritdoc />
        public double LongRunningRequest(double[] waitTimes)
        {
            Console.WriteLine("Calculating long running request on thread {0}", Thread.CurrentThread.ManagedThreadId);
            
            if (waitTimes.Length == 0)
            {
                return 0;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Array.ForEach(waitTimes, item =>
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(item));
                });

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}