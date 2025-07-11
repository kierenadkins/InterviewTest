using System.Linq;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Application.Requests.UserR;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("Logs")]
public class LogController : Controller
{
    private ISender _sender;
    public LogController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<ViewResult> List()
    {
        var response = await _sender.Send(new GetLogRequest());
        var model = response.Select(p => new LogUserModel
        {
            UserId = (int) p.UserId,
            Action = p.Action,
            Timestamp = p.Timestamp
        }).ToList();

        return View(model);
    }
}

