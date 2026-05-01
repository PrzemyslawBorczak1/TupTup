namespace Core.Core.Observation.Events;

public sealed record MotionEndEvent(
    Guid SessionId,
    DateTimeOffset Timestamp
) : IRawEvent
{
    public RawEventType Type => RawEventType.MotionEnd;
}