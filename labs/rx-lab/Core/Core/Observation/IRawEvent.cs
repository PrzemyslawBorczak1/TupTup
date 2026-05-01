namespace Core.Core.Observation;

public interface IRawEvent
{
    Guid SessionId { get; }
    DateTimeOffset Timestamp { get; }
    RawEventType Type { get; }
}