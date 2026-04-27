namespace IndoorLocalization.Trl3.Core.Motion;

public interface IGyroSource
{
    /// <summary>
    /// Angular velocity around Z axis in rad/s
    /// </summary>
    IObservable<double> OmegaZ { get; }
}