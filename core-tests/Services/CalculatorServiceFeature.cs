// -----------------------------------------------------------------------
// <copyright file="CalculatorServiceFeature.cs" company="UBS AG">
// Copyright (c) 2012.
// </copyright>
// -----------------------------------------------------------------------

namespace Ubs.Math.Core.Test.Services
{
    using System;

    using Machine.Specifications;
    using Machine.Fakes;

    using Ubs.Math.Core.Services;

    [Subject(typeof(CalculatorService))]
    public class when_adding_items : WithSubject<CalculatorService>
    {
        It should_return_zero_for_an_empty_array_of_items = () 
            => Subject.Sum(new double[0]).ShouldEqual(0);
        
        It should_sum_an_array_of_doubles_correctly = () 
            => Subject.Sum(new[] { 2.0d, 4.2d }).ShouldEqual(6.2d);

        It should_throw_an_overflow_exceptions_when_the_result_is_greater_than_the_maximum_allowed_for_double = () 
            => Catch.Exception(()=> Subject.Sum(new[] { double.MaxValue, double.MaxValue })).ShouldBeOfType<OverflowException>();
    }

    [Subject(typeof(CalculatorService))]
    public class when_calculating_the_mean_of_items : WithSubject<CalculatorService>
    {
        It should_return_zero_for_an_empty_array_of_items = () 
            => Subject.Mean(new double[0]).ShouldEqual(0);
        
        It should_calculate_the_mean_of_an_array_of_doubles_correctly = () 
            => Subject.Mean(new[] { 2.0d, 4.2d }).ShouldEqual(3.1d);
    }

    [Subject(typeof(CalculatorService))]
    public class when_calculating_the_median_of_items : WithSubject<CalculatorService>
    {
        It should_return_zero_for_an_empty_array_of_items = ()
            => Subject.Median(new double[0]).ShouldEqual(0);

        It should_calculate_the_meadian_of_an_array_containing_an_even_number_of_doubles_correctly = ()
            => Subject.Median(new[] { 1d, 90d, 4400d, 91d }).ShouldEqual(90.5d);

        It should_calculate_the_meadian_of_an_array_containing_an_odd_number_of_doubles_correctly = ()
            => Subject.Median(new[] { 1d, 90d, 4400d, 91d, 2d }).ShouldEqual(90d);
    }

    [Subject(typeof(CalculatorService))]
    public class when_performing_long_running_requests : WithSubject<CalculatorService>
    {
        It should_return_zero_for_an_empty_array_of_items = ()
            => Subject.LongRunningRequest(new double[0]).ShouldEqual(0);

        It should_return_the_total_amount_of_time_waited_in_milliseconds = ()
            => Subject.LongRunningRequest(new[] { 10d, 90d, 100d }).ShouldBeCloseTo(200d, 20d);
    }
}