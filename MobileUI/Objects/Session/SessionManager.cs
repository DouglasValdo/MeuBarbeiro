namespace MobileUI.Objects.Session;

public static class SessionManager
{
    private const string LoggedUserKey = "LOGGEDUSERID";
    
    public static async Task<Guid?> GetCurrentUser()
    {
        var loggedUserId = await SecureStorage.GetAsync(LoggedUserKey);

        if (string.IsNullOrWhiteSpace(loggedUserId)) return null;

        return Guid.Parse(loggedUserId); 
    }

    public static async Task StoreUser(Guid userId)
    {
        await SecureStorage.SetAsync(LoggedUserKey, userId.ToString());
    }

    public static void ClearUser()
    {
        SecureStorage.Remove(LoggedUserKey);
    }
}