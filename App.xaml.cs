namespace signals;

public partial class App : Application
{
	AppShell _shell;
	public App(AppShell shell)
	{
		_shell = shell;
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = new Window
		{
			Page = _shell
		};
		return window;
	}
}