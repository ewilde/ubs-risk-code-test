// -----------------------------------------------------------------------
// <copyright file="IConsoleWriter.cs" company="Market Invoice">
// Copyright (c) 2012.
// </copyright>
// -----------------------------------------------------------------------
namespace Ubs.Math.Core.Services
{
    using System;

    /// <summary>
    /// Defines behaviour for a console writer.
    /// </summary>
    public interface IConsoleWriter
    {
        /// <summary>
        /// Writes an information message to the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="formattingArguments">The formatting arguments.</param>
        void WriteInformation(string message, params object[] formattingArguments);

        /// <inheritdoc />
        void WriteDebug(string message, params object[] formattingArguments);

        /// <inheritdoc />
        void WriteError(string message, params object[] formattingArguments);

        /// <inheritdoc />
        void WriteSucess(string message, params object[] formattingArguments);

        void Write(string message, ConsoleColor color = ConsoleColor.White, params object[] formattingArguments);
    }
}