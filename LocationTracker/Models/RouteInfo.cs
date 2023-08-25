namespace LocationTracker.Models;

public class RouteInfo
{
    public string Date { get; private set; }
    public List<LocationModel> RouteHistory { get; set; }

    public RouteInfo()
    {
        Date = DateTime.Now.ToString("O");
    }
}