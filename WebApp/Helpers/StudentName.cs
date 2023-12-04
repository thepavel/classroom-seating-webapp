
namespace WebApp.Helpers;

public class StudentName
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// creates new student name object from first and last name
    /// </summary>
    /// <param name="first">First name</param>
    /// <param name="last">Last name</param>
    public StudentName(string first, string last)
    {
        FirstName = first;
        LastName = last;
    }


}