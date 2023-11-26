namespace WebApp.Models
{
    public class PeriodRoster
    {
        public int Period { get; set; }
        public required string[] StudentNames { get; set; }
    }
}