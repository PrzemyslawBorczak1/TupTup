using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Text;
using System.Threading.Tasks;
using Core.Core.Fingerprints;
using IndoorLocalization.Trl3.Core.Time;
using Microsoft.Maui.ApplicationModel;

namespace App;

public partial class FingerprintLabPage : ContentPage
{
    
    private IWifiSource? _wifi;
    private IBleSource? _ble;

    private readonly CompositeDisposable _disposables = new();
    private ITickSource? _tickSource;
    
    private readonly FingerprintBuilder _builder = new();
    
    
    public FingerprintLabPage()
    {
        InitializeComponent();
    }
    
#if ANDROID
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var locationStatus =
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        if (locationStatus != PermissionStatus.Granted)
        {
            await DisplayAlert(
                "Permission required",
                "Location permission is required for WiFi/BLE scanning.",
                "OK");
            return;
        }

        var bluetoothStatus =
            await Permissions.RequestAsync<Permissions.Bluetooth>();

        if (bluetoothStatus != PermissionStatus.Granted)
        {
            await DisplayAlert(
                "Permission required",
                "Bluetooth permission is required for BLE scanning.",
                "OK");
            return;
        }

        var context = Android.App.Application.Context;

        _wifi = new AndroidWifiSource(context);
        _ble  = new AndroidBleSource(context);
    }
#endif
    
    private void OnStartClicked(object sender, EventArgs e)
    {
        if (_wifi == null || _ble == null)
            return;

        // ensure previous tickSource is disposed before starting again
        _tickSource?.Dispose();
        _tickSource = null;

        _disposables.Clear();
        _builder.Reset();

        // WiFi stream
        _wifi.ScanResults
            .Subscribe(batch =>
            {
                _builder.AddBatch(RadioType.Wifi, batch);
            })
            .DisposeWith(_disposables);

        // BLE stream
        _ble.ScanResults
            .Subscribe(batch =>
            {
                _builder.AddBatch(RadioType.Ble, batch);
            })
            .DisposeWith(_disposables);

        // co 3 sekundy budujemy snapshot
        _tickSource = new RxTickSource(TimeSpan.FromSeconds(3));
        _tickSource.Start();
        
        _tickSource.Ticks
            .Subscribe(_ =>
            {
                var fp = _builder.Build();
                _builder.Reset();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    FingerprintLabel.Text =
                        string.Join("\n",
                            fp.Tokens
                                .OrderBy(t => t.Type)
                                .ThenBy(t => t.Id)
                                .Select(t =>
                                    $"{t.Type,-5} {t.Bucket,-6} {t.Id}"));
                });
            })
            .DisposeWith(_disposables);
    }

    private void OnStopClicked(object sender, EventArgs e)
    {
        _disposables.Clear();
        _builder.Reset();

        _tickSource?.Dispose();
        _tickSource = null;

        FingerprintLabel.Text = string.Empty;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _disposables.Clear();

        _tickSource?.Dispose();
        _tickSource = null;

#if ANDROID
        (_wifi as IDisposable)?.Dispose();
        (_ble as IDisposable)?.Dispose();

        _wifi = null;
        _ble = null;
#endif
    }

    private void OnCaptureClicked(object sender, EventArgs e)
    {
        
    }

    private void OnCompareClicked(object sender, EventArgs e)
    {
    }

}