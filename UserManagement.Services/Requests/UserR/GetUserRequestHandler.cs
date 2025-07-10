using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Application.Requests.UserR
{
    //Imagine this is more complex XD
    public class GetUserRequest : IRequest<User>
    {
        public int id { get; set; }
    }

    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, User?>
    {
        private readonly IUserService _userService;
        public GetUserRequestHandler(IUserService userService) => _userService = userService;

        public Task<User?> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = _userService.GetById(request.id);
            return Task.FromResult(user);
        }
    }
}
