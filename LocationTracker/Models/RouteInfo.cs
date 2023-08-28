namespace LocationTracker.Models;

public class RouteInfo
{
    public string Date { get; private set; }
    public string RouteHistory { get; set; }

    public RouteInfo()
    {
        Date = DateTime.Now.ToString("dd-mmy-yyyy");
    }
}