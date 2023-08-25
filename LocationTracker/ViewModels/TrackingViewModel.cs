using CommunityToolkit.Mvvm.Messaging;
using LocationTracker.Helpers;
using LocationTracker.Models;
using LocationTracker.Services;
using LocationTracker.Services.Interfaces;

namespace LocationTracker.ViewModels;

public class TrackingViewModel : BaseViewModel, IDisposable
{

    private readonly LocationService locationService;
    private readonly IDBService dBService;

    private List<LocationModel> RouteHistory = new();

    public Command ClearRouteHistoryCommand
        => new Command(() => ClearRouteHistory());

    public TrackingViewModel(LocationService locationService, IDBService dBService)
    {
        this.locationService = locationService;
        this.dBService = dBService;
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
        await dBService.Save(new RouteInfo
        {
            RouteHistory = RouteHistory
        });
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