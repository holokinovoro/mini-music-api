using Application.Dto;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.User;

public class GetUsersQuery : IRequest<List<UserDto>>{}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly ILogger<GetUsersQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(
        ILogger<GetUsersQueryHandler> logger,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
    }


    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository.GetUsers(cancellationToken);

            if (users == null)
            {
                _logger.LogError("Users not found");
                throw new ArgumentNullException(nameof(users));
            }

            _logger.LogInformation("Users taken");

            return _mapper.Map<List<UserDto>>(users);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during getting users");
            throw;
        }
    }
}
