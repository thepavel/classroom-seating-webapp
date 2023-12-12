using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ClassroomInfoService _classroomService;

    public ClassroomModel Classroom { get; private set; }
    private readonly List<PeriodRoster> ClassRosters;
    public List<PeriodRosterViewModel> Rosters { get; private set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _classroomService = new ClassroomInfoService();

        Classroom = _classroomService.ClassroomInfo;
        ClassRosters = _classroomService.PeriodRosters;
        Rosters = (from roster in ClassRosters select new PeriodRosterViewModel(roster)).ToList();
    }

    public void OnGet()
    {

    }
}

public class PeriodRosterViewModel
{
    private readonly PeriodRoster roster;
    public string[] SortedNames { get; private set; }
    public int Period => roster.Period;

    public PeriodRosterViewModel(PeriodRoster roster)
    {
        this.roster = roster;
        SortedNames = roster.GetSortedRoster();

    }
}