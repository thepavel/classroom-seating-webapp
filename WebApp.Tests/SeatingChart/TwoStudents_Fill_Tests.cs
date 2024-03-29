﻿using Shouldly;
using WebApp.Helpers;

namespace WebApp.Tests;

public class TwoStudents_Fill_Tests
{
    public TwoStudents_Fill_Tests() 
    {
        Students = new List<StudentName> {
            new StudentName("first", "student"),
            new StudentName("next", "pupil")
        };
    }

    public List<StudentName> Students { get; }
    public string SecondStudentName => Students[1].FullName;

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void SecondStudentIsIgnored_In_1x1_Chart(bool useAltFill) 
    {
        //given 
        var seatingChart = new SeatingChart(1, 1, Students, useAltFill);

        //when
        var student = seatingChart.Chart[0,0];

        //then
        student.ShouldBe(Students[0].FullName);
    }

    //combine with other tests if possible
    [Theory] //TODO: update for 2nd student check
    [InlineData(1, 2, 0, 0, 0)] // 1x2 grid should put 1st student into [0,0]
    [InlineData(2, 1, 0, 0, 0)] // 2x1 grid should put 1st student into [0,0]
    public void StudentTakesCorrectPlaceInChart_UpUntilNoMoreUncrowdedPlaces(int rows, int columns, int expectedRowIndex, int expectedColumnIndex, int studentIndex)
    {
        //given
        var seatingChart = new SeatingChart(rows, columns, Students, useAlternateFill: true);

        //when
        var student = seatingChart.Chart[expectedRowIndex, expectedColumnIndex];

        //then
        student.ShouldBe(Students[studentIndex].FullName);
    }

    [Theory]
    [InlineData(2, 1, 1, 0)] // 2x1 grid should put 2nd student into [1,0]
    [InlineData(1, 2, 0, 1)] // 1x2 grid should put 2nd student into [0,1]
    [InlineData(1, 3, 0, 2)] // 1x3 grid should put 2nd student into [0,2]
    [InlineData(1, 4, 0, 2)] // 1x4 grid should put 2nd student into [0,2]
    [InlineData(3, 1, 2, 0)] // 3x1 grid should put 2nd student into [2,0]
    [InlineData(4, 1, 2, 0)] // 4x1 grid should put 2nd student into [2,0]
    [InlineData(2, 2, 1, 1)] // 2x2 grid should put 2nd student into [1,1]
    [InlineData(2, 3, 0, 2)] // 2x3 grid should put 2nd student into [0,2]
    [InlineData(3, 2, 1, 1)] // 3x2 grid should put 2nd student into [1,1]
    public void SecondStudentTakesCorrectPlaceInChart_UpUntilNoMoreUncrowdedPlaces(int rows, int columns, int expectedRowIndex, int expectedColumnIndex)
    {
        //given
        var seatingChart = new SeatingChart(rows, columns, Students, useAlternateFill: true);

        //when
        var student = seatingChart.Chart[expectedRowIndex, expectedColumnIndex];

        //then
        student.ShouldBe(SecondStudentName);
    }
}
