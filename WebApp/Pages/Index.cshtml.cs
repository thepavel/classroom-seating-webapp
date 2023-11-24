using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ClassroomInfoService _classroomService;

    public ClassroomModel Classroom {get; set;}

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _classroomService = GetClassroomModel();
        Classroom = _classroomService.GetClassroomModel();
    }

    private ClassroomInfoService GetClassroomModel()
    {
        return new ClassroomInfoService();
    }

    public void OnGet()
    {

    }
}
