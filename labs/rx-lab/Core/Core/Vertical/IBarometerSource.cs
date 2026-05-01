namespace Core.Core.Vertical;

public interface IBarometerSource
{
    /// <summary>
    /// Atmospheric pressure in hPa
    /// </summary>
    IObservable<float> Pressure { get; }

    void Start();
    void Stop();
}