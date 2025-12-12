namespace CleanLeave.Domain.ValueObjects;

public sealed class TimePeriod
{
    public DateTime Start { get; }
    public DateTime End { get; }

    private TimePeriod(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public static TimePeriod Create(DateTime start, DateTime end)
    {
        if (start == default)
            throw new ArgumentException("Start time is required", nameof(start));

        if (end == default)
            throw new ArgumentException("End time is required", nameof(end));

        if (end < start)
            throw new ArgumentException("End time must be after start time");

        return new TimePeriod(start, end);
    }

    public TimeSpan Duration => End - Start;
}