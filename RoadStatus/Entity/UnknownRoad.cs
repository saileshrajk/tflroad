namespace RoadStatus.Entity
{
    public class UnknownRoad : IRoad
    {
        private readonly string roadName;
        public UnknownRoad(string roadName)
        {
            this.roadName = roadName;
        }

        public string GetDisplayMessage()
        {
            return $"The status of road {roadName} cannot be determined";
        }

        public ApplicationStatus GetApplicationStatus()
        {
            return ApplicationStatus.Unknown;
        }
    }
}
