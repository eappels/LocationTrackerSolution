using CommunityToolkit.Mvvm.Messaging;
using LocationTracker.Helpers;
using LocationTracker.Models;
using LocationTracker.Services;
using System.Diagnostics;

namespace LocationTracker.ViewModels;

public class TrackingViewModel : BaseViewModel, IDisposable
{

    private readonly LocationService locationService;

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
            WeakReferenceMessenger.Default.Send(new Location(e.Latitude, e.Longitude));
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