﻿namespace Ubs.Math.Server
{
    using System;
    using System.ServiceModel;

    using Ubs.Math.Core.Services;

    class Service
    {
        static void Main(string[] args)
        {
            // Create a ServiceHost for the CalculatorService type
            using (ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService), new Uri("http://localhost:8000/ubs/calculator")))
            {
                // Open the ServiceHost to create listeners and start listening for messages
                serviceHost.Open();

                // The service can now be accessed
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
