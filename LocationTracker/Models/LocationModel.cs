namespace LocationTracker.Models;

public class LocationModel
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public LocationModel()
    {
    }

    public LocationModel(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}