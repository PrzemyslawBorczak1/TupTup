namespace IndoorLocalization.Trl3.Core.Time;

public interface ITickSource : IDisposable
{
    IObservable<Tick> Ticks { get; }
    void Start();
    void Stop();
}