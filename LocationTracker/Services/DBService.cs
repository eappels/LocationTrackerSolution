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
        await Init();
        return await Database.InsertAsync(route);
    }

    public async Task<List<RouteInfo>> GetRoutes()
    {
        await Init();
        return await Database.Table<RouteInfo>().Where(ri => ri.Date == DateTime.Now.ToString("dd-mmy-yyyy")).ToListAsync();
    }
}