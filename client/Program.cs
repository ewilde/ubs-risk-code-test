// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="UBS AG">
//   Copyright (c) 2012.
// </copyright>
// <summary>
//   The entry point for the client console application. Responsible for bootstrapping,
//   setting up and kicking off client tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ubs.Math.Client
{
    using System;
    using System.ServiceModel;

    using StructureMap;

    using Ubs.Math.Core.Configuration;
    using Ubs.Math.Core.Services;

    /// <summary>
    /// The entry point for the client console application. Responsible for bootstrapping,
    /// setting up and kicking off client tests.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        /// <param name="consoleWriter">The console writer.</param>
        public Program(IConsoleWriter consoleWriter)
        {
            this.ConsoleWriter = consoleWriter;
        }

        /// <summary>
        /// Gets or sets the console writer.
        /// </summary>
        /// <value>
        /// The console writer.
        /// </value>
        public IConsoleWriter ConsoleWriter { get; set; }

        /// <summary>
        /// Main entry point for the console application, currently no command line
        /// arguments are supported
        /// </summary>
        /// <param name="args">The command line arguments, currently none.</param>
        /// <returns>Returns 0 if the execution path happened as expected; otherwise returns -1.</returns>
        public static int Main(string[] args)
        {
            Bootstrap();

            Program program;
            try
            {
                program = ObjectFactory.GetInstance<Program>();            
            }
            catch (StructureMapException structureMapException)
            {
                Console.WriteLine(structureMapException);
                Console.WriteLine(ObjectFactory.WhatDoIHave());
                return -1;
            }
            
            try
            {
                program.Run();
            }
            catch (EndpointNotFoundException endpointException)
            {
                program.ConsoleWriter.WriteError("Could not connect to server, have you started it?");
                program.ConsoleWriter.WriteDebug(endpointException.ToString());
            }
            catch (Exception exception)
            {
                program.ConsoleWriter.WriteError(exception.ToString());
                return -1;
            }

            program.ConsoleWriter.Write("Press any key to continue");
            Console.ReadKey();
            return 0;
        }

        /// <summary>
        /// Runs this program and carries out all the tests.
        /// </summary>
        public void Run()
        {
            ServiceWrapper<ICalculator>.Use(client =>
                {
                    this.ConsoleWriter.WriteInformation("Sum: " + client.Sum(new[] { 4.2d, 0.3d, 3.3d }));
                });
        }

        /// <summary>
        /// Bootstraps this instance configuring the IoC container.
        /// </summary>
        private static void Bootstrap()
        {
            ObjectFactory.Configure(config => config.AddRegistry<CoreRegistry>());
        }
    }
}
