using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App;

namespace App;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    
    private async void OnMotionLabClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MotionLabPage());
    }

    private async void OnFingerprintLabClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FingerprintLabPage());
    }
    
    private async void OnBarometerLabClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BarometerLabPage());
    }
}