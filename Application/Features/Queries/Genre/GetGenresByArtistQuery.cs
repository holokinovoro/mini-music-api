using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;

namespace Application.Features.Queries.Genre;

public class GetGenresByArtistQuery : IRequest<List<GenreDto>>
{
    public int artistId { get; set; }
}

public class GetGenresByArtistQueryHandler : IRequestHandler<GetGenresByArtistQuery, List<GenreDto>>
{
    private readonly IGenreRepository _genreRepository;

    public GetGenresByArtistQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    public async Task<List<GenreDto>> Handle(GetGenresByArtistQuery request, CancellationToken cancellationToken)
    {
        var genres = await _genreRepository.GetGenresByArtist(request.artistId, cancellationToken);

        var response = genres.Select(s => new GenreDto
        {
            Id = s.Id,
            Name = s.Name
        }).ToList();

        return response;
    }
}
