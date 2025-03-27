using System;
using System.Collections.ObjectModel;
using signals.Models;

namespace signals.Services;

public class DataService : IDataService
{
    public DataService()
    {
        // Initialize any necessary data or services here
    }

    public Task<IList<StockItem>> FetchData()
    {
        return Task.FromResult<IList<StockItem>>(Stocks);
    }

    public ObservableCollection<StockItem> Stocks { get; set; } = new()
        {
            new StockItem { Symbol = "OKLO", Price = 38.79m,  PriceChange = -3.98m, Pattern= "Rising wedge", MarketRegime = "High Volatility", Confidence = 0.438, TrendStrength = 0.43, IsPositive = true, 
                BaseSize = 30, AdjustedSize = 21, RiskFactor = 0.1, EnhancedAnalysis = "Momentum", EnhancedAnalysisValue= -0.16, SpyCorrelation = 0.40, SectorExposure = 0.0 },
            new StockItem { Symbol = "TSLA", Price = 337.80m, PriceChange = 15.28m, MarketRegime = "High Volatility", Confidence = 0.375, TrendStrength = 0.48, IsPositive = true,
                BaseSize = 9, AdjustedSize = 6, RiskFactor = 0.1, EnhancedAnalysis = "Momentum", EnhancedAnalysisValue= 0.00, SpyCorrelation = 0.59, SectorExposure = 0.0},
            new StockItem { Symbol = "PLTR", Price = 101.35m, PriceChange = -3.82m, MarketRegime = "High Volatility", Confidence = 0.344, TrendStrength = 0.16, IsPositive = true, 
                BaseSize = 18, AdjustedSize = 12, RiskFactor = 0.1, EnhancedAnalysis = "Momentum", EnhancedAnalysisValue= -0.04, SpyCorrelation = 0.46, SectorExposure = 0.0},
            new StockItem { Symbol = "GRAB", Price = 4.90m, PriceChange = 0m, MarketRegime = "High Volatility", Confidence = 0.281, TrendStrength = 0.27, IsPositive = false, 
                BaseSize = 544, AdjustedSize = 380, RiskFactor = 0.1, EnhancedAnalysis = "Momentum", EnhancedAnalysisValue= 0.02, SpyCorrelation = 0.21, SectorExposure = 0.0},
        };
}
