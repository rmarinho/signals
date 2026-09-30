<!--
  Thanks for contributing to signals!
  Please fill out the sections below and delete any that don't apply.
-->

## Summary

<!-- What does this PR do and why? Link any related issue: e.g. "Closes #123". -->

## Changes

<!-- Bullet the key changes. -->
-

## Screenshots / recordings

<!-- For UI changes, add before/after screenshots or a short clip. Note the platform(s). -->

## Testing

<!-- How did you verify this? Which target framework(s) did you build/run? -->
- [ ] `net9.0-android`
- [ ] `net9.0-ios`
- [ ] `net9.0-maccatalyst`
- [ ] `net9.0-windows`

## Checklist

- [ ] Follows the conventions in [`.github/copilot-instructions.md`](../.github/copilot-instructions.md) / [`AGENTS.md`](../AGENTS.md)
- [ ] MVVM: uses `[ObservableProperty]` / `[RelayCommand]`; no hand-written `INotifyPropertyChanged`
- [ ] New pages/view models/services registered in `MauiProgram`
- [ ] XAML uses compiled bindings (`x:DataType`)
- [ ] User-facing strings added to `Resources/Signals.resx` (no hard-coded display text)
- [ ] No new MVVM / DI / logging dependencies introduced
- [ ] Builds for at least one target framework
