using System.Threading.Tasks;
using RoadStatus.Entity;

namespace RoadStatus.Repository
{
    public interface IRoadStatusRepository
    {
        Task<IRoad> GetRoadStatus(string roadName);
    }
}