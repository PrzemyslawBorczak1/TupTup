#if ANDROID

using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Core.Core.Fingerprints;
using System.Reactive.Subjects;

namespace App;

public sealed class AndroidBleSource :
    IBleSource,
    IDisposable
{
    private readonly BluetoothLeScanner? _scanner;

    private readonly Subject<IReadOnlyList<RadioReading>> _subject =
        new();

    private readonly Dictionary<string, int> _current =
        new();

    private readonly ScanCallback _callback;

    public IObservable<IReadOnlyList<RadioReading>> ScanResults =>
        _subject;

    public AndroidBleSource(Context context)
    {
        var manager =
            (BluetoothManager)context
                .GetSystemService(Context.BluetoothService)!;

        _scanner = manager.Adapter?.BluetoothLeScanner;

        _callback = new BleCallback(_current, _subject);

        _scanner?.StartScan(_callback);
    }

    public void Dispose()
    {
        _scanner?.StopScan(_callback);

        _subject.OnCompleted();
        _subject.Dispose();
    }

    private sealed class BleCallback : ScanCallback
    {
        private readonly Dictionary<string, int> _current;
        private readonly IObserver<IReadOnlyList<RadioReading>> _observer;

        public BleCallback(
            Dictionary<string, int> current,
            IObserver<IReadOnlyList<RadioReading>> observer)
        {
            _current = current;
            _observer = observer;
        }

        public override void OnScanResult(
            ScanCallbackType callbackType,
            ScanResult? result)
        {
            if (result?.Device?.Address == null)
                return;

            _current[result.Device.Address] = result.Rssi;

            var snapshot =
                _current.Select(kv =>
                        new RadioReading(
                            RadioType.Ble,
                            kv.Key,
                            kv.Value))
                    .ToList();

            _observer.OnNext(snapshot);
        }
    }
}

#endif