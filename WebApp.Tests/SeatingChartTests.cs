using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class SeatingChartTests
{
    private SeatingChart SeatingChart;
    private static SeatingChart TwoByTwoSeatingChart => new(new ClassPeriod(2,2));

    public SeatingChartTests()
    {
        SeatingChart = new SeatingChart(new ClassPeriod(1, 1));
    }

    //Ignore[Fact] cannot handle 2x2 yet.
    internal void SeatingChart_2x2_TwoStudentsTests()
    {
        //given
        var students = new List<StudentName> { new("default", "student"), new("another", "one") };
        SeatingChart = new SeatingChart(new ClassPeriod(2, 2, students.ToArray()));
        
        //when
        var chart = SeatingChart;
    }


}
