using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class ClassroomInfoService
    {
        private readonly string _dataFile = "classroom.json";
        private readonly string _classroomRostersFile = "data/classes.json";

        private readonly ClassroomModel classroom;
        private readonly List<PeriodRoster> periodRosters;

        public ClassroomInfoService()
        {
            
            classroom = LoadClassroomInfo();
            periodRosters = LoadPeriodRosters();
        }

        private List<PeriodRoster> LoadPeriodRosters()
        {
            using StreamReader r = new(_classroomRostersFile);
            string json = r.ReadToEnd();

            var periodRosters = JsonSerializer.Deserialize<PeriodRoster[]>(json) ?? Array.Empty<PeriodRoster>();
            
            return new List<PeriodRoster>(periodRosters);
        }

        private ClassroomModel LoadClassroomInfo()
        {
            using StreamReader r = new(_dataFile);
            string json = r.ReadToEnd();
            
            var classroom = JsonSerializer.Deserialize<ClassroomModel>(json) ?? new ClassroomModel();
            
            return classroom;
        }

        public ClassroomModel ClassroomInfo => classroom;
        public List<PeriodRoster> PeriodRosters => periodRosters;
    }
}