// -----------------------------------------------------------------------
// <copyright file="ICalculator.cs" company="UBS AG">
// Copyright (c) 2012.
// </copyright>
// -----------------------------------------------------------------------
namespace Ubs.Math.Core.Services
{
    using System.ServiceModel;

    /// <summary>
    /// Defines the behaviour of maths calculations that can be performed
    /// by this service.
    /// </summary>
    [ServiceContract(Namespace = ServiceConstants.UbsMathNamespace)]
    public interface ICalculator
    {
        /// <summary>
        /// Should sum the input values - e.g. {1,2,3,4} => 10
        /// </summary>
        /// <param name="input">The array of input values</param>
        /// <returns>the sum of the input values</returns>
        [OperationContract]
        double Sum(double[] input);

        /// <summary>
        /// Should calculate the average (mean) of the input values - 
        /// e.g. {1,2,3,4} => 2.5
        /// </summary>
        /// <param name="input">The array of input values</param>
        /// <returns>the arithmetic mean</returns>
        [OperationContract]
        double Mean(double[] input);

        /// <summary>
        /// Should calculate the median of the input values - 
        /// e.g. {1,2,3} => 2
        /// 
        /// If the input array contains an even number of elements - 
        /// the result should be the mean of the two central elements
        /// e.g. {1,90,4400,91} => 90.5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperationContract]
        double Median(double[] input);

        /// <summary>
        /// Should iterate through the input array - waiting for 
        /// n milliseconds (where n is the value at the current 
        /// point in the iteration) before advancing to the 
        /// next value.
        /// e.g. {1,2,3} => wait 1ms, wait 2ms, wait 3ms => return 6
        /// </summary>
        /// <param name="waitTimes">an array of wait times</param>
        /// <returns>the total time waited (in ms)</returns>
        [OperationContract]
        double LongRunningRequest(double[] waitTimes);
    }
}