namespace WebApp.Models
{
    public class PeriodRoster
    {
        public PeriodRoster()
        {
            StudentNames = Array.Empty<string>();
        }
        public int Period { get; set; }
        public string[] StudentNames { get; set; }
        public string[] GetSortedRoster()
        {
            
            Array.Sort(StudentNames);
            return StudentNames;
        }
    }
}