using IndoorLocalization.Trl3.Core.Motion;

namespace Core.Core.Segments.Rules;

// Wzorzec Template Method
// którym reguły segmentacji definiują wyłącznie predykat dopasowania oraz typ segmentu,
// natomiast wspólna polityka tworzenia obiektów segmentów została umieszczona w klasie bazowej.

public abstract class SegmentRuleBase : ISegmentRule
{
    public virtual int WindowSize => 3;

    public Segment? TryMatch(IReadOnlyList<MotionEvent> window)
    {
        ArgumentNullException.ThrowIfNull(window);

        if (window.Count != WindowSize)
            return null;

        var a = window[0];
        var b = window[1];
        var c = window[2];

        if (!IsMatch(a, b, c))
            return null;

        return CreateSegment(a, b, c);
    }

    protected abstract bool IsMatch(
        MotionEvent a,
        MotionEvent b,
        MotionEvent c);
    
    protected abstract SegmentType SegmentType { get; }

    // Domyślna polityka
    protected virtual Segment CreateSegment(
        MotionEvent a,
        MotionEvent b,
        MotionEvent c)
    {
        // segment zaczyna się w stanie "środkowym"
        // i kończy na trzecim zdarzeniu
        return new Segment(
            SegmentType,
            b.Time,
            c.Time);
    }
}
