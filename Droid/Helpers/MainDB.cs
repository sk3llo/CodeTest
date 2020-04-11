using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Util;
using SQLite;
using Testproject.ViewModels;

namespace Testproject.Droid.Helpers
{
    public class MainDB
    {
        readonly SQLiteAsyncConnection _database;

        public MainDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<LoginModel>().Wait();
        }

        // True if saved, false otherwise (duplicate)
        public async Task<bool> SaveLogin(string login, string password)
        {
            var n = new LoginModel();
            n.login = login;
            n.password = password;
            await _database.InsertAsync(n);
            return true;
        }


        public async Task<LoginModel> LoginCheckExists(string login)
        {
            List<LoginModel> list = await _database.Table<LoginModel>().ToListAsync();

            if (list != null && list.Count > 0)
            {
                foreach (LoginModel record in list)
                {
                    // Found duplicate record
                    if (record.login == login)
                    {
                        return record;
                    }
                }
            }

            return null;

        }

        public Task<List<LoginModel>> queryAll()
        {
            return _database.Table<LoginModel>().ToListAsync();
        }

    }
}
