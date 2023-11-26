using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ClassroomInfoService _classroomService;

    public ClassroomModel Classroom {get; private set;}

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _classroomService = new ClassroomInfoService();

        Classroom = _classroomService.GetClassroomInfo();
    }

    public void OnGet()
    {

    }
}
