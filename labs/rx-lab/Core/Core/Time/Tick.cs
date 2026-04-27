namespace IndoorLocalization.Trl3.Core.Time;

public readonly record struct Tick(
    long Index,
    DateTimeOffset Timestamp
);