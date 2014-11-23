using ChildTracker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ChildTracker.SQLiteManagement
{
    public class SQLiteLocationsDBHelper
    {
        private const string dbName = "SQLiteLocations.db";
        private static SQLiteLocationsDBHelper _instance;


        private SQLiteLocationsDBHelper ()
	{
	}

        public static SQLiteLocationsDBHelper Instance()
        {
            if (_instance == null)
            {
                _instance = new SQLiteLocationsDBHelper();
            }

            return _instance;
        }

        public async Task AddLocation(SQLiteLocationModel location)
        {           
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.InsertAsync(location);
        }

        public async Task<IEnumerable<SQLiteLocationModel>> GetLatestFiveLocationsForUser(string userId)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            var query = conn.Table<SQLiteLocationModel>()
                .Where(l => l.UserId == userId)
                .OrderByDescending(l=>l.LocationDate)
                .Take(5);
            return await query.ToListAsync();
        }

        public async void InitializeDB()
        {
            bool dbExists = await CheckDbAsync(dbName);
            if (!dbExists)
            {
                await this.CreateDatabaseAsync();
            }
        }
      
        private async Task CreateDatabaseAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.CreateTableAsync<SQLiteLocationModel>();
        }

        private async Task<bool> CheckDbAsync(string dbName)
        {
            bool dbExist = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch (Exception)
            {
                dbExist = false;
            }

            return dbExist;
        }
    }
}
