namespace Core.Core.Observation.Events;

public sealed record OrientationChangeEvent(
    Guid SessionId,
    DateTimeOffset Timestamp,
    float DeltaHeading
) : IRawEvent
{
    public RawEventType Type => RawEventType.OrientationChange;
}