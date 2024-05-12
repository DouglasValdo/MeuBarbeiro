namespace MobileUI.Utils;

public static class ResourceProvider
{
    public static Dictionary<DayOfWeek, string> PTDaysOfWeek = new Dictionary<DayOfWeek, string>
        {
            {DayOfWeek.Monday, "Seg" },
            {DayOfWeek.Tuesday, "Ter" },
            {DayOfWeek.Wednesday, "Qua" },
            {DayOfWeek.Thursday, "Qui" },
            {DayOfWeek.Friday, "Sex" },
            {DayOfWeek.Saturday, "Sab" },
            {DayOfWeek.Sunday, "Dom" }
        };
    public static string LoginRequired() => "You need to login to be able to use this page.";

    public static string UnableToRetrieveSchedule() => "Error on retrieve User Schedules. Try again Later.";

    public static string GenericError() => "Some error has occured try again later.";

    public static string UnableToDeleteSchedule() => "Unable to delete Schedule \ud83d\ude25";

    public static string DeletingSchedule() => "Deleting Schedule...";
    public static string LoadingSchedule() =>  "Loading Available Schedules...";
}