using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Requests.UserR;
using UserManagement.Models;

using UserManagement.Web.Models.Users;
using UserManagement.WebMS.Controllers;

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

    private readonly Mock<ISender> _sender = new();
    private UsersController CreateController() => new(_sender.Object);

}
