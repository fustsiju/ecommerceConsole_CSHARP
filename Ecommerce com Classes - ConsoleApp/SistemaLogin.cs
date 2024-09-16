class SistemaLogin
{
    private DatabaseManager dbManager;

    public SistemaLogin(DatabaseManager databaseManager)
    {
        dbManager = databaseManager;
    }

    public bool loginUser(string username, string password)
    {
        string storedPasswordHash = dbManager.GetPasswordHash(username);
        return storedPasswordHash == password;  
    }

    public void RegisterUser(string username, string password)
    {
        dbManager.addUser(username, password);
    }
}
