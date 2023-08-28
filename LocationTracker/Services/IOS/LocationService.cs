using CoreLocation;
using LocationTracker.Models;

namespace LocationTracker.Services;

public partial class LocationService
{
    private CLLocationManager _iosLocationManager;

    public void IosInitialize()
    {
        OnStatusChanged($"LocationService->Initialize");
        _iosLocationManager ??= new CLLocationManager()
        {
            DesiredAccuracy = CLLocation.AccurracyBestForNavigation,
            DistanceFilter = CLLocationDistance.FilterNone,
            PausesLocationUpdatesAutomatically = false
        };

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                OnStatusChanged("Permission for location is not granted, we can't get location updates");
                return;
            }
            _iosLocationManager.RequestAlwaysAuthorization();
            _iosLocationManager.LocationsUpdated += LocationsUpdated;
            _iosLocationManager.StartUpdatingLocation();
        });
    }

    public void IosStop()
    {
        OnStatusChanged($"LocationService->Stop");
        if (_iosLocationManager is null)
            return;

        _iosLocationManager.StopUpdatingLocation();
        _iosLocationManager.LocationsUpdated -= LocationsUpdated;
    }

    private void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
    {
        var locations = e.Locations;
        LocationChanged?.Invoke(this, new LocationModel(
            locations[^1].Coordinate.Latitude,
            locations[^1].Coordinate.Longitude));
    }
}