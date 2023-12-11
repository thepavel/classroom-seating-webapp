using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages;

public class ClassroomPeriodModel : PageModel
{
    private readonly ILogger<ClassroomPeriodModel> _logger;
    private readonly ClassroomInfoService _classroomService;

    [FromQuery(Name = "period")]
    public int Period { get; set; }

    public ClassPeriod ClassPeriod { get; set; }


    public string ClassroomDimensionsDisplay => $"{ClassPeriod.Rows} x {ClassPeriod.Columns}";
    public int Columns { get; set; }
    public int Rows { get; set; }


    public ClassroomPeriodModel(ILogger<ClassroomPeriodModel> logger)
    {
        _logger = logger;
        _classroomService = new ClassroomInfoService();

        var classroomInfo = _classroomService.ClassroomInfo;

        Columns = classroomInfo.Columns;
        Rows = classroomInfo.Rows;
        ClassPeriod = new ClassPeriod(Rows, Columns);

    }

    public void OnGet()
    {
        //TODO: validate querystring input
        ClassPeriod = LoadRosterForPeriod(Period);
    }

    private ClassPeriod LoadRosterForPeriod(int period)
    {
        var periodRoster = _classroomService.GetPeriodRoster(period);

        var studentNames = RosterSorter.GetSortedStudentNames(periodRoster.StudentNames);
        
        return new ClassPeriod(Rows, Columns, studentNames);
    }

}