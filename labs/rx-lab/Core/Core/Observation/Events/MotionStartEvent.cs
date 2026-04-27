namespace Core.Core.Observation.Events;

public sealed record MotionStartEvent(
    Guid SessionId,
    DateTimeOffset Timestamp
) : IRawEvent
{
    public RawEventType Type => RawEventType.MotionStart;
}


