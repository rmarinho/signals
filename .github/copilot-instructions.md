# Copilot instructions for `signals`

These instructions tell GitHub Copilot (and other AI coding agents) how this
repository is structured, what it is meant to do, and the conventions to follow
when generating or changing code. Keep changes consistent with what is described
here.

## Project intent

`signals` is a cross-platform **.NET MAUI** application that surfaces
**stock market trading signals**. Each stock is presented as a rich card showing
technical and quantitative indicators such as:

- Price and price change (absolute + percentage, color-coded green/red).
- Detected chart **patterns** (e.g. "Rising wedge").
- **Market regime** (e.g. "High Volatility", "Low Volatility").
- **Signal strength**: confidence and trend strength.
- **Position sizing**: base size, adjusted size, risk factor.
- **Enhanced / market analysis**: momentum, SPY correlation, sector exposure.

The app is organized as a tabbed shell with one tab per analysis lens:
**Technical, Market, Price, SEC, News**.

> Current state: the app is an early UI mock-up. `DataService` returns a
> hard-coded in-memory list of `StockItem`s. There is no live market data feed
> yet. When asked to "load data", prefer extending `IDataService` rather than
> wiring HTTP calls directly into view models.

## Tech stack

- **.NET 9** / **.NET MAUI** (`Microsoft.Maui.Controls` 9.0.x), single project.
- Target frameworks: `net9.0-android`, `net9.0-ios`, `net9.0-maccatalyst`, and
  `net9.0-windows` (Windows only when building on Windows). SDK pinned in
  `global.json` (`9.0.100`).
- **MVVM** via `CommunityToolkit.Mvvm` (source generators).
- **CommunityToolkit.Maui** (popups, snackbar, behaviors).
- **LocalizationResourceManager.Maui** for localized strings (`Resources/Signals.resx`).
- **Serilog** for file logging + `Microsoft.Extensions.Logging`.
- **Microsoft.Extensions.Http.Resilience** (available for future resilient HTTP).
- **Nerdbank.GitVersioning** for versioning.

## Project layout

| Folder        | Purpose |
|---------------|---------|
| `Models/`     | Observable data models (`StockItem`). |
| `Services/`   | Data access behind interfaces (`IDataService` / `DataService`). |
| `ViewModels/` | MVVM view models (`BaseViewModel`, `MainViewModel`). |
| `Pages/`      | `ContentPage`s, one per shell tab (Technical/Market/Price/SEC/News). |
| `Views/`      | Reusable `ContentView`s (`StockView` renders a single stock card). |
| `Helpers/`    | `IValueConverter`s (`ValueToColorConverter`, `ProgressToColorConverter`). |
| `Resources/`  | Fonts, images, styles, splash, app icon, and `Signals.resx` strings. |
| `Platforms/`  | Per-platform entry points and native config. |
| `MauiProgram.cs` | DI registration and app bootstrap. |
| `AppShell.xaml`  | Tab/route definitions. |

## Architecture & conventions

Follow these patterns when adding or modifying code:

### Dependency injection
- Register every page, view model, and service in `MauiProgram.CreateMauiApp`.
- Pages and services are registered as **singletons**; inject dependencies via
  constructors (e.g. pages receive `MainViewModel`, view models receive
  `IDataService`, `ILogger<T>`, `IDispatcher`, etc.).
- Depend on **interfaces** (`IDataService`), not concrete types, from view models.

### MVVM with CommunityToolkit.Mvvm
- View models and observable models are `partial` classes that derive from
  `ObservableObject` (models) or `BaseViewModel` (view models).
- Use `[ObservableProperty]` on **private fields** (e.g. `string? _symbol;`) and
  let the generator create the public property. Do **not** hand-write
  `INotifyPropertyChanged` boilerplate.
- Use `[NotifyPropertyChangedFor(nameof(Computed))]` to refresh computed,
  read-only display properties (see `StockItem.PercentageChange`).
- Use `[RelayCommand]` for commands; the generator produces `XxxCommand`
  (e.g. `LoadData` → `LoadDataCommand`). Invoke from
  `OnAppearing` via `await viewModel.LoadDataCommand.ExecuteAsync(null)`.

### Pages & views
- Each `Page` sets `BindingContext` in its constructor from an injected view model.
- Trigger data loads in `OnAppearing`, not in constructors.
- `StockView` is the canonical stock card; set `x:DataType="models:StockItem"`
  on views and `xmlns` aliases (`helpers:`, `models:`, `f:` for fonts) to keep
  bindings compiled. Prefer **compiled bindings** (`x:DataType`) everywhere.

### Styling & colors
- This is a **dark-themed** UI. Common hex colors used across the app:
  - Positive/up: `#21b559` (green)
  - Negative/down: `#f87171` (red)
  - Neutral/muted text: `#FF6D6D6D` / `Colors.Gray`
  - Card background: `#18202e`; borders/dividers: `#252e3d`
- Convert numeric values to colors with the existing converters in `Helpers/`
  rather than embedding color logic in XAML. Keep color thresholds consistent
  (`> 0` green, `< 0` red, `== 0` gray).

### Logging
- Inject `ILogger<T>` and log errors with `_logger.LogError(ex, "message")`.
  Logs roll daily to `AppDataDirectory/logs/log.txt` via Serilog.

### Localization
- Add user-facing strings to `Resources/Signals.resx` and consume them through
  `LocalizationResourceManager`; avoid hard-coded display strings where a
  localized resource is appropriate.

### Platform-specific code
- Guard platform code with `#if IOS || MACCATALYST`, `#if WINDOWS`, etc., as in
  `MauiProgram.cs` (handler swaps) and `AppShell.xaml.cs` (nav bar visibility).

## Build & run

```bash
# Restore
dotnet restore

# Build for a specific target framework (pick one you have workloads for)
dotnet build -t:Build -f net9.0-maccatalyst
dotnet build -t:Build -f net9.0-android

# Run (example: Mac Catalyst)
dotnet build -t:Run -f net9.0-maccatalyst
```

- Requires the **MAUI workloads**: `dotnet workload install maui`.
- iOS/Mac Catalyst builds require macOS + Xcode; Windows target only builds on
  Windows.
- There is currently **no test project**. If you add tests, create a separate
  test project and reference the app project; do not add a test runner to the
  app csproj.

## When generating code

- Match the structure and naming already present (`signals.*` namespaces,
  PascalCase types, `_camelCase` private fields).
- Match the indentation of the **file you are editing** (root/template files use
  tabs; hand-written files under `Services/`, `ViewModels/`, `Helpers/` use four
  spaces).
- Keep changes surgical and consistent with the patterns above; do not introduce
  new MVVM frameworks, DI containers, or logging libraries.
- Only add comments where they clarify non-obvious logic.
