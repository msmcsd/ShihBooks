namespace ShihBooks.Plugins.DataStore.Sqlite
{
    internal class Constants
    {
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, "ShihBooks.db3");
    }
}
