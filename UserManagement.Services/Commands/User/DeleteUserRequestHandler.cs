using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Application.Requests.UserR
{
    //Imagine this is more complex XD
    public class DeleteUserCommand : IRequest<string>
    {
        public long Id { get; set; }
        public string Forename { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateOnly DateOfBirth { get; set; } = default!;
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService;
        public DeleteUserCommandHandler(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        public Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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

                _userService.Delete(user);

                var log = "User deleted successfully";

                _logService.Save(new Log { UserId = user.Id, Timestamp = DateTime.Now, Action = log });
                return Task.FromResult(log);
            }
            catch (Exception)
            {
                return Task.FromResult("Something has gone wrong");
            }
        }
    }
}
