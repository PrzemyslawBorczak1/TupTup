using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace IndoorLocalization.Trl3.Core.Time;

public sealed class RxTickSource : ITickSource
{
    private readonly TimeSpan _period;
    private readonly Subject<Tick> _subject = new();

    private IDisposable? _subscription;
    private long _index;

    public IObservable<Tick> Ticks => _subject; 

    public RxTickSource(TimeSpan? period = null)
    {
        _period = period ?? TimeSpan.FromSeconds(1);
    }

    public void Start()
    {
        if (_subscription != null)
            return;

        _subscription =
            Observable.Interval(_period)
                .Subscribe(_ =>
                {
                    var tick = new Tick(
                        Index: Interlocked.Increment(ref _index),
                        Timestamp: DateTimeOffset.UtcNow);

                    _subject.OnNext(tick);
                });
    }

    public void Stop()
    {
        _subscription?.Dispose();
        _subscription = null;
    }

    public void Dispose()
    {
        Stop();
        _subject.OnCompleted();
        _subject.Dispose();
    }
}