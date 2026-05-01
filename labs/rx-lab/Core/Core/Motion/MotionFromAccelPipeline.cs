using System.Reactive.Linq;

namespace IndoorLocalization.Trl3.Core.Motion;

public sealed class MotionFromAccelPipeline
{
    public IObservable<MotionState> States { get; }

    public MotionFromAccelPipeline(
        IAccelerationSource accelSource,
        TimeSpan window,
        double varianceThreshold)
    {
        States =
            accelSource.Accel
                // 1. Liczymy moduł przyspieszenia |a| = sqrt(ax² + ay² + az²)
                .Select(a =>
                    Math.Sqrt(
                        a.ax * a.ax +
                        a.ay * a.ay +
                        a.az * a.az))

                // 2. okno czasowe
                .Buffer(window)

                // 3. licz wariancję czyli miarę tego, jak bardzo sygnał zmienia się w czasie.
                .Where(samples => samples.Count > 0)
                .Select(samples =>
                {
                    var mean = samples.Average();
                    var variance =
                        samples.Select(x => (x - mean) * (x - mean))
                            .Average();

                    return variance;
                })

                // 4. próg → MotionState
                .Select(v =>
                    v > varianceThreshold
                        ? MotionState.Walking
                        : MotionState.Stopped)

                // 5. stabilizacja
                .DistinctUntilChanged()

                // 6. hot observable
                .Publish()
                .RefCount();
    }
}

public readonly record struct MotionSample(
    long TickIndex,
    MotionState State
);