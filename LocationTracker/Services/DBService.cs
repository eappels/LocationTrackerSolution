using LocationTracker.Helpers;
using LocationTracker.Models;
using LocationTracker.Services.Interfaces;
using SQLite;

namespace LocationTracker.Services;

public class DBService : IDBService
{

    private SQLiteAsyncConnection Database;

    public DBService()
    {
    }

    private async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<RouteInfo>();
    }

    public async Task<int> Save(RouteInfo route)
    {
        return await Database.InsertAsync(route);
    }
}