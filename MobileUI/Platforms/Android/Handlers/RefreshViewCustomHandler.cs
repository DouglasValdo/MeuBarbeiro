using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace MobileUI.Platforms.Android.Handlers;

public class RefreshViewCustomHandler : RefreshViewHandler
{
    protected override void ConnectHandler(MauiSwipeRefreshLayout platformView)
    {
        var deviceOffset = platformView?.ProgressViewEndOffset ?? 0;

        base.ConnectHandler(platformView);

        platformView?.SetProgressViewEndTarget(true, deviceOffset);
        
        
    }
}