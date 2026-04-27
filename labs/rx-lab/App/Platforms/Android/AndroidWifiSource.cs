#if ANDROID

using Android.Content;
using Android.Net.Wifi;
using Core.Core.Fingerprints;
using System.Reactive.Subjects;

namespace App;

public sealed class AndroidWifiSource :
    IWifiSource,
    IDisposable
{
    private readonly WifiManager _wifiManager;
    private readonly Context _context;

    private readonly Subject<IReadOnlyList<RadioReading>> _subject =
        new();

    private readonly BroadcastReceiver _receiver;

    public IObservable<IReadOnlyList<RadioReading>> ScanResults =>
        _subject;

    public AndroidWifiSource(Context context)
    {
        _context = context;

        _wifiManager =
            (WifiManager)context.GetSystemService(Context.WifiService)!;

        _receiver = new WifiReceiver(_wifiManager, _subject);

        _context.RegisterReceiver(
            _receiver,
            new IntentFilter(WifiManager.ScanResultsAvailableAction));

        _wifiManager.StartScan();
    }

    public void Dispose()
    {
        try
        {
            _context.UnregisterReceiver(_receiver);
        }
        catch { }

        _subject.OnCompleted();
        _subject.Dispose();
    }

    private sealed class WifiReceiver : BroadcastReceiver
    {
        private readonly WifiManager _manager;
        private readonly IObserver<IReadOnlyList<RadioReading>> _observer;

        public WifiReceiver(
            WifiManager manager,
            IObserver<IReadOnlyList<RadioReading>> observer)
        {
            _manager = manager;
            _observer = observer;
        }

        public override void OnReceive(Context? context, Intent? intent)
        {
            var results = _manager.ScanResults;

            var mapped =
                results.Select(r =>
                        new RadioReading(
                            RadioType.Wifi,
                            r.Bssid,
                            r.Level))
                    .ToList();

            _observer.OnNext(mapped);
        }
    }
}

#endif