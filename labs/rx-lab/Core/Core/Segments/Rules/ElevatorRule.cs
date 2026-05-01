using IndoorLocalization.Trl3.Core.Motion;

namespace Core.Core.Segments.Rules;

public sealed class ElevatorRule : SegmentRuleBase
{
    protected override SegmentType SegmentType => SegmentType.Elevator;

    // Stopped → Vertical → (Stopped | Walking)
    protected override bool IsMatch(MotionEvent a, MotionEvent b, MotionEvent c) =>
        a.To == MotionState.Stopped &&
        b.To == MotionState.Vertical &&
        (c.To == MotionState.Stopped || c.To == MotionState.Walking);
}
