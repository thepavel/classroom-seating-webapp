using System.Configuration.Assemblies;
using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests
{
    public class ClassroomPeriodTests
    {
        private const int DefaultRows = 4;
        private const int DefaultColumns = 4;
        private ClassroomPeriod DefaultClassroomPeriod;

        public ClassroomPeriodTests() {
            DefaultClassroomPeriod = new ClassroomPeriod(DefaultRows, DefaultColumns);
        }

        [Fact]
        public void ClassroomPeriod_Constructor_Chart_IsNotNull()
        {
            var classroomPeriod = new ClassroomPeriod(1, 1);
            var chart = classroomPeriod.GetClassroomSeatingChart();
            
            chart.ShouldNotBeNull();
        }

        [Fact]
        public void ClassroomPeriod_Constructor_ChartDimensions_AreRowsTimesColumns()
        {
            int rows = DefaultRows;
            int columns = DefaultColumns;
            var classroomPeriod = DefaultClassroomPeriod;

            var seatingChart = classroomPeriod.GetClassroomSeatingChart();
            seatingChart.Length.ShouldBe(rows * columns);
        }

        [Fact]
        public void ClassroomPeriod_GetStudentSeatingChart_Returns2DArray_WithX()
        {
            int rows = DefaultRows;
            int columns = DefaultColumns;
            var seatingChart = DefaultClassroomPeriod.GetClassroomSeatingChart();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    seatingChart[i, j].ShouldBe("x");
                }
            }

        }

        [Fact]
        public void ClassroomPeriod_Students_IsInitiallyEmpty()
        {
            var classroomPeriod = DefaultClassroomPeriod;
            classroomPeriod.Students.ShouldBeEmpty();
        }

        [Fact]
        public void ClassroomPeriod_AddStudent_ReturnsTrue()
        {
            // Given
            var classroomPeriod = DefaultClassroomPeriod;

            // When
            var result = classroomPeriod.AddStudent(new StudentName("student", "name"));
            // Then
            result.ShouldBeTrue();
            classroomPeriod.Students.Count.ShouldBe(1);

        }

        [Fact]
        public void ClassroomPeriod_GetStudentSeatingChart_StartsWithAddedStudent()
        {
            // Given
            var classroomPeriod = new ClassroomPeriod(DefaultRows, DefaultColumns);
            var defaultStudent = new StudentName("student", "name");
            //When
            classroomPeriod.AddStudent(defaultStudent);

            var chart = classroomPeriod.GetClassroomSeatingChart();

            chart[0, 0].ShouldBe(defaultStudent.FullName);
        }
        

    }
}