using LocationTracker.Models;

namespace LocationTracker.Services.Interfaces;

public interface IDBService
{
    Task<int> Save(RouteInfo route);
}