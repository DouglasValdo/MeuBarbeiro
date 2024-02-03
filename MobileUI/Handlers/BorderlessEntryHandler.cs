using MobileUI.Controls.Editors;
#if __ANDROID__
using Android.Content.Res;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace MobileUI.Handlers;

public static class BorderlessEntryHandler
{
    public static void AppendMapping()
    {
        Microsoft.Maui.Handlers.ElementHandler.ElementMapper
            .AppendToMapping("Borderless", (handler, element) =>
            {
                if(element is not BorderlessEntry) return;

#if __ANDROID__
                if (handler.PlatformView is AppCompatEditText editText)
                    editText.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
            });
    }
}