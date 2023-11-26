using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class ClassroomInfoService
    {
        private readonly string _dataFile = "classroom.json";

        private readonly ClassroomModel classroom;

        public ClassroomInfoService()
        {
            using StreamReader r = new(_dataFile);
            string json = r.ReadToEnd();
            classroom = JsonSerializer.Deserialize<ClassroomModel>(json) ?? new ClassroomModel();

        }

        public ClassroomModel GetClassroomInfo()
        {
            return classroom;
        }
    }
}