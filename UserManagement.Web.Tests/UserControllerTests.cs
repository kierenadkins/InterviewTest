using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Requests.UserR;
using UserManagement.Models;

using UserManagement.Web.Models.Users;
using UserManagement.WebMS.Controllers;
using static UserManagement.WebMS.Controllers.UsersController;

namespace UserManagement.Data.Tests;

public class UserControllerTests
{
    [Fact]
    public async Task GetUsers_WhenCalled_ReturnsOkWithUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Forename = "John", Surname = "Doe", Email = "john@example.com", IsActive = true }
        };

        _sender
            .Setup(s => s.Send(It.IsAny<GetAllUsersRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        var controller = CreateController();

        // Act
        var result = await controller.List();

        // Assert
        result.Model
             .Should().BeOfType<UserListViewModel>()
             .Which.Items.Should().BeEquivalentTo(users);
    }

    [Fact]
    public void Add_Get_ShouldReturnView()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.Add();

        // Assert
        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public async Task AddAsync_WhenModelInvalid_ShouldReturnViewWithErrors()
    {
        // Arrange
        var controller = CreateController();
        var invalidModel = new AddUserModel();
        var validator = new AddUserModelValidator();

        // Act
        var result = await controller.AddAsync(invalidModel);

        // Assert
        var viewResult = result as ViewResult;
        viewResult.Should().NotBeNull();

        var model = viewResult!.Model as AddUserModel;
        model.Should().NotBeNull();
        model.Errors.Should().NotBeEmpty();
        model.Errors.Should().Contain("Forename is required.");
        model.Errors.Should().Contain("Surname is required.");
        model.Errors.Should().Contain("Email is required.");
        model.Errors.Should().Contain("Date of Birth is required.");

        _sender.Verify(s => s.Send(It.IsAny<SaveUserCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task AddAsync_WhenModelIsValid_ShouldSendCommandAndRedirect()
    {
        // Arrange
        var controller = CreateController();
        var validModel = new AddUserModel
        {
            Forename = "Jane",
            Surname = "Doe",
            Email = "jane.doe@example.com",
            IsActive = true,
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

        // Act
        var result = await controller.AddAsync(validModel);

        // Assert
        _sender.Verify(s => s.Send(It.Is<SaveUserCommand>(cmd =>
            cmd.Forename == "Jane" &&
            cmd.Surname == "Doe" &&
            cmd.Email == "jane.doe@example.com" &&
            cmd.IsActive == true &&
            cmd.DateOfBirth == new DateOnly(1990, 1, 1)
        ), It.IsAny<CancellationToken>()), Times.Once);

        var redirectResult = result as RedirectToActionResult;
        redirectResult.Should().NotBeNull();
        redirectResult!.ActionName.Should().Be("List");
    }

    private readonly Mock<ISender> _sender = new();
    private UsersController CreateController() => new(_sender.Object);

}
