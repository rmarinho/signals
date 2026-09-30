# signals

A cross-platform **.NET 9 MAUI** app that surfaces **stock market trading
signals**. Each stock is shown as a rich card combining technical and
quantitative indicators.

## Features

- Per-stock cards (`StockView`) showing:
  - Price and price change (absolute + percentage, color-coded).
  - Detected chart **patterns** (e.g. "Rising wedge").
  - **Market regime** (e.g. "High Volatility").
  - **Signal strength**: confidence + trend strength.
  - **Position sizing**: base size, adjusted size, risk factor.
  - **Analysis**: momentum, SPY correlation, sector exposure.
- Tabbed navigation: **Technical · Market · Price · SEC · News**.
- Dark themed UI, localized strings, and rolling file logs.

> **Status:** early UI mock-up. Data comes from an in-memory `DataService`;
> there is no live market data feed yet.

## Tech stack

- .NET 9 / .NET MAUI (`Microsoft.Maui.Controls` 9.0.x), single project
- MVVM via `CommunityToolkit.Mvvm` (source generators)
- `CommunityToolkit.Maui`, `LocalizationResourceManager.Maui`
- `Serilog` + `Microsoft.Extensions.Logging`
- `Nerdbank.GitVersioning`

Targets: `net9.0-android`, `net9.0-ios`, `net9.0-maccatalyst`, and
`net9.0-windows` (Windows only). SDK pinned to `9.0.100` in `global.json`.

## Getting started

```bash
# Install MAUI workloads (one-time)
dotnet workload install maui

# Restore dependencies
dotnet restore

# Build for a target you have workloads for
dotnet build -t:Build -f net9.0-maccatalyst   # or net9.0-android / net9.0-windows

# Run (example: Mac Catalyst)
dotnet build -t:Run -f net9.0-maccatalyst
```

iOS/Mac Catalyst builds require macOS + Xcode. The Windows target only builds on
Windows.

## Project structure

| Folder | Purpose |
|--------|---------|
| `Models/` | Observable data models (`StockItem`). |
| `Services/` | Data access behind `IDataService`. |
| `ViewModels/` | MVVM view models. |
| `Pages/` | One `ContentPage` per shell tab. |
| `Views/` | Reusable controls (`StockView`). |
| `Helpers/` | Value converters. |
| `Resources/` | Fonts, images, styles, localized strings. |
| `MauiProgram.cs` | DI registration & bootstrap. |
| `AppShell.xaml` | Tabs / routes. |

## Contributing with AI tools

This repo is set up to be **AI-agent friendly**:

- [`.github/copilot-instructions.md`](.github/copilot-instructions.md) — detailed
  conventions for GitHub Copilot.
- [`AGENTS.md`](AGENTS.md) — quick reference for any AI coding agent.

Please keep those files up to date when project conventions change.
