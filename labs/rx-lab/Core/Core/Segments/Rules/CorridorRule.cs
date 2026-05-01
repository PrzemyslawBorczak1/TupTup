using IndoorLocalization.Trl3.Core.Motion;

namespace Core.Core.Segments.Rules;

public sealed class CorridorRule : SegmentRuleBase
{
    protected override SegmentType SegmentType => SegmentType.Corridor;

    // Stopped → Walking → Stopped
    protected override bool IsMatch(MotionEvent a, MotionEvent b, MotionEvent c) =>
        a.To == MotionState.Stopped &&
        b.To == MotionState.Walking &&
        c.To == MotionState.Stopped;
}
