using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Common.Service;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using MobileUI.Controls.Custom;
using MobileUI.Objects.Helpers;
using MobileUI.Objects.Session;

namespace MobileUI.Objects.ViewModels;

public partial class HomeTabViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<Schedule> _userActiveSchedules = [];
    [ObservableProperty] private bool _isBottomSheetPresented;
    private Schedule? _userSelectedSchedule;
    [ObservableProperty] private bool _isScheduleRefreshing;
    [ObservableProperty] private bool _isListViewSchedulesVisible;
    [ObservableProperty] private bool _isNoScheduleVisible;

    public ICommand GetUpdateSchedulesCommand { get; }
    public ICommand DeleteScheduleCommand { get; }
    private ConfirmModal? _confirmModal;

    public HomeTabViewModel(IApplicationService appServiceProvider, ILogger logger) : base(appServiceProvider, logger)
    {
        //In case the user has no schedule we want to display it 
        //otherwise show the listview with schedules
        SetHomeTabContent();

        GetUpdateSchedulesCommand = new RelayCommand(SetHomeTabContent);
        DeleteScheduleCommand = new AsyncRelayCommand(ShowDeleteScheduleModal);

    }

    private async void SetHomeTabContent()
    {
        //when this task finishes we try to work with our "UserActiveSchedules"
        await GetUserActiveSchedules();

        if (UserActiveSchedules.Any()) SetPageContent();
        else SetPageNoContent();

    }

    private async Task GetUserActiveSchedules()
    {
        var sessionManager = new SessionManager();
        var currentUserId = await sessionManager.GetCurrentUser();
        
        if (currentUserId is null)
        {
            await ApplicationHelper.DisplayMessage("You need to login to be able to use this page.");
            return;
        }

        try
        {
            IsScheduleRefreshing = true;
            
            //in case the no schedule is visible hide it
            IsNoScheduleVisible = false;
            
            var userSchedules = await AppServiceProvider
                        .ScheduleService.GetUserSchedulesAsync(currentUserId.Value);

            if (userSchedules == null || userSchedules.IsSucesseful == false)
            {
                await ApplicationHelper.DisplayMessage("Error on retrieve User Schedules. Try again Later.");
                return;
            }

            if (UserActiveSchedules.Any()) UserActiveSchedules.Clear();

            if (userSchedules.Result == null) return;

            foreach (var schedule in userSchedules.Result)
                UserActiveSchedules.Add(schedule);

        }
        catch (Exception e)
        {
            Logger.Log(LogLevel.Error, e, "Some error has occured while getting user Schedules.");
            await ApplicationHelper.DisplayMessage("Some error has occured try again later.");
            throw;
        }
        finally
        {
            IsScheduleRefreshing = false;
        }
    }
    
    private async Task ShowDeleteScheduleModal()
    {
        if (_userSelectedSchedule == null) 
            await ApplicationHelper.DisplayMessage("Unable to delete Schedule \ud83d\ude25");

        _confirmModal = new ConfirmModal
        {
            BindingContext = new ConfirmModalViewModel
            {
                Confirm = "OK",
                Cancel = "Cancel",
                Message = _userSelectedSchedule?.ScheduleTask.Name,
                Title = "Delete Schedule",
                CancelCommand = new RelayCommand(Cancel),
                ConfirmCommand = new RelayCommand(DeleteUserSchedule)
            }
        };

        await Shell.Current.ShowPopupAsync(_confirmModal);

    }

    public void ListViewSchedules_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        IsBottomSheetPresented = !IsBottomSheetPresented;
        
        if(e.Item is Schedule currentSchedule)
            _userSelectedSchedule = currentSchedule;
    }

    private async void Cancel()
    {
        if(_confirmModal != null) await _confirmModal.CloseAsync();
    }

    private async void DeleteUserSchedule()
    {
        //change the menu of schedule item options visibility 
        IsBottomSheetPresented = !IsBottomSheetPresented;
        
        if(_confirmModal != null) await _confirmModal.CloseAsync();

        if(_userSelectedSchedule == null) return;
        
        var result = await ApplicationHelper.ExecuteLoadingTask("Deleting Schedule...", 
            () => AppServiceProvider.ScheduleService.DeleteScheduleAsync(_userSelectedSchedule.Id));
        
        if(result == null || result.IsSucesseful == false) return;

        await Shell.Current.DisplaySnackbar($"Deleted {_userSelectedSchedule.ScheduleTask.Name}.",
            null, "OK", TimeSpan.FromSeconds(5));
        
        //we want to update the list of active schedules after doing a
        //modification to the list. In this case after the delete operation. 
        SetHomeTabContent();
    }
    
    private void SetPageContent()
    {
        IsListViewSchedulesVisible = true;
        IsNoScheduleVisible = !IsListViewSchedulesVisible;
    }

    private void SetPageNoContent()
    {
        IsListViewSchedulesVisible = false;
        IsNoScheduleVisible = !IsListViewSchedulesVisible;
    }
    
}