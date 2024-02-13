using System.Globalization;
using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Views;
using MobileUI.Controls.Custom;
using MobileUI.Objects.ViewModels;

namespace MobileUI.Objects.Helpers;

public static class ApplicationHelper
{
    public static async Task DisplayMessage(string errorMessage)
    {
        var popup = new AppModal
        {
            BindingContext = new AppModalViewModel{ MessageText = errorMessage},
            CanBeDismissedByTappingOutsideOfPopup = true
        };
            
        await Shell.Current.ShowPopupAsync(popup);
    }

    public static async Task<T> ExecuteLoadingTask<T>(string? loadingMessage, Func<Task<T>> toExecute)
    {
        var viewModel = new AppModalViewModel { IsBusy = true };

        if (!string.IsNullOrWhiteSpace(loadingMessage))
            viewModel.MessageText = loadingMessage;

        var popup = new AppModal
        {
            BindingContext = viewModel,
            CanBeDismissedByTappingOutsideOfPopup = false
        };

        Shell.Current.ShowPopup(popup);
        
        try
        {
            var result = await toExecute.Invoke();
            popup.Close();
            return result;
        }
        catch
        {
            popup.Close();
            throw;
        }
    }

    public static async Task DisplayHttpRequestExceptionMessage(HttpRequestException serviceException)
    {
        switch (serviceException.HttpRequestError)
        {
            case HttpRequestError.Unknown:
                await DisplayMessage("Check your internet connection.");
                break;
            case HttpRequestError.ConnectionError:
            case HttpRequestError.NameResolutionError:
            case HttpRequestError.SecureConnectionError:
            case HttpRequestError.HttpProtocolError:
            case HttpRequestError.ExtendedConnectNotSupported:
            case HttpRequestError.VersionNegotiationError:
            case HttpRequestError.UserAuthenticationError:
            case HttpRequestError.ProxyTunnelError:
            case HttpRequestError.InvalidResponse:
            case HttpRequestError.ResponseEnded:
            case HttpRequestError.ConfigurationLimitExceeded:
            default:
                await DisplayMessage("Error accessing the service.");
                break;
        }
    }
    
    public static bool IsValidUserPhoneNumber(string userPhoneNumber)
    {
        var isValidInt = int.TryParse(userPhoneNumber,
            NumberStyles.Number, CultureInfo.InvariantCulture, out _);

        const string pattern = @"^\d{9}$";
        
        // Create a Regex object
        Regex regex = new (pattern);

        var isValidPhoneNumber = isValidInt && regex.IsMatch(userPhoneNumber);

        return isValidPhoneNumber;
    }
}