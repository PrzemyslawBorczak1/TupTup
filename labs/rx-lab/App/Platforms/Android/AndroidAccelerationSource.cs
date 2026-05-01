using System.Reactive.Subjects;
using Android.Content;
using Android.Hardware;
using IndoorLocalization.Trl3.Core.Motion;

namespace App;

public sealed class AndroidAccelerationSource :
    Java.Lang.Object,
    IAccelerationSource,
    ISensorEventListener,
    IDisposable
{
    private readonly SensorManager _sensorManager;
    private readonly Sensor? _accelerometer;

    private readonly Subject<(float ax, float ay, float az)> _subject = new();

    private bool _isRegistered;
    private volatile bool _disposed;

    public IObservable<(float ax, float ay, float az)> Accel => _subject;

    public AndroidAccelerationSource(Context context)
    {
        _sensorManager = (SensorManager)
            context.GetSystemService(Context.SensorService)!;

        _accelerometer = _sensorManager
            .GetDefaultSensor(SensorType.Accelerometer);
    }

    // ---------- lifecycle ----------

    public void Start()
    {
        if (_accelerometer == null) return;
        if (_isRegistered) return;

        _sensorManager.RegisterListener(
            (ISensorEventListener)this,
            _accelerometer,
            SensorDelay.Game
        );

        _isRegistered = true;
    }

    public void Stop()
    {
        if (!_isRegistered) return;

        _sensorManager.UnregisterListener(
            (ISensorEventListener)this,
            _accelerometer
        );

        _isRegistered = false;
    }

    // ---------- ISensorEventListener ----------

    public void OnSensorChanged(SensorEvent? e)
    {
        if (_disposed) return;
        if (e?.Sensor?.Type != SensorType.Accelerometer)
            return;

        _subject.OnNext((
            e.Values[0],
            e.Values[1],
            e.Values[2]
        ));
    }

    public void OnAccuracyChanged(
        Sensor? sensor,
        SensorStatus accuracy)
    {
        // ignore
    }

    // ---------- cleanup ----------

    public new void Dispose()
    {
        _disposed = true;
        Stop();
        _subject.OnCompleted();
        _subject.Dispose();
    }
}