namespace Core.Core.Observation.Events;

public sealed record BleScanEvent(
    Guid SessionId,
    DateTimeOffset Timestamp,
    IReadOnlyList<BleReading> Readings
) : IRawEvent
{
    public RawEventType Type => RawEventType.BleScan;
}