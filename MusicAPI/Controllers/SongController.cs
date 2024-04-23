using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Application.Features.Commands.SongCommands.CreateSong;
using Domain.Models;
using Application.Features.Queries.Song.GetSong;
using Application.Features.Commands.SongCommands.Update;
using Application.Features.Commands.SongCommands.Delete;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public class SongController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SongController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Song>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllSongs()
        {
            var request = new GetAllSongsQuery();
            var response = await _mediator.Send(request);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(response);
        }

        [HttpGet("{songId}")]
        [ProducesResponseType(200, Type = typeof(SongDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSong(int songId)
        {
            var request = new GetSongByIdQuery
            {
                SongId = songId
            };

            var response = await _mediator.Send(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(response);
        }

        [HttpGet("/artist/{artistId}")]
        [ProducesResponseType(200, Type = typeof(SongDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSongsByArtist(int artistId)
        {
            var request = new GetSongsByArtist
            {
                ArtistId = artistId
            };

            var response = await _mediator.Send(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(response);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateSong(CreateSongCommand command)
        {
            var response = await _mediator.Send(command);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }

        [HttpPut("{songId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateSong(int songId, [FromBody] SongDto updatedSong)
        {
            if (updatedSong == null)
                return BadRequest(ModelState);

            if (songId != updatedSong.Id)
                return BadRequest(ModelState);

            var request = new UpdateSongCommand
            {
                Id = songId,
                UpdateSong = updatedSong
            };

            await _mediator.Send(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }

        [HttpDelete("{songId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteSong(int songId)
        {

            var request = new DeleteSongCommand
            {
                Id = songId
            };

            await _mediator.Send(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}