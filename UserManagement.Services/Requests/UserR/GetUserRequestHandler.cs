using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Domain.Objects;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Application.Requests.UserR
{
    // Request that asks for a user and their logs by ID
    public class GetUserRequest : IRequest<GetUserRequestResult>
    {
        public int Id { get; set; }
    }

    // Handler that handles the GetUserRequest
    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, GetUserRequestResult>
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService;

        public GetUserRequestHandler(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        public Task<GetUserRequestResult> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = _userService.GetById(request.Id);
            var userLogs = new List<Log>();

            if (user != null)
            {
                userLogs = _logService.GetByUserId((int)user.Id);

                return Task.FromResult(new GetUserRequestResult
                {
                    user = user,
                    logs = userLogs
                });
            }

            return Task.FromResult(new GetUserRequestResult
            {});
        }
    }
}
