namespace RoadStatus.Entity
{
    public interface IRoad
    {
        string GetDisplayMessage();
        ApplicationStatus GetApplicationStatus();
    }
}
