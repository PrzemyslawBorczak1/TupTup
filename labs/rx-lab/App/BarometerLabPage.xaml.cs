using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using IndoorLocalization.Trl3.App;

namespace App;

public partial class BarometerLabPage : ContentPage
{
    private readonly CompositeDisposable _disposables = new();

#if ANDROID
    private AndroidBarometerSource? _barometer;
#else
    private IBarometerSource? _barometer;
#endif

    private double? _lastPressure;
    private double? _referencePressure;

    // TRL-3: stała wysokość piętra (później można kalibrować)
    private const double FloorHeightMeters = 3.0;

    private string _pressureText = "Pressure: (no data)";
    public string PressureText
    {
        get => _pressureText;
        set { _pressureText = value; OnPropertyChanged(); }
    }

    private string _referenceText = "Reference: (not set)";
    public string ReferenceText
    {
        get => _referenceText;
        set { _referenceText = value; OnPropertyChanged(); }
    }

    private string _deltaText = "Δ Height: (n/a)";
    public string DeltaText
    {
        get => _deltaText;
        set { _deltaText = value; OnPropertyChanged(); }
    }

    private string _floorText = "Floor offset: (n/a)";
    public string FloorText
    {
        get => _floorText;
        set { _floorText = value; OnPropertyChanged(); }
    }

    private string _debugText = "";
    public string DebugText
    {
        get => _debugText;
        set { _debugText = value; OnPropertyChanged(); }
    }

    public BarometerLabPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

#if ANDROID
        _barometer = new AndroidBarometerSource(Android.App.Application.Context);
        _barometer.Start();

        // Stabilizacja TRL-3: średnia z 1 sekundy
        _barometer.Pressure
            .Buffer(TimeSpan.FromSeconds(1))
            .Where(b => b.Count > 0)
            .Select(b => b.Average())
            .Subscribe(avg => MainThread.BeginInvokeOnMainThread(() => OnPressure(avg)))
            .DisposeWith(_disposables);
#endif
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _disposables.Dispose();

#if ANDROID
        _barometer?.Dispose();
        _barometer = null;
#endif
    }

    private void OnSetReferenceClicked(object sender, EventArgs e)
    {
        if (_lastPressure is null)
            return;

        _referencePressure = _lastPressure;

        ReferenceText = $"Reference: {_referencePressure.Value:F2} hPa (0)";
        DeltaText = "Δ Height: 0.00 m";
        FloorText = "Floor offset: 0";
    }

    private void OnPressure(double pressureHpa)
    {
        _lastPressure = pressureHpa;

        PressureText = $"Pressure: {pressureHpa:F2} hPa";

        if (_referencePressure is null)
        {
            DebugText = "Set reference to start Δh estimation.";
            return;
        }

        var deltaH = RelativeAltitudeMeters(pressureHpa, _referencePressure.Value);
        var floorOffset = (int)Math.Round(deltaH / FloorHeightMeters);

        DeltaText = $"Δ Height: {deltaH:F2} m";
        FloorText = $"Floor offset: {floorOffset} (≈ {FloorHeightMeters:F1} m/floor)";

        // mały debug: ile to odpowiada w hPa (intuicja)
        var deltaP = pressureHpa - _referencePressure.Value;
        DebugText = $"ΔP: {deltaP:+0.00;-0.00;0.00} hPa | (avg 1s)";
    }

    /// <summary>
    /// Relative altitude computed from pressure ratio P/P_ref (weather-proof baseline).
    /// Δh = 44330 * (1 - (P / Pref) ^ 0.1903)
    /// </summary>
    private static double RelativeAltitudeMeters(double pressure, double referencePressure)
    {
        return 44330.0 * (1.0 - Math.Pow(pressure / referencePressure, 0.1903));
    }
}

internal static class RxDisposableExtensions
{
    public static void DisposeWith(this IDisposable disposable, CompositeDisposable cd)
        => cd.Add(disposable);
}