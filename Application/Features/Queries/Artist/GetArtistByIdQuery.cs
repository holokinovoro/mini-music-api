using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;

namespace Application.Features.Queries.Artist;

public class GetArtistByIdQuery : IRequest<ArtistDto>
{
    public int ArtistId { get; set; }
}

public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, ArtistDto>
{
    private readonly IArtistRepository _artistRepository;

    public GetArtistByIdQueryHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }
    public async Task<ArtistDto> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetArtist(request.ArtistId, cancellationToken);

        var response = new ArtistDto
        {
            Id = artist.Id,
            Name = artist.Name
        };

        return response;
    }
}
