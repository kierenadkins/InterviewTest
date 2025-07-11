using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using UserManagement.Application.Requests.UserR;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private ISender _sender;
    public UsersController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<ViewResult> List()
    {
        var response =  await _sender.Send(new GetAllUsersRequest());
        var items = response.Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        }).ToList();

        var model = new UserListViewModel
        {
            Items = items
        };

        return View(model);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(UserListItemViewModel model)
    {
        await _sender.Send(new DeleteUserCommand
        {
            Id = model.Id,
            Forename = model.Forename ?? "",
            Surname = model.Surname ?? "",
            Email = model.Email ?? "",
            IsActive = model.IsActive,
            DateOfBirth = model.DateOfBirth
        });

        return RedirectToAction(nameof(List));
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddAsync(AddUserModel model)
    {
        var validator = new AddUserModelValidator().Validate(model);

        if (!validator.IsValid)
        {
            model.Errors = validator.Errors.Select(e => e.ErrorMessage).ToList();
            return View(model);
        }

        await _sender.Send(new SaveUserCommand
        {
            Forename = model.Forename ?? "",
            Surname = model.Surname ?? "",
            Email = model.Email ?? "",
            IsActive = model.IsActive,
            DateOfBirth = model.DateOfBirth
        });

        return RedirectToAction(nameof(List));
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> EditAsync(int id)
    {
        var user = await _sender.Send(new GetUserRequest { id = id});

        if (user == null)
            return NotFound();

        var model = new EditUserModel
        {
            Id = (int) user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth
        };
        return View(model);
    }

    [HttpPost("edit")]
    public async Task<IActionResult> EditAsync(EditUserModel model)
    {
        var validator = new EditUserModelValidator().Validate(model);

        if (!validator.IsValid)
        {
            model.Errors = validator.Errors.Select(e => e.ErrorMessage).ToList();
            return View(model);
        }

        await _sender.Send(new UpdateUserCommand
        {
            Id = model.Id,
            Forename = model.Forename ?? "",
            Surname = model.Surname ?? "",
            Email = model.Email ?? "",
            IsActive = model.IsActive,
            DateOfBirth = model.DateOfBirth
        });

        return RedirectToAction("List");
    }

    [HttpGet("View/{id}")]
    public async Task<IActionResult> ViewAsync(int id)
    {
        var user = await _sender.Send(new GetUserRequest { id = id });
        if (user == null)
            return NotFound();

        var model = new ViewUserModel
        {
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth
        };
        return View(model);
    }

    public class AddUserModelValidator : AbstractValidator<AddUserModel>
    {
        public AddUserModelValidator()
        {
            RuleFor(x => x.Forename)
                .NotEmpty().WithMessage("Forename is required.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.");
        }
    }
    public class EditUserModelValidator : AbstractValidator<EditUserModel>
    {
        public EditUserModelValidator()
        {
            RuleFor(x => x.Forename)
                .NotEmpty().WithMessage("Forename is required.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.");
        }
    }
}
