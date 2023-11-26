using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class ClassroomInfoService
    {
        private readonly string _dataFile = "classroom.json";
        private readonly string _classroomRostersFile = "data\\classes.json";

        private readonly ClassroomModel classroom;

        public ClassroomInfoService()
        {
            
            classroom = LoadClassroomInfo();

        }

        private ClassroomModel LoadClassroomInfo()
        {
            using StreamReader r = new(_dataFile);
            string json = r.ReadToEnd();
            var classroom = JsonSerializer.Deserialize<ClassroomModel>(json) ?? new ClassroomModel();
            r.Close();
            return classroom;
        }

        public ClassroomModel ClassroomInfo => classroom;
    }
}