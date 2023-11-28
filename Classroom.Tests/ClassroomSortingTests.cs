namespace Classroom.Tests;
using WebApp.Models;

public class ClassroomSortingTests
{
    [Fact]
    public void CanCreateClassroom()
    {
        var model = new ClassroomModel();
        model.Columns = 1;
        Assert.True(model.Columns == 1);
        //Assert.True(false, "fuck");
    }
}