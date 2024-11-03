using SQLite;
using InicioSesion.Models;

namespace InicioSesion.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        //Constructor
        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<User> GetUserAsync(string username)
        {
            return _database.Table<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }
    }
}
