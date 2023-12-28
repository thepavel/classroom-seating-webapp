using System.Configuration.Assemblies;
using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests
{
    public class ClassPeriodTests
    {
        private const int DefaultRows = 4;
        private const int DefaultColumns = 4;
        private readonly ClassPeriod DefaultClassPeriod;
        private readonly ClassPeriod MinimalClassPeriod;

        public ClassPeriodTests()
        {
            DefaultClassPeriod = new ClassPeriod(DefaultRows, DefaultColumns);
            MinimalClassPeriod = new ClassPeriod(1, 1);
        }

        [Fact]
        public void ClassPeriod_Constructor_Chart_IsNotNull()
        {
            var chart = MinimalClassPeriod.Chart;

            chart.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 4)]
        public void ClassPeriodDimensions_ShouldReflectRowsAndColumns(int rows, int columns, int size) 
        {
            var classPeriod = new ClassPeriod(rows, columns);
            classPeriod.Size.ShouldBe(size);
        }

        [Fact]
        public void ClassPeriod_Constructor_ChartDimensions_AreRowsTimesColumns()
        {
            var seatingChart = DefaultClassPeriod.Chart;
            seatingChart.Length.ShouldBe(DefaultRows * DefaultColumns);
        }

        [Fact(DisplayName = "Default seating chart is filled with x's")]
        public void ClassPeriod_GetStudentSeatingChart_Returns2DArray_WithX()
        {
            var seatingChart = DefaultClassPeriod.Chart;

            for (int i = 0; i < DefaultRows; i++)
            {
                for (int j = 0; j < DefaultColumns; j++)
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
            var classPeriod = new ClassPeriod(DefaultRows, DefaultColumns);
            var defaultStudent = new StudentName("student", "name");
            //When
            classPeriod.AddStudent(defaultStudent);

            var chart = classPeriod.Chart;

            chart[0, 0].ShouldBe(defaultStudent.FullName);
        }

        [Fact]
        public void ClassPeriod_AddTwoStudents_PutsStudentsInCorrectPlaces()
        {
            //given a Default classroom: 4x4
            var defaultStudent = new StudentName("first", "student");
            var secondStudent = new StudentName("another", "pupil");

            var result = DefaultClassPeriod.AddStudent(defaultStudent);

            //when 
            result = DefaultClassPeriod.AddStudent(secondStudent);

            //then
            result.ShouldBeTrue();
            var chart = DefaultClassPeriod.Chart;
            chart[0,1].ShouldBe("x");
            chart[0,2].ShouldBe(DefaultClassPeriod.Students[1].FullName);
        }

        [Fact]
        public void ClassPeriod_CanAddStudentsUntilLimitIsReached()
        {
            //given 
            var classPeriod = new ClassPeriod(1, 1);
            var defaultStudent = new StudentName("student", "name");

            //when 
            var result = classPeriod.AddStudent(defaultStudent);

            //then
            result.ShouldBeTrue();
        }

        [Fact]
        public void ClassPeriod_CannotAddStudents_AfterLimitIsReached() 
        {
            //given 
            var defaultStudent = new StudentName("default", "student");
            var anotherStudent = new StudentName("another", "student");

            var result = MinimalClassPeriod.AddStudent(defaultStudent);

            //when
            result = MinimalClassPeriod.AddStudent(anotherStudent);

            //then
            result.ShouldBeFalse();
        }

        [Fact]
        public void ClassPeriod_WhenStudentsCannotBeAdded_ThenChartStaysUnchanged() 
        {
            //given 
            var defaultStudent = new StudentName("default", "student");
            var anotherStudent = new StudentName("another", "student");
            var result = MinimalClassPeriod.AddStudent(defaultStudent);

            //when
            result = MinimalClassPeriod.AddStudent(anotherStudent);

            //then
            MinimalClassPeriod.Students.Count.ShouldBe(1);
            MinimalClassPeriod.Students.First().FirstName.ShouldBe("default");
        }

    }
}