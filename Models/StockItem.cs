using CommunityToolkit.Mvvm.ComponentModel;

namespace signals.Models;

public partial class StockItem : ObservableObject
{
	[ObservableProperty]
	string? _name;

	[ObservableProperty]
	string? _description;

	[ObservableProperty]
	string? _imageUrl;

	[ObservableProperty]
	string? _symbol;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(PercentageChange))]
	[NotifyPropertyChangedFor(nameof(PriceChangeDisplay))]
	decimal _price;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(PercentageChange))]
	[NotifyPropertyChangedFor(nameof(PriceChangeDisplay))]
	decimal? _priceChange;

	[ObservableProperty]
	string? _marketRegime;

	[ObservableProperty]
	double _confidence;

	[ObservableProperty]
	double _trendStrength;

	[ObservableProperty]
	bool _isPositive;

	[ObservableProperty]
	int _baseSize;

	[ObservableProperty]
	int _adjustedSize;

	[ObservableProperty]
	double _riskFactor;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(PatternDisplay))]
	string? _pattern;

	[ObservableProperty]
	string? _enhancedAnalysis;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(EnhancedAnalysisValueColor))]
	double? _enhancedAnalysisValue;

	[ObservableProperty]
	string? _marketAnalysis;

	[ObservableProperty]
	double? _spyCorrelation;
	
	[ObservableProperty]
	double? _sectorExposure;

	public decimal? PercentageChange => PriceChange / Price * 100;

	public Color PriceChangeColor => PriceChange > 0 ? Color.FromArgb("#21b559") : (PriceChange < 0 ? Color.FromArgb("#f87171") : Colors.Gray);
	public string PriceChangeDisplay => PriceChange > 0 ? $"+{PriceChange:F2} ({PercentageChange:F2}%)" : $"{PriceChange:F2} ({PercentageChange:F2}%)";
	public string PatternDisplay => string.IsNullOrEmpty(Pattern) ? "No patterns detected" : $"- {Pattern}";

	public Color PatternDisplayColor => !string.IsNullOrEmpty(Pattern) ? Color.FromArgb("#f87171") : Colors.Gray;

	public Color MarketRegimeColor => MarketRegime?.ToLower() switch
	{
		"high volatility" => Color.FromArgb("#ecc117"),
		"low Volatility" => Color.FromArgb("#21b559"),
		_ => Colors.Gray
	};

	public Color EnhancedAnalysisValueColor => EnhancedAnalysisValue > 0 ? Color.FromArgb("#21b559") : (EnhancedAnalysisValue < 0 ? Color.FromArgb("#f87171") : Colors.Gray);


	public string ConfidenceDisplay => $"Confidence: {Confidence:P1}";
	public string TrendStrengthDisplay => $"Trend Strength: {TrendStrength:F2}";
}