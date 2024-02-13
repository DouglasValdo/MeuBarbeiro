using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
// ReSharper disable All

namespace MobileUI.Objects.ViewModels;

public class OTPPageViewModel : ObservableObject, IQueryAttributable
{
    private string _goToPage = string.Empty;
    private IDictionary<string, object> _queryToFoward = new Dictionary<string, object>();
    public ICommand GoToHomePageCommand { get; }

    public OTPPageViewModel()
    {
        GoToHomePageCommand = new AsyncRelayCommand(GoToHomePage);
    }

    private async Task GoToHomePage()
    {
        //validate the opt and then go to the provided page.
        await Shell.Current.GoToAsync(_goToPage, _queryToFoward);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var url = query["GoToPage"]?.ToString();
        
        if(string.IsNullOrWhiteSpace(url)) return;
        
        _queryToFoward = query;
        
        _goToPage = url;
    }
}