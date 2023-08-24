using CommunityToolkit.Mvvm.Messaging;
using LocationTracker.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace LocationTracker.Views;

public partial class TrackingView : ContentPage
{

    private Polyline line { get; set; } = new() { StrokeColor = Colors.Blue, StrokeWidth = 12 };

	public TrackingView(TrackingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

#if DEBUG
        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(51.166065270430614, 3.8475870795395712), Distance.FromMeters(100)));
#endif

        WeakReferenceMessenger.Default.Register<Location>(this, (s, location) =>
        {
            if (map is not null)
            {
                if (map.Pins.Count == 0)
                {                   
                    Pin pin = new Pin()
                    {
                        Label = "Current Location",
                        Type = PinType.Generic,
                        Location = location
                    };
                    map.Pins.Add(pin);
                }
                else
                {
                    map.Pins[0].Location = location;
                    line.Geopath.Add(location);
                }
                if (map.MapElements.Count == 0)
                {
                    map.MapElements.Add(line);
                }
            }
        });
    }
}