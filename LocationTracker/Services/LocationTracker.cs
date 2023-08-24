using CoreLocation;
using LocationTracker.Services.Interfaces;

namespace LocationTracker.Services;

public class LocationTrackerService : ILocationTrackerInterface
{
    public event EventHandler<Location> LocationChanged;
    private CLLocationManager _iosLocationManager;

    public LocationTrackerService()
    {
    }

    public void Start()
    {
        _iosLocationManager ??= new CLLocationManager()
        {
            DesiredAccuracy = CLLocation.AccuracyBest,
            DistanceFilter = CLLocationDistance.FilterNone,
            PausesLocationUpdatesAutomatically = false
        };

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                return;
            }
            _iosLocationManager.RequestAlwaysAuthorization();
            _iosLocationManager.LocationsUpdated += LocationsUpdated;
            _iosLocationManager.StartUpdatingLocation();
        });
    }

    public void Stop()
    {
        if (_iosLocationManager is null)
            return;

        _iosLocationManager.StopUpdatingLocation();
        _iosLocationManager.LocationsUpdated -= LocationsUpdated;
    }

    protected virtual void OnLocationChanged(Location e)
    {
        LocationChanged?.Invoke(this, e);
    }

    private void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
    {
        var locations = e.Locations;
        LocationChanged?.Invoke(this, new Location(
            locations[^1].Coordinate.Latitude,
            locations[^1].Coordinate.Longitude));
    }
}