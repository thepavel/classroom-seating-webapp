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

    [Fact]
    public void CollapsedChartLengthIsMultipleOfRowsAndColumns()
    {
        //given 
        var twoByTwoChart = new SeatingChart(new ClassPeriod(2, 2));

        //when
        var chart = SeatingChart.CollapseChart(twoByTwoChart.Chart, 2, 2);

        //then
        chart.Length.ShouldBe(4);
        foreach(var name in chart) 
        {
            name.ShouldBe("x");
        }
    }

    [Theory()]
    [InlineData(4, 4)]
    public void CollapsedChart_ContainsStudents(int rows, int columns) 
    {
        //given
        var seatingChart = new SeatingChart(new ClassPeriod(rows, columns));

        //when
        var chart = SeatingChart.CollapseChart(seatingChart.Chart, rows, columns);

        //then
        for(var i = 0; i < rows; i++) 
        {
            for (var j = 0; j < columns; j++) 
            {
                chart[i+j].ShouldBe(seatingChart.Chart[i,j]);
            }
        }
    }
}
