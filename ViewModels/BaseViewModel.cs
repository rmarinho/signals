using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace signals.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    protected BaseViewModel(IDispatcher dispatcher)
    {
        Dispatcher = dispatcher;
    }

    protected IDispatcher Dispatcher { get; }

}
