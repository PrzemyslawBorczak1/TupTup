namespace IndoorLocalization.Trl3.Core.Motion;

public interface IPressureSource
{
    /// <summary>
    /// Atmospheric pressure in hPa
    /// </summary>
    IObservable<float> Pressure { get; }
}