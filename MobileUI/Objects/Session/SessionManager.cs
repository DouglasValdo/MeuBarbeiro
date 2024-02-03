namespace MobileUI.Objects.Session;

public class SessionManager
{
    private const string LoggedUserKey = "LOGGEDUSERID";
    
    public Guid? GetUser()
    {
        var loggedUserId = SecureStorage.GetAsync(LoggedUserKey).Result;

        if (string.IsNullOrWhiteSpace(loggedUserId)) return null;

        return Guid.Parse(loggedUserId); 
    }

    public void StoreUser(Guid userId)
    {
        SecureStorage.SetAsync(LoggedUserKey, userId.ToString());
    }
}