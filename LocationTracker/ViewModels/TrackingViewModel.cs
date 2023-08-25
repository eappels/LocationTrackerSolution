using CommunityToolkit.Mvvm.Messaging;
using LocationTracker.Helpers;
using LocationTracker.Models;
using LocationTracker.Services;

namespace LocationTracker.ViewModels;

public class TrackingViewModel : BaseViewModel, IDisposable
{

    private readonly LocationService locationService;
    private List<LocationModel> RouteHistory = new();

    public Command ClearRouteHistoryCommand
        => new Command(() => ClearRouteHistory());

    public TrackingViewModel(LocationService locationService)
    {
        this.locationService = locationService;
        locationService.Initialize();
        locationService.LocationChanged += OnLocationChanged;
        IsTracking = false;
    }

    private void OnLocationChanged(object sender, LocationModel e)
    {
        if (IsTracking)
        {
            RouteHistory.Add(e);
            WeakReferenceMessenger.Default.Send(new Location(e.Latitude, e.Longitude));
        }
        else
        {
            WeakReferenceMessenger.Default.Send(new Location(0, 0));
        }
    }

    private void ClearRouteHistory()
    {        
        WeakReferenceMessenger.Default.Send("ClearRouteHistoryRequest");
    }

    public async Task SaveRouteHistory()
    {
        //Save to DB
        await Task.Delay(1000);
        RouteHistory.Clear();
    }

    private bool isTracking;
    public bool IsTracking
    {
        get => isTracking;
        set => SetProperty(ref isTracking, value);
    }

    public void Dispose()
    {
        locationService.LocationChanged -= OnLocationChanged;
        locationService.IosStop();
    }
}