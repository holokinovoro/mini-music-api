using Application.Dto;
using Application.Features.Commands.GenreCommands.Create;
using Application.Features.Commands.GenreCommands.Delete;
using Application.Features.Commands.GenreCommands.Update;
using Application.Features.Queries.Genre;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/genres")]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGenres()
        {
            var request = new GetAllGenres { };

            var response = await _mediator.Send(request);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(response);
        }

        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGenreById(int genreId)
        {
            var request = new GetGenreById
            {
                Id = genreId
            };

            var response = await _mediator.Send(request);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(response);
        }

        [HttpGet("{artistId}/artist")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGenresByArtistId(int artistId)
        {
            var request = new GetGenresByArtistQuery
            {
                artistId = artistId
            };

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGenre([FromQuery] int artistId, [FromBody] GenreDto genreCreate)
        {
            if (genreCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = new CreateGenreCommand
            {
                artistId = artistId,
                createGenre = genreCreate
            };

            await _mediator.Send(request);

            return NoContent();
        }

        [HttpPut("{genreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateGenre(int genreId,
            [FromQuery] int artistId,
            [FromBody] GenreDto updatedGenre)
        {
            if (updatedGenre == null)
                return BadRequest(ModelState);

            if (genreId != updatedGenre.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var request = new UpdateGenreCommand
            {
                ArtistId = artistId,
                GenreId = genreId,
                Genre = updatedGenre
            };

            await _mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{genreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteGenre(int genreId)
        {

            var request = new DeleteGenreCommand
            {
                GenreId = genreId
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(request);

            return NoContent();
        }
    }
}