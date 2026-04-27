namespace Core.Core.Observation.Events;

public sealed record PressureChangeEvent(
    Guid SessionId,
    DateTimeOffset Timestamp,
    double DeltaPressure
) : IRawEvent
{
    public RawEventType Type => RawEventType.PressureChange;
}