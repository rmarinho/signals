using signals.ViewModels;

namespace signals;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is MainViewModel viewModel)
		{
			await viewModel.LoadDataCommand.ExecuteAsync(null);
		}
	}
}
