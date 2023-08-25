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

#if DEBUG
    double[,] array2D = new double[,] {
        { 51.166064963389616, 3.847573885357794 },
        { 51.16658951527224, 3.8476539486623564 },
        { 51.166935750870174, 3.8476208190190886 },
        { 51.1669755677973, 3.8482889334916424 },
        { 51.16658951527224, 3.8483910832250494 },
        { 51.16608573783506, 3.848482189744034 }
    };
#endif

    private void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
    {
#if DEBUG
        LocationChanged?.Invoke(this, new LocationModel(
               array2D[0,0],
               array2D[0,1]));
        Shift2DArray();
#else
        var locations = e.Locations;
        LocationChanged?.Invoke(this, new LocationModel(
            locations[^1].Coordinate.Latitude,
            locations[^1].Coordinate.Longitude));
#endif
    }

#if DEBUG
    private void Shift2DArray()
    {
        double x = array2D[0, 0];
        double y = array2D[0, 1];

        for (int i = 0; i < 5; i++)
        {
            array2D[i, 0] = array2D[i + 1, 0];
            array2D[i, 1] = array2D[i + 1, 1];
        }
        array2D[5, 0] = x;
        array2D[5, 1] = y;
    }
#endif
}