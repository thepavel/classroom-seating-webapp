using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages;

public class ClassroomPeriodModel : PageModel
{
    private readonly ILogger<ClassroomPeriodModel> _logger;

    [FromQuery(Name = "period")]
    public int PeriodNumber { get; set; }

    public ClassroomPeriodModel(ILogger<ClassroomPeriodModel> logger)
    {
        _logger = logger;
    }
}