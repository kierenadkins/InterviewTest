using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Application.Requests.UserR
{
    public class GetLogRequest : IRequest<List<Log>>
    {
    }

    public class GetLogRequestHandler : IRequestHandler<GetLogRequest, List<Log>>
    {
        private readonly ILogService _logService;

        public GetLogRequestHandler(ILogService logService)
        {
            _logService = logService;
        }

        public Task<List<Log>> Handle(GetLogRequest request, CancellationToken cancellationToken)
        {
            var logs = _logService.GetAll();
            return Task.FromResult(logs.ToList());
        }
    }
}
