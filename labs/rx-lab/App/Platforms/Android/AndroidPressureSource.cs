using System.Reactive.Subjects;
using Android.Content;
using Android.Hardware;
using IndoorLocalization.Trl3.Core.Motion;

namespace App;

public sealed class AndroidPressureSource :
    Java.Lang.Object,
    ISensorEventListener,
    IPressureSource,
    IDisposable
{
    private readonly SensorManager _sensorManager;
    private readonly Sensor? _pressure;
    private readonly Subject<float> _subject = new();
    private volatile bool _disposed;

    public IObservable<float> Pressure => _subject;

    public AndroidPressureSource(Context context)
    {
        _sensorManager =
            (SensorManager)context.GetSystemService(Context.SensorService)!;

        _pressure =
            _sensorManager.GetDefaultSensor(SensorType.Pressure);
    }

    public void Start()
    {
        if (_pressure == null)
            return;

        _sensorManager.RegisterListener(
            this,
            _pressure,
            SensorDelay.Game);
    }

    public void Stop()
    {
        _sensorManager.UnregisterListener(this);
    }

    public void OnSensorChanged(SensorEvent e)
    {
        if (_disposed) return;
        if (e.Sensor.Type == SensorType.Pressure)
        {
            _subject.OnNext(e.Values[0]); // hPa
        }
    }

    public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
    {
        // not needed for TRL-3
    }

    public new void Dispose()
    {
        _disposed = true;
        Stop();
        _subject.OnCompleted();
        _subject.Dispose();
    }
}