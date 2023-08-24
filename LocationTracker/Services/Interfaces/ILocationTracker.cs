namespace LocationTracker.Services.Interfaces;

public interface ILocationTrackerInterface
{
    event EventHandler<Location> LocationChanged;
    void Start();
    void Stop();
}