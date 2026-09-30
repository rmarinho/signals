# AGENTS.md

Guidance for AI coding agents (GitHub Copilot, Claude, Codex, etc.) working in
this repository. This file mirrors the detailed
[`.github/copilot-instructions.md`](.github/copilot-instructions.md) — read that
for the full set of conventions.

## What this is

`signals` is a **.NET 9 MAUI** app that displays **stock market trading signals**
(price, patterns, market regime, confidence/trend strength, position sizing, and
correlation analysis) in a tabbed UI: Technical, Market, Price, SEC, News.

> The app is currently a UI mock-up backed by an in-memory `DataService`; there
> is no live data feed yet.

## Project map

- `Models/` — observable models (`StockItem`).
- `Services/` — data access behind `IDataService`.
- `ViewModels/` — `BaseViewModel`, `MainViewModel`.
- `Pages/` — one `ContentPage` per shell tab.
- `Views/` — reusable controls (`StockView` = stock card).
- `Helpers/` — value converters.
- `Resources/` — fonts, images, styles, localized strings (`Signals.resx`).
- `MauiProgram.cs` — DI registration + bootstrap. `AppShell.xaml` — tabs/routes.

## Conventions (must follow)

- **MVVM via CommunityToolkit.Mvvm**: `partial` classes; `[ObservableProperty]`
  on `_camelCase` fields; `[RelayCommand]` for commands; `[NotifyPropertyChangedFor]`
  for computed properties. No hand-written `INotifyPropertyChanged`.
- **DI**: register pages/view models/services (as singletons) in `MauiProgram`;
  inject via constructors; depend on interfaces (`IDataService`).
- **Pages** set `BindingContext` from an injected view model and load data in
  `OnAppearing`, not the constructor.
- **Compiled bindings**: set `x:DataType` in XAML.
- **Dark theme colors**: up `#21b559`, down `#f87171`, muted `#FF6D6D6D`,
  card `#18202e`, divider `#252e3d`. Use converters in `Helpers/` for value→color.
- **Logging**: `ILogger<T>` + `LogError(ex, "...")` (Serilog file sink).
- **Localization**: add strings to `Resources/Signals.resx`.
- **Platform code**: guard with `#if IOS || MACCATALYST` / `#if WINDOWS`.

## Build & run

```bash
dotnet workload install maui      # one-time
dotnet restore
dotnet build -t:Build -f net9.0-maccatalyst   # or net9.0-android / net9.0-windows
dotnet build -t:Run   -f net9.0-maccatalyst
```

There is no test project yet. If adding tests, create a separate test project —
do not add a test runner to `signals.csproj`.

## Do / don't

- ✅ Keep changes surgical and match the existing file's style and indentation.
- ✅ Extend `IDataService` for new data needs.
- ❌ Don't add new MVVM frameworks, DI containers, or logging libraries.
- ❌ Don't hard-code display strings where a localized resource fits.
