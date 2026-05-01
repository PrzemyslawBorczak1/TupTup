using System.Reactive.Linq;

namespace IndoorLocalization.Trl3.Core.Motion;

public sealed class VerticalMotionPipeline
{
    public IObservable<MotionState> States { get; }

    public VerticalMotionPipeline(
        IPressureSource pressureSource,
        TimeSpan window,
        float deltaThreshold)
    {
        States =
            pressureSource.Pressure
                .Buffer(window)
                .Where(b => b.Count >= 2)
                .Select(b => b.Last() - b.First())
                .Where(delta => Math.Abs(delta) >= deltaThreshold)
                .Select(_ => MotionState.Vertical)
                .DistinctUntilChanged()
                .Publish()
                .RefCount();
    }
}