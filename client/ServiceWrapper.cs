// -----------------------------------------------------------------------
// <copyright file="ServiceWrapper.cs" company="UBS AG">
// Copyright (c) 2012.
// </copyright>
// -----------------------------------------------------------------------
namespace Ubs.Math.Client
{
    using System.ServiceModel;

    /// <summary>
    /// Delegate to run whilst connected to the service
    /// </summary>
    /// <typeparam name="T">Type of service connected to</typeparam>
    /// <param name="proxy">The proxy.</param>
    public delegate void UseServiceDelegate<in T>(T proxy);

    /// <summary>
    /// A service wrapper that makes sure the client proxy is created using a 
    /// channel factory, properly closed or aborted if an error occurs.
    /// Original code slighlty modified from: http://web.archive.org/web/20100703123454/http://old.iserviceoriented.com/blog/post/Indisposable+-+WCF+Gotcha+1.aspx
    /// </summary>
    /// <typeparam name="T">Type of service to create a client proxy</typeparam>
    public static class ServiceWrapper<T>
    {
        /// <summary>
        /// The channel factory
        /// </summary>
        private static readonly ChannelFactory<T> ChannelFactory = new ChannelFactory<T>(string.Empty);

        /// <summary>
        /// Connects to the service and calls specified code block, then disconnects and cleans up.
        /// </summary>
        /// <param name="codeBlock">The code block to call when connected.</param>
        public static void Use(UseServiceDelegate<T> codeBlock)
        {
            IClientChannel proxy = (IClientChannel)ChannelFactory.CreateChannel();
            bool success = false;
            try
            {
                codeBlock((T)proxy);
                proxy.Close();
                success = true;
            }
            finally
            {
                if (!success)
                {
                    proxy.Abort();
                }
            }
        }
    }
}