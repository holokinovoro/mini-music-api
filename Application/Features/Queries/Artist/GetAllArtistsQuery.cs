﻿using Application.Dto;
using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Artist
{
    public class GetAllArtistsQuery : IRequest<List<ArtistDto>>{}

    public class GetAllArtistQueryHandler : IRequestHandler<GetAllArtistsQuery, List<ArtistDto>>
    {
        private readonly IArtistRepository _artistRepository;

        public GetAllArtistQueryHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }
        public async Task<List<ArtistDto>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
        {
            var artists = await _artistRepository.GetArtists(cancellationToken);

            var response = artists.Select(s => new ArtistDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return response;
        }
    }
}
