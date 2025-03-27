using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocalizationResourceManager.Maui;
using Microsoft.Extensions.Logging;
using signals.Models;
using signals.Services;

namespace signals.ViewModels;
public partial class MainViewModel : BaseViewModel
{
    readonly IDataService _dataService;
    readonly ILogger _logger;
    readonly IPopupService _popupService;
    readonly ILocalizationResourceManager _resourceManager;

    public MainViewModel(IDispatcher dispatcher, ILogger<MainViewModel> logger,
        IDataService dataService, IPopupService popupService, ILocalizationResourceManager resourceManager) : base(dispatcher)
    {
        _logger = logger;
        _popupService = popupService;
        _dataService = dataService;
        _resourceManager = resourceManager;
    }

    [RelayCommand]
    async Task LoadData()
    {
        try
        {
            var stocks = await _dataService.FetchData();
            Stocks.Clear();
            foreach (var stock in stocks)
            {
                Stocks.Add(stock);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading data");
          //  await _popupService.ShowPopupAsync("Error", "Failed to load data", "OK");
        }
    }

    [ObservableProperty]
    ObservableCollection<StockItem> _stocks = [];
}