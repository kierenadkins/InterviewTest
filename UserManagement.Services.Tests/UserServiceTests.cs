using System.Collections.Generic;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class UserServiceTests
{
    private readonly Mock<IDataContext> _dataContext = new();
    private UserService CreateService() => new(_dataContext.Object);

    [Fact]
    public void GetAll_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange
        var service = CreateService();
        var users = SetupUsers();

        // Act
        var result = service.GetAll();

        // Assert
        result.Should().BeSameAs(users);
    }

    [Fact]
    public void Save_ShouldCallCreateWithCorrectUser()
    {
        // Arrange
        var service = CreateService();
        var user = new User();

        // Act
        service.Save(user);

        // Assert
        _dataContext.Verify(x => x.Create(user), Times.Once);
    }

    [Fact]
    public void Delete_ShouldCallDeleteWithCorrectUser()
    {
        // Arrange
        var service = CreateService();
        var user = new User();

        // Act
        service.Delete(user);

        // Assert
        _dataContext.Verify(x => x.Delete(user), Times.Once);
    }

    [Fact]
    public void Update_ShouldCallUpdateWithCorrectUser()
    {
        // Arrange
        var service = CreateService();
        var user = new User();

        // Act
        service.Update(user);

        // Assert
        _dataContext.Verify(x => x.Update(user), Times.Once);
    }

    [Fact]
    public void GetById_WhenUserExists_ReturnsCorrectUser()
    {
        // Arrange
        var service = CreateService();
        var users = new List<User>
        {
            new User { Id = 1, Forename = "John" },
            new User { Id = 2, Forename = "Jane" }
        }.AsQueryable();

        _dataContext.Setup(x => x.GetAll<User>()).Returns(users);

        // Act
        var result = service.GetById(2);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(2);
        result.Forename.Should().Be("Jane");
    }

    [Fact]
    public void GetById_WhenUserDoesNotExist_ReturnsNull()
    {
        // Arrange
        var service = CreateService();
        var users = new List<User>
        {
            new User { Id = 1, Forename = "John" }
        }.AsQueryable();

        _dataContext.Setup(x => x.GetAll<User>()).Returns(users);

        // Act
        var result = service.GetById(999);

        // Assert
        result.Should().BeNull();
    }

    private IQueryable<User> SetupUsers(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true)
    {
        var users = new[]
        {
            new User
            {
                Forename = forename,
                Surname = surname,
                Email = email,
                IsActive = isActive
            }
        }.AsQueryable();

        _dataContext
            .Setup(s => s.GetAll<User>())
            .Returns(users);

        return users;
    }
}
