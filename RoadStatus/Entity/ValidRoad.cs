using System;

namespace RoadStatus.Entity
{
    public class ValidRoad : IRoad
    {
        public ValidRoad(string displayName, string roadStatus, string roadStatusDescription)
        {
            DisplayName = displayName;
            RoadStatus = roadStatus;
            RoadStatusDescription = roadStatusDescription;
        }
        public string DisplayName { get; }
        public string RoadStatus { get; }
        public string RoadStatusDescription { get; }
        public string GetDisplayMessage()
        {
           return $"The status of the {DisplayName} is as follows " +
                $"{Environment.NewLine} " +
                $"Road Status is {RoadStatus}" +
                $"{Environment.NewLine} " +
                $"Road Status Description is {RoadStatusDescription}";
        }

        public ApplicationStatus GetApplicationStatus()
        {
            return ApplicationStatus.Success;
        }
    }
}
