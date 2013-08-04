// -----------------------------------------------------------------------
// <copyright file="CalculationFeature.cs" company="UBS AG">
// Copyright (c) 2012.
// </copyright>
// -----------------------------------------------------------------------
namespace client_tests.Integration
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Machine.Specifications;

    using Ubs.Math.Client;
    using Ubs.Math.Core.Services;

    [Subject("Running requests")]
    public class when_connected_to_the_calculation_service : base_service
    {
        Establish context = () => StartService();

        Because of = () => WaitForServiceStart();

        It should_be_possible_to_call_the_sum_operation = ()
            =>
            {
                ServiceWrapper<ICalculator>.Use(client =>
                {
                   client.Sum(new[] { 4.2d, 0.3d, 3.3d }).ShouldEqual(7.8d);
                });
            };

        It should_be_possible_to_call_the_mean_operation = ()
            =>
            {
                ServiceWrapper<ICalculator>.Use(client =>
                {
                   client.Mean(new[] { 4.2d, 0.3d, 3.3d }).ShouldEqual(2.6d);
                });
            };

        It should_be_possible_to_call_the_median_operation = ()
            =>
            {
                ServiceWrapper<ICalculator>.Use(client =>
                {
                   client.Median(new[] { 4.2d, 0.3d, 3.3d }).ShouldEqual(3.3d);
                });
            };

        It should_be_possible_to_call_the_long_running_request_operation = ()
            =>
            {
                ServiceWrapper<ICalculator>.Use(client =>
                {
                   client.LongRunningRequest(new[] { 10d, 5d, 5d }).ShouldBeCloseTo(20d, 5);
                });
            };

        It should_be_possible_to_execute_multiple_requests_from_different_client = ()
            =>
            {
                Parallel.For(0, 30, index =>
                    {
                        ServiceWrapper<ICalculator>.Use(client =>
                        {
                            client.Sum(new[] { 4.2d, 0.3d, 3.3d }).ShouldEqual(7.8d);
                            client.Mean(new[] { 4.2d, 0.3d, 3.3d }).ShouldEqual(2.6d);
                            client.Median(new[] { 4.2d, 0.3d, 3.3d }).ShouldEqual(3.3d);
                        });
                    });
            };

        Cleanup programs = () =>
        {
            StopService();
        };
    }

    public class base_service
    {
        public static void StartService()
        {
            var existingApp = Process.GetProcessesByName("ubs.math.server").FirstOrDefault();
            if (existingApp != null)
            {
                existingApp.Kill();
            }

            Process = 
                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.GetFullPath(@"..\..\..\server\bin\Debug\ubs.math.server.exe"),
                    WindowStyle = ProcessWindowStyle.Normal
                });
        }

        public static void StopService()
        {
            Process.Kill();
        }

        public static void WaitForServiceStart()
        {
            // #Hack use mutex, or IPC to notify waiting clients that service is ready.
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
        }

        protected static Process Process { get; set; }
    }
}