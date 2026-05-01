using IndoorLocalization.Trl3.Core.Motion;

namespace Core.Core.Segments;

public interface ISegmentRule
{
    /// <summary>How many MotionEvents this rule needs (e.g. 3).</summary>
    int WindowSize { get; }

    /// <summary>
    /// Try to produce a segment from the sliding window.
    /// Return null if no match.
    /// </summary>
    Segment? TryMatch(IReadOnlyList<MotionEvent> window);
}