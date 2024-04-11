namespace MobileUI.Utils;

public static class ResourceProvider
{
    public static string LoginRequired()
    {
        return "You need to login to be able to use this page.";
    }

    public static string UnableToRetrieveSchedule()
    {
        return "Error on retrieve User Schedules. Try again Later.";
    }

    public static string GenericError()
    {
        return "Some error has occured try again later.";
    }

    public static string UnableToDeleteSchedule()
    {
        return "Unable to delete Schedule \ud83d\ude25";
    }

    public static string DeletingSchedule()
    {
        return "Deleting Schedule...";
    }
}