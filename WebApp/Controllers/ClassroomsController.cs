using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
public class ClassroomsController : Controller
{

    

    public ClassroomsController()
    { 

    }

    [HttpGet]
    public IEnumerable<string> GetClassrooms()
    {
        return new List<string>() {"a", "new", "string", "list"};
    }



}

