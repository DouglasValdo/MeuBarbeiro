using MobileUI.Objects.ViewModels;

namespace MobileUI.Controls.Pages;

public partial class OTPPage : ContentPage
{
    public OTPPage()
    {
        InitializeComponent();
        BindingContext = new OTPPageViewModel();
        
    }
}