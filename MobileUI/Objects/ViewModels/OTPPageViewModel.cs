using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
// ReSharper disable All

namespace MobileUI.Objects.ViewModels;

public class OTPPageViewModel : ObservableObject, IQueryAttributable
{
    private string goToPage = string.Empty;
    public ICommand GoToHomePageCommand { get; }

    public OTPPageViewModel()
    {
        GoToHomePageCommand = new RelayCommand(GoToHomePage);
    }

    private async void GoToHomePage()
    {
        //validate the opt and then go to the provided page.
        await Shell.Current.GoToAsync(goToPage);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var url = query["GoToPage"]?.ToString();
        if(string.IsNullOrWhiteSpace(url)) return;
        goToPage = url;
    }
}