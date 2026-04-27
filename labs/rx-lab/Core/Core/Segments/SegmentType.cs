using System;

namespace Core.Core.Segments;

public enum SegmentType
{
    Corridor,
    Stop,
    Elevator
}

public sealed record Segment(
    SegmentType Type,
    TimeSpan Start,
    TimeSpan End)
{
    public TimeSpan Duration => End - Start;
}
