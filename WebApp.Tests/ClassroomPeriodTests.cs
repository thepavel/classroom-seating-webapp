using System.Configuration.Assemblies;
using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests
{
    public class ClassPeriodTests
    {
        private const int DefaultRows = 4;
        private const int DefaultColumns = 4;
        private ClassPeriod DefaultClassPeriod;

        public ClassPeriodTests() {
            DefaultClassPeriod = new ClassPeriod(DefaultRows, DefaultColumns);
        }

        [Fact]
        public void ClassPeriod_Constructor_Chart_IsNotNull()
        {
            var ClassPeriod = new ClassPeriod(1, 1);
            var chart = ClassPeriod.GetClassroomSeatingChart();
            
            chart.ShouldNotBeNull();
        }

        [Fact]
        public void ClassPeriod_Constructor_ChartDimensions_AreRowsTimesColumns()
        {
            int rows = DefaultRows;
            int columns = DefaultColumns;
            var ClassPeriod = DefaultClassPeriod;

            var seatingChart = ClassPeriod.GetClassroomSeatingChart();
            seatingChart.Length.ShouldBe(rows * columns);
        }

        [Fact(DisplayName = "Default seating chart is filled with x's")]
        public void ClassPeriod_GetStudentSeatingChart_Returns2DArray_WithX()
        {
            int rows = DefaultRows;
            int columns = DefaultColumns;
            var seatingChart = DefaultClassPeriod.GetClassroomSeatingChart();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    seatingChart[i, j].ShouldBe("x");
                }
            }

        }

        [Fact]
        public void ClassPeriod_Students_IsInitiallyEmpty()
        {
            var ClassPeriod = DefaultClassPeriod;
            ClassPeriod.Students.ShouldBeEmpty();
        }

        [Fact]
        public void ClassPeriod_AddStudent_ReturnsTrue()
        {
            // Given
            var ClassPeriod = DefaultClassPeriod;

            // When
            var result = ClassPeriod.AddStudent(new StudentName("student", "name"));
            // Then
            result.ShouldBeTrue();
            ClassPeriod.Students.Count.ShouldBe(1);

        }

        [Fact]
        public void ClassPeriod_GetStudentSeatingChart_StartsWithAddedStudent()
        {
            // Given
            var ClassPeriod = new ClassPeriod(DefaultRows, DefaultColumns);
            var defaultStudent = new StudentName("student", "name");
            //When
            ClassPeriod.AddStudent(defaultStudent);

            var chart = ClassPeriod.GetClassroomSeatingChart();

            chart[0, 0].ShouldBe(defaultStudent.FullName);
        }
        

    }
}