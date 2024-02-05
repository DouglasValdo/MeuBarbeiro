using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Common.Service;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using MobileUI.Objects.Helpers;
using MobileUI.Objects.Session;

namespace MobileUI.Objects.ViewModels;

public partial class HomeTabViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<Schedule> _userActiveSchedules = new ();
    
    public HomeTabViewModel(IApplicationService appServiceProvider, ILogger logger) : base(appServiceProvider, logger)
    {
        GetUserActiveSchedules();
    }

    private async void GetUserActiveSchedules()
    {
        var sessionManager = new SessionManager();
        var currentUserId = await sessionManager.GetUser();
        
        if (currentUserId == null)
        {
            await ApplicationHelper.DisplayMessage("You need to login to be able to use this page.");
            return;
        }

        try
        {
            var userSchedules = await ApplicationHelper
                .ExecuteLoadingTask("Loading...",
                    () => AppServiceProvider
                        .ScheduleService.GetUserSchedulesAsync(currentUserId.Value));

            if (userSchedules == null || userSchedules.IsSucesseful == false)
            {
                await ApplicationHelper.DisplayMessage("Error on retrieve User Schedules. Try again Later.");
                return;
            }
            
            if(UserActiveSchedules.Any()) UserActiveSchedules.Clear();

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
        
    }
}