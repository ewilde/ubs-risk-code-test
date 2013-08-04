// -----------------------------------------------------------------------
// <copyright file="ConsoleWriter.cs" company="Market Invoice">
// Copyright (c) 2012.
// </copyright>
// -----------------------------------------------------------------------
namespace Ubs.Math.Core.Services
{
    using System;

    /// <summary>
    /// Default implementation of a console writer.
    /// </summary>
    public class ConsoleWriter : IConsoleWriter
    {
        /// <inheritdoc />
        public void WriteInformation(string message, params object[] formattingArguments)
        {
            this.Write(message, ConsoleColor.Cyan, formattingArguments);
        }

        /// <inheritdoc />
        public void WriteDebug(string message, params object[] formattingArguments)
        {
            this.Write(message, ConsoleColor.Gray, formattingArguments);
        }

        /// <inheritdoc />
        public void WriteError(string message, params object[] formattingArguments)
        {
            this.Write(message, ConsoleColor.Red, formattingArguments);
        }

        /// <inheritdoc />
        public void WriteSucess(string message, params object[] formattingArguments)
        {
            this.Write(message, ConsoleColor.Green, formattingArguments);
        }

        /// <inheritdoc />
        public void Write(string message, ConsoleColor color = ConsoleColor.White, params object[] formattingArguments)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message, formattingArguments);
            }
            finally
            {
                Console.ForegroundColor = originalColor;
            }
        }
    }
}