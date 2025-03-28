using signals.ViewModels;

namespace signals.Pages;

public partial class TechnicalPage : ContentPage
{
	public TechnicalPage(MainViewModel mainViewModel)
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
