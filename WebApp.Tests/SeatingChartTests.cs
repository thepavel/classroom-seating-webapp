using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests
{
    public class SeatingChartTests
    {
        private const int DefaultRows = 4;
        private const int DefaultColumns = 4;

        [Fact]
        public void SeatingChart_Constructor_Chart_IsNotNull()
        {
            var seatingChart = new SeatingChart(1, 1);
            seatingChart.Chart.ShouldNotBeNull();
        }

        [Fact]
        public void SeatingChart_Constructor_ChartDimensions_AreRowsTimesColumns()
        {
            int rows = DefaultRows;
            int columns = DefaultColumns;
            var seatingChart = new SeatingChart(rows, columns);

            seatingChart.Chart.Length.ShouldBe(rows * columns);            
        }

        [Fact]
        public void SeatingChart_GetStudentSeatingChart_Returns2DArray_WithX() 
        {
            int rows = DefaultRows;
            int columns = DefaultColumns;
            var seatingChart = new SeatingChart(rows, columns);
            var studentSeatingChart = seatingChart.GetStudentSeatingChart();
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    studentSeatingChart[i, j].ShouldBe("x");
                }
            }
            
            
        }
        // [Fact]
        // public void SeatingChart_EmptyChart_PutsFirstStudentInTopRight()
        // {

        // }
    }
}