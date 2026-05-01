namespace App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}
	
	public string VersionString =>
		$"Build {AppInfo.Current.BuildString}";

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}