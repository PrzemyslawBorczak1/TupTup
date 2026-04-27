using Core.Core.Vertical;

namespace IndoorLocalization.Trl3.App;

#if ANDROID

using Android.Content;
using Android.Hardware;
using System.Reactive.Subjects;

public sealed class AndroidBarometerSource :
    Java.Lang.Object,
    ISensorEventListener,
    IBarometerSource,
    IDisposable
{
    private readonly SensorManager _sensorManager;
    private readonly Sensor? _pressureSensor;

    private readonly Subject<float> _pressure = new();

    public IObservable<float> Pressure => _pressure;

    public AndroidBarometerSource(Context context)
    {
        _sensorManager =
            (SensorManager)context.GetSystemService(Context.SensorService)!;

        _pressureSensor =
            _sensorManager.GetDefaultSensor(SensorType.Pressure);
        
        if (_pressureSensor == null)
        {
            // Brak sensora
            
        }
    }

    public void Start()
    {
        if (_pressureSensor == null)
            return;

        _sensorManager.RegisterListener(
            this,
            _pressureSensor,
            SensorDelay.Normal);
    }

    public void Stop()
    {
        _sensorManager.UnregisterListener(this);
    }

    public void OnSensorChanged(SensorEvent? e)
    {
        if (e?.Sensor?.Type != SensorType.Pressure)
            return;

        // hPa
        var pressure = e.Values[0];

        _pressure.OnNext(pressure);
    }

    public void OnAccuracyChanged(Sensor? sensor, SensorStatus accuracy)
    {
        // opcjonalnie: można emitować accuracy
    }

    public void Dispose()
    {
        Stop();
        _pressure.OnCompleted();
        _pressure.Dispose();
    }
}

#endif