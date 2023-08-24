using LocationTracker.Helpers;

namespace LocationTracker.ViewModels;

public class TrackingViewModel : BaseViewModel, IDisposable
{


    public TrackingViewModel()
    {
        IsTracking = false;
    }

    private bool isTracking;
    public bool IsTracking
    {
        get => isTracking;
        set => SetProperty(ref isTracking, value);
    }

    public void Dispose()
    {

    }
}