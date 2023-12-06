using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests
{
    public class SeatingChartTests
    {

        [Fact]
        public void SeatingChart_Constructor_Chart_IsNotNull()
        {
            var seatingChart = new SeatingChart(1, 1);
            seatingChart.Chart.ShouldNotBeNull();
        }

        [Fact]
        public void SeatingChart_Constructor_ChartDimensions_AreRowsTimesColumns()
        {
            var seatingChart = new SeatingChart(4, 4);
            seatingChart.Chart[0].Capacity.ShouldBe(4);
            
        }

        // [Fact]
        // public void SeatingChart_EmptyChart_PutsFirstStudentInTopRight()
        // {

        // }
    }
}