namespace RoadStatus.Entity
{
    public class InvalidRoad : IRoad
    {
        private readonly string roadName;
        public InvalidRoad(string roadName)
        {
            this.roadName = roadName;
        }
        public string GetDisplayMessage()
        {
            return $"{roadName} is not a valid road";
        }

        public ApplicationStatus GetApplicationStatus()
        {
            return ApplicationStatus.Failed;
        }
    }
}
