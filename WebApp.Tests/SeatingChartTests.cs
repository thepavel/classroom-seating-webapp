using System.Configuration.Assemblies;
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
            var classRoster = new ClassroomPeriod(1, 1);
            var chart = classRoster.GetClassroomSeatingChart();
            
            chart.ShouldNotBeNull();
        }

        [Fact]
        public void SeatingChart_Constructor_ChartDimensions_AreRowsTimesColumns()
        {
            int rows = DefaultRows;
            int columns = DefaultColumns;
            var classRoster = new ClassroomPeriod(rows, columns);

            var seatingChart = classRoster.GetClassroomSeatingChart();
            seatingChart.Length.ShouldBe(rows * columns);
        }

        [Fact]
        public void SeatingChart_GetStudentSeatingChart_Returns2DArray_WithX()
        {
            int rows = DefaultRows;
            int columns = DefaultColumns;
            var seatingChart = new ClassroomPeriod(rows, columns);
            var studentSeatingChart = seatingChart.GetClassroomSeatingChart();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    studentSeatingChart[i, j].ShouldBe("x");
                }
            }


        }

        [Fact]
        public void SeatingChart_Students_IsInitiallyEmpty()
        {
            var seatingChart = new ClassroomPeriod(DefaultRows, DefaultColumns);
            seatingChart.Students.ShouldBeEmpty();
        }

        [Fact]
        public void SeatingChart_AddStudent_ReturnsTrue()
        {
            // Given
            var seatingChart = new ClassroomPeriod(DefaultRows, DefaultColumns);

            // When
            var result = seatingChart.AddStudent(new StudentName("student", "name"));
            // Then
            result.ShouldBeTrue();
            seatingChart.Students.Count.ShouldBe(1);

        }

        [Fact]
        public void SeatingChart_GetStudentSeatingChart_StartsWithAddedStudent()
        {
            // Given
            var seatingChart = new ClassroomPeriod(DefaultRows, DefaultColumns);
            var defaultStudent = new StudentName("student", "name");
            //When
            seatingChart.AddStudent(defaultStudent);

            var chart = seatingChart.GetClassroomSeatingChart();

            chart[0, 0].ShouldBe(defaultStudent.FullName);
        }
        // [Fact]
        // public void SeatingChart_EmptyChart_PutsFirstStudentInTopRight()
        // {

        // }
    }
}