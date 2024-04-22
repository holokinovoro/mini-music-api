using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Application.Features.Commands.SongCommands.CreateSong;
using Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Application.Features.Queries.Song.GetSong;

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

        /*[HttpPut("{songId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSong(int songId, [FromBody] SongDto updatedSong)
        {
            if (updatedSong == null)
                return BadRequest(ModelState);

            if (songId != updatedSong.Id)
                return BadRequest(ModelState);

            if (!_songRepository.SongExists(songId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var songMap = _mapper.Map<Song>(updatedSong);

            if (!_songRepository.UpdateSong(songMap))
            {
                ModelState.AddModelError("", "Somethin wents wrong updating song");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }*/

      /*  [HttpDelete("{songId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSong(int songId)
        {
            if (!_songRepository.SongExists(songId))
            {
                return NotFound();
            }

            var songToDelete = _songRepository.GetSong(songId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_songRepository.DeleteSong(songToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting song");
            }

            return NoContent();
        }*/
    }
}