using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.UserCommands.Delete;

public class DeleteUserCommand : IRequest
{
    public Guid userId { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(
        ILogger<DeleteUserCommandHandler> logger,
        IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }


    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserById(request.userId, cancellationToken);

            if (user is not null)
                await _userRepository.Delete(user.Id, cancellationToken);

            _logger.LogInformation("user deleted {Id}", user.Id);

        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during deleting user");
            throw;
        }
    }
}

