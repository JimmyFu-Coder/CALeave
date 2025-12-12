using CleanLeave.Domain.ValueObjects;
using Xunit;

namespace CleanLeave.Domain.Tests.ValueObjects;

public class TimePeriodTests
{
    [Fact]
    public void Create_Should_Create_TimePeriod_When_Valid()
    {
        var start = new DateTime(2025, 1, 1);
        var end = new DateTime(2025, 1, 2);

        var period = TimePeriod.Create(start, end);

        Assert.Equal(start, period.Start);
        Assert.Equal(end, period.End);
        Assert.Equal(TimeSpan.FromDays(1), period.Duration);
    }

    [Fact]
    public void Create_Should_Throw_When_End_Is_Before_Start()
    {
        var start = new DateTime(2025, 1, 2);
        var end = new DateTime(2025, 1, 1);

        Assert.Throws<ArgumentException>(() =>
            TimePeriod.Create(start, end));
    }
}