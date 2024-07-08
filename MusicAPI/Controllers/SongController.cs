using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Application.Features.Commands.SongCommands.CreateSong;
using Domain.Models;
using Application.Features.Queries.Song.GetSong;
using Application.Features.Commands.SongCommands.Update;
using Application.Features.Commands.SongCommands.Delete;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Authentication;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/songs")]
    [Authorize]
    public class SongController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SongController(
            ILogger logger,
            IMapper mapper,
            IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "PermissionRead")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Song>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllSongs()
        {
            var request = new GetAllSongsQuery();
            var response = await _mediator.Send(request);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed Get Session for song");
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Get session for song");
            return Ok(response);
        }

        [HttpGet("{songId}")]
        [Authorize(Policy = "PermissionRead")]
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
            {
                _logger.LogError("Failed Get Session for song {Id}", songId);
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Get session for song {Id}", songId);
            return Ok(response);
        }

        [HttpGet("/artist/{artistId}")]
        [Authorize(Policy = "PermissionRead")]
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
            {
                _logger.LogError("Failed Get Session for song by artist {Id}", artistId);
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Get Session for song by artist {Id}", artistId);
            return Ok(response);
        }

        [HttpGet("{genreId}/songs")]
        [Authorize(Policy = "PermissionRead")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Song>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSongsByGenre(int genreId)
        {
            var request = new GetSongsByGenreQuery
            {
                GenreId = genreId
            };

            var response = await _mediator.Send(request);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed Get Session for song by genre {Id}", genreId);
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Get Session for song by genre {Id}", genreId);

            return Ok(response);
        }


        [HttpPost]
        [Authorize(Policy = "PermissionCreate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSong(CreateSongCommand command)
        {
            var response = await _mediator.Send(command);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed Create Session for song {Id}", command.createSong.Id);
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Create Session for song {Id}", command.createSong.Id);
            return NoContent();
        }

        [HttpPut("{songId}")]
        [Authorize(Policy = "PermissionUpdate")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateSong([FromBody] SongDto updatedSong)
        {
            if (updatedSong == null)
                return BadRequest(ModelState);

            var request = new UpdateSongCommand
            {
                UpdateSong = updatedSong
            };

            await _mediator.Send(request);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed Update Session for song {Id}", updatedSong.Id);
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Update Session for song {Id}", updatedSong.Id);
            return NoContent();
        }

        [HttpDelete("{songId}")]
        [Authorize(Policy = "PermissionDelete")]
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
            {
                _logger.LogError("Failed Delete Session for song {Id}", songId);
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Delete Session for song {Id}", songId);
            return NoContent();
        }
    }
}