namespace WebApp.Helpers;
public class NameParser
{
    public static StudentName FromString(string name)
    {
        var firstName = name.Split(" ").First();
        var lastName = name.Split(" ").Last();
        return new StudentName(firstName, lastName);
    }


}
