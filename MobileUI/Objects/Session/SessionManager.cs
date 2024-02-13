namespace MobileUI.Objects.Session;

public class SessionManager
{
    private const string LoggedUserKey = "LOGGEDUSERID";
    
    public async Task<Guid?> GetCurrentUser()
    {
        var loggedUserId = await SecureStorage.GetAsync(LoggedUserKey);

        if (string.IsNullOrWhiteSpace(loggedUserId)) return null;

        return Guid.Parse(loggedUserId); 
    }

    public async Task StoreUser(Guid userId)
    {
        await SecureStorage.SetAsync(LoggedUserKey, userId.ToString());
    }
}