using System.Data.SQLite;

namespace FreelanceAplication.Data
{
    public abstract class DatabaseManager
    {
        protected readonly string connectionString = "Data Source=users.db;Version=3;";

        protected SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
