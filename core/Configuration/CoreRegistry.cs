// -----------------------------------------------------------------------
// <copyright file="CoreRegistry.cs" company="UBS AG">
// Copyright (c) 2012.
// </copyright>
// -----------------------------------------------------------------------
namespace Ubs.Math.Core.Configuration
{
    using StructureMap.Configuration.DSL;

    using Ubs.Math.Core.Services;

    /// <summary>
    /// Core registry, used to configure DI for this library
    /// </summary>
    public class CoreRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreRegistry"/> class.
        /// </summary>
        public CoreRegistry()
        {
            this.Scan(x =>
                {
                    x.WithDefaultConventions();
                    x.TheCallingAssembly();
                    x.IncludeNamespaceContainingType<IConsoleWriter>();
                });
        }
    }
}