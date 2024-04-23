using Application.Dto;
using Application.Features.Commands.ArtistCommands.Create;
using Application.Features.Commands.ArtistCommands.Delete;
using Application.Features.Commands.ArtistCommands.Update;
using Application.Features.Queries.Artist;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/artists")]
    public class ArtistController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ArtistController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{artistId}/artist")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtistById(int artistId)
        {
            var request = new GetArtistByIdQuery
            {
                ArtistId = artistId
            };

            var response = await _mediator.Send(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtists()
        {
            var request = new GetAllArtistsQuery();
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("artist/genre/{genreId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtistsByGenreId(int genreId)
        {
            var request = new GetArtistsByGenre
            {
                GenreId = genreId
            };

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("artist/{songId}")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtistBySongId(int songId)
        {
            var request = new GetArtistBySongIdQuery
            {
                SongId = songId
            };

            var response = await _mediator.Send(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateArtist([FromQuery] int genreId, [FromBody] ArtistDto artistCreate)
        {
            if (artistCreate == null)
                return BadRequest(ModelState);

            var request = new CreateArtistCommand
            {
                GenreId = genreId,
                CreateArtist = artistCreate
            };

            await _mediator.Send(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }

        [HttpPut("{artistId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateArtist(int artistId,
            [FromQuery] int genreId,
            [FromBody] ArtistDto updatedArtist)
        {
            if (updatedArtist == null)
                return BadRequest(ModelState);

            if (artistId != updatedArtist.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var request = new UpdateArtistCommand
            {
                ArtistId = artistId,
                GenreId = genreId,
                ArtistUpdate = updatedArtist
            };

            await _mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{artistId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteArtist(int artistId)
        {
            var request = new DeleteArtistCommand
            {
                Id = artistId
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(request);

            return NoContent();
        }
    }
}