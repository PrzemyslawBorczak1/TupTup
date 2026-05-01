namespace IndoorLocalization.Trl3.Core.Motion;

public sealed record MotionEvent(
    MotionState From,
    MotionState To,
    TimeSpan Time
);