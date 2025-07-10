using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Application.Requests.UserR
{
    //Imagine this is more complex XD
    public class SaveUserCommand : IRequest<string>
    {
        public string Forename { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateOnly DateOfBirth { get; set; } = default!;
    }

    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, string>
    {
        private readonly IUserService _userService;
        public SaveUserCommandHandler(IUserService userService) => _userService = userService;

        public Task<string> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    Forename = request.Forename ?? "",
                    Surname = request.Surname ?? "",
                    Email = request.Email ?? "",
                    IsActive = request.IsActive,
                    DateOfBirth = request.DateOfBirth
                };

                _userService.Save(user);

                return Task.FromResult("User saved successfully");
            }
            catch (Exception)
            {
                return Task.FromResult("Something has gone wrong");
            }
        }
    }
}
