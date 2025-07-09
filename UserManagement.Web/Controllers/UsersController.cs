using System.Linq;
using FluentValidation;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpPost("delete")]
    public IActionResult Delete(UserListItemViewModel model)
    {
        var user = new User
        {
            Forename = model.Forename ?? "",
            Surname = model.Surname ?? "",
            Email = model.Email ?? "",
            IsActive = model.IsActive,
            DateOfBirth = model.DateOfBirth
        };

        _userService.Delete(user);
        return RedirectToAction(nameof(List));
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost("add")]
    public IActionResult Add(AddUserModel model)
    {
        var validator = new AddUserModelValidator().Validate(model);

        if (!validator.IsValid)
        {
            model.Errors = validator.Errors.Select(e => e.ErrorMessage).ToList();
            return View(model);
        }

        var user = new User
        {
            Forename = model.Forename ?? "",
            Surname = model.Surname ?? "",
            Email = model.Email ?? "",
            IsActive = model.IsActive,
            DateOfBirth = model.DateOfBirth
        };

        _userService.Save(user);

        return RedirectToAction(nameof(List));
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
}
