using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ClassroomInfoService _classroomService;

    public ClassroomModel Classroom {get; private set;}
    public List<PeriodRoster> ClassRosters { get; private set;}

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _classroomService = new ClassroomInfoService();

        Classroom = _classroomService.ClassroomInfo;
        ClassRosters = _classroomService.PeriodRosters;
    }

    public void OnGet()
    {

    }
}
