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
    public class GetAllUsersRequest : IRequest<List<User>>
    {
    }

    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, List<User>>
    {
        private readonly IUserService _userService;
        public GetAllUsersRequestHandler(IUserService userService) => _userService = userService;

        public Task<List<User>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var users = _userService.GetAll();
            return Task.FromResult(users.ToList());
        }
    }
}
