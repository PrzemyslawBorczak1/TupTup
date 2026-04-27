using IndoorLocalization.Trl3.Core.Motion;

namespace Core.Core.Segments.Rules;

public sealed class StopRule : SegmentRuleBase
{
    protected override SegmentType SegmentType => SegmentType.Stop;

    // Walking → Stopped → Walking
    protected override bool IsMatch(MotionEvent a, MotionEvent b, MotionEvent c) =>
        a.To == MotionState.Walking &&
        b.To == MotionState.Stopped &&
        c.To == MotionState.Walking;
}

