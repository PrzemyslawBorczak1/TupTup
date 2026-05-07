namespace TupTrack.UI.Pages.StatisticsPage;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage(StatisticsPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }


    private async void OnAppearance(object? sender, EventArgs e)
    {
        if (BindingContext is StatisticsPageViewModel vm)
            vm.LoadSummaries();
    }
}