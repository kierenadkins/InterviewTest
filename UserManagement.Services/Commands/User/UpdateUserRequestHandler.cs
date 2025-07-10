using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Application.Requests.UserR
{
    //Imagine this is more complex XD
    public class UpdateUserCommand : IRequest<string>
    {
        public long Id { get; set; }
        public string Forename { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateOnly DateOfBirth { get; set; } = default!;
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly IUserService _userService;
        public UpdateUserCommandHandler(IUserService userService) => _userService = userService;

        public Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    Id = request.Id,
                    Forename = request.Forename ?? "",
                    Surname = request.Surname ?? "",
                    Email = request.Email ?? "",
                    IsActive = request.IsActive,
                    DateOfBirth = request.DateOfBirth
                };

                _userService.Update(user);

                return Task.FromResult("User saved successfully");
            }
            catch (Exception)
            {
                return Task.FromResult("Something has gone wrong");
            }
        }
    }
}
