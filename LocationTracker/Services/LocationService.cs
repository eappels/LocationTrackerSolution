using LocationTracker.Models;

namespace LocationTracker.Services;

public partial class LocationService
{
    public event EventHandler<LocationModel> LocationChanged;
    public event EventHandler<string> StatusChanged;

    public void Initialize()
    {
#if IOS
        IosInitialize();
#endif
    }

    public void Stop()
    {
#if IOS
        IosStop();
#endif
    }

    protected virtual void OnLocationChanged(LocationModel e)
    {
        LocationChanged?.Invoke(this, e);
    }

    protected virtual void OnStatusChanged(string e)
    {
        StatusChanged?.Invoke(this, e);
    }
}
