using System.Reactive.Linq;

namespace IndoorLocalization.Trl3.Core.Motion;

public sealed class TurnDetectionPipeline
{
    public IObservable<TurnDirection> Turns { get; }

    public TurnDetectionPipeline(
        IGyroSource gyroSource,
        TimeSpan window,
        double omegaThreshold,
        double minConfidence = 0.7,
        TimeSpan cooldown = default)
    {
        if (cooldown == default)
            cooldown = TimeSpan.FromSeconds(2);

        var kalman = new Kalman1D();

        var omegaIntent =
            gyroSource.OmegaZ
                .ApplyKalman(kalman)
                .Publish()
                .RefCount();

        // 1. Okno czasowe
        var candidates =
            omegaIntent
                .Buffer(window)
                .Where(w => w.Count > 0)
                .Select(samples =>
                {
                    var avg = samples.Average();
                    var magnitude = Math.Abs(avg);

                    var variance =
                        samples.Select(x => x)
                            .Variance();

                    var confidence =
                        magnitude / (magnitude + variance + 1e-5);

                    return new
                    {
                        Direction =
                            avg > 0
                                ? TurnDirection.Left
                                : TurnDirection.Right,
                        Magnitude = magnitude,
                        Confidence = confidence
                    };
                })
                .Where(x =>
                    x.Magnitude > omegaThreshold &&
                    x.Confidence > minConfidence);

        // 2. Anty-flapping (cooldown)
        Turns =
            candidates
                .Select(x => x.Direction)
                .Throttle(cooldown)
                .Publish()
                .RefCount();
    }
}