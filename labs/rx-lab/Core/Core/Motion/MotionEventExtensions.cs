using System.Reactive.Linq;

namespace IndoorLocalization.Trl3.Core.Motion;

public static class MotionEventExtensions
{
    public static IObservable<MotionEvent> ToMotionEvents(
        this IObservable<MotionState> states,
        IObservable<TimeSpan> time)
    {
        return states
            .DistinctUntilChanged()
            .WithLatestFrom(
                time,
                (state, t) => (State: state, Time: t))
            .Buffer(2, 1)                 // ← kluczowe
            .Where(buffer => buffer.Count == 2)
            .Select(buffer => new MotionEvent(
                From: buffer[0].State,
                To:   buffer[1].State,
                Time: buffer[1].Time
            ));
    }
}