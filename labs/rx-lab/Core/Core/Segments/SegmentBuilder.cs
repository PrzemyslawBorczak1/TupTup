using System.Reactive.Linq;
using IndoorLocalization.Trl3.Core.Motion;

namespace Core.Core.Segments;

public sealed class SegmentBuilder
{
    public IObservable<Segment> Segments { get; }

    public SegmentBuilder(
        IObservable<MotionEvent> motionEvents,
        IEnumerable<ISegmentRule> rules)
    {
        ArgumentNullException.ThrowIfNull(motionEvents);
        ArgumentNullException.ThrowIfNull(rules);

        var ruleList = rules.ToList();
        if (ruleList.Count == 0)
            throw new ArgumentException("At least one segment rule is required.", nameof(rules));

        if (ruleList.Any(r => r.WindowSize <= 0))
            throw new ArgumentException("Segment rule WindowSize must be > 0.", nameof(rules));

        // Maksymalny rozmiar okna wymagany przez reguły
        var windowSize = ruleList.Max(r => r.WindowSize);

        Segments =
            motionEvents
                .Buffer(windowSize, 1)
                .Select(window => MatchFirst(ruleList, window))
                .Where(segment => segment is not null)
                .Select(segment => segment!);
    }

    private static Segment? MatchFirst(
        IReadOnlyList<ISegmentRule> rules,
        IList<MotionEvent> window)
    {
        // Sprawdzamy reguły po kolei (kolejność = priorytet)
        foreach (var rule in rules)
        {
            var n = rule.WindowSize;
            if (window.Count < n)
                continue;

            // Bierzemy ostatnie N zdarzeń (sufiks)
            var slice =
                window
                    .Skip(window.Count - n)
                    .ToList();

            var segment = rule.TryMatch(slice);
            if (segment != null)
                return segment;
        }

        return null;
    }
}