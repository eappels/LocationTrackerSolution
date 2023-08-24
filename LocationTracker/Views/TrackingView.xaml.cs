using CommunityToolkit.Mvvm.Messaging;
using LocationTracker.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;

namespace LocationTracker.Views;

public partial class TrackingView : ContentPage
{

    private Polyline line { get; set; } = new() { StrokeColor = Colors.Blue, StrokeWidth = 12 };

	public TrackingView(TrackingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        WeakReferenceMessenger.Default.Register<Location>(this, (s, location) =>
        {
            if (map is not null)
            {
                if (location.Latitude == 0 && location.Longitude == 0)
                {
                    if (map.Pins.Count > 0)
                    {
                        map.Pins.Clear();
                    }
                    return;
                }
                if (map.Pins.Count == 0)
                {                   
                    Pin pin = new Pin()
                    {
                        Label = "Current Location",
                        Type = PinType.Generic,
                        Location = location
                    };
                    map.Pins.Add(pin);
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Location, Distance.FromMeters(100)));
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

        WeakReferenceMessenger.Default.Register<string>(this, (s, str) =>
        {
            if (str == "ClearRouteHistory")
            {
                if (map.MapElements.Count > 0)
                {
                    if (line is not null)
                        line.Geopath.Clear();
                }
            }
        });
    }
}