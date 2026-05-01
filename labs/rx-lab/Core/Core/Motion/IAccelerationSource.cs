using System.Reactive.Linq;

namespace IndoorLocalization.Trl3.Core.Motion;

public interface IAccelerationSource
{
    /// <summary>
    /// Acceleration in m/s^2 (ax, ay, az)
    /// </summary>
    IObservable<(float ax, float ay, float az)> Accel { get; }
}