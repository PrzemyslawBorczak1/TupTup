namespace IndoorLocalization.Trl3.Core.Motion;

public sealed record TurnEvent(
    TurnDirection Direction,
    TimeSpan Time
);