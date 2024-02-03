using CommunityToolkit.Maui.Views;
using MobileUI.Controls.Custom;
using MobileUI.Objects.ViewModels;

namespace MobileUI.Objects.Helpers;

public static class ApplicationHelper
{
    public static async Task DisplayError(string errorMessage)
    {
        var popup = new AppPopUp
        {
            BindingContext = new AppPopUpViewModel{ MessageText = errorMessage},
            CanBeDismissedByTappingOutsideOfPopup = true
        };
            
        await Shell.Current.ShowPopupAsync(popup);
    }

    public static async Task<T> ExecuteLoadingTask<T>(string? loadingMessage, Func<Task<T>> toExecute)
    {
        var viewModel = new AppPopUpViewModel { IsBusy = true };

        if (!string.IsNullOrWhiteSpace(loadingMessage))
            viewModel.MessageText = loadingMessage;

        var popup = new AppPopUp
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
}