using Borg.System.DDD.ValueObjects.Time;
using System;
using Xunit;

namespace Test.Borg.System.DDD.ValueObjects
{
    public class PeriodTests
    {
        [Fact]
        public void start_can_not_be_later_than_end()
        {
            Shouldly.Should.Throw<InvalidOperationException>(() =>
            {
                var p = Period.Create(DateTimeOffset.Parse("2018-01-01"), DateTimeOffset.Parse("2017-01-01"));
            });

            return;
        }
    }
}