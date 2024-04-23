﻿using Application.Dto;
using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Genre
{
    public class GetAllGenres : IRequest<List<GenreDto>>
    {
    }

    public class GetAllGenresHandle : IRequestHandler<GetAllGenres, List<GenreDto>>
    {
        private readonly IGenreRepository _genreRepository;

        public GetAllGenresHandle(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<List<GenreDto>> Handle(GetAllGenres request, CancellationToken cancellationToken)
        {
            var genres = await _genreRepository.GetGenres(cancellationToken);

            var response = genres.Select(s => new GenreDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return response;
        }
    }
}
