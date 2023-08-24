using LocationTracker.Helpers;
using LocationTracker.Services.Interfaces;
using System.Diagnostics;

namespace LocationTracker.ViewModels;

public class TrackingViewModel : BaseViewModel, IDisposable
{

    private readonly ILocationTrackerInterface locationTrackerInterface;

    public TrackingViewModel(ILocationTrackerInterface locationTrackerInterface)
    {
        this.locationTrackerInterface = locationTrackerInterface;
        this.locationTrackerInterface.LocationChanged += OnLocationChanged;
        this.locationTrackerInterface.Start();
        IsTracking = false;
    }

    private void OnLocationChanged(object sender, Location e)
    {
        if (IsTracking)
        {
            Debug.WriteLine($"Location changed: {e.Latitude}, {e.Longitude}");
        }
        else
        {
            Debug.WriteLine("Tracking disabled");
        }
        
    }

    private bool isTracking;
    public bool IsTracking
    {
        get => isTracking;
        set => SetProperty(ref isTracking, value);
    }

    public void Dispose()
    {
        locationTrackerInterface.Stop();
        locationTrackerInterface.LocationChanged -= OnLocationChanged;
    }
}