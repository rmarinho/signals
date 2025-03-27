using CommunityToolkit.Maui.Core;
using LocalizationResourceManager.Maui;

namespace signals;

public partial class AppShell : Shell
{
	IPopupService _popupService;
	ILocalizationResourceManager _resourceManager;
	public AppShell(IPopupService popupService, ILocalizationResourceManager resourceManager)
	{
		_popupService = popupService;
		_resourceManager = resourceManager;
		InitializeComponent();
#if MACCATALYST || WINDOWS
		SetNavBarIsVisible(this, false);
#else
		SetNavBarIsVisible(this, true);
#endif
	}

}
