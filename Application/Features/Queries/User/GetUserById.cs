using Application.Dto;
using Application.Interfaces.IRepository;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Features.Queries.User;

public class GetUserById : IRequest<UserDto>
{
    public Guid Id { get; set; }
}

public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(
        IMapper mapper,
        ILogger logger,
        IUserRepository userRepository
        )
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
    }


    public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserById(request.Id, cancellationToken);
            if (user == null)
                _logger.LogError("user not found");

            return _mapper.Map<UserDto>(user);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during gettin user");
            throw;
        }
    }
}
