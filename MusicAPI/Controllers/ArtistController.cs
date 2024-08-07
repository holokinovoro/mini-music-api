﻿using Application.Dto;
using Application.Features.Commands.ArtistCommands.Create;
using Application.Features.Commands.ArtistCommands.Delete;
using Application.Features.Commands.ArtistCommands.Update;
using Application.Features.Queries.Artist;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/artists")]
    [Authorize]
    public class ArtistController : ControllerBase
    {
        private readonly ILogger<ArtistController> _logger;
        private readonly IMediator _mediator;

        public ArtistController(
            ILogger<ArtistController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{artistId}/artist")]
        [Authorize(Policy = "PermissionRead")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtistById(int artistId)
        {
            var request = new GetArtistByIdQuery
            {
                ArtistId = artistId
            };

            var response = await _mediator.Send(request);

            if (response == null)
                _logger.LogError("Failed get session for artist");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Get session for artist: {Id}", request.ArtistId);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = "PermissionRead")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtists()
        {
            var request = new GetAllArtistsQuery();
            var response = await _mediator.Send(request);

            if(response == null)
            {
                _logger.LogError("Failed to get session for artists");
            }

            _logger.LogInformation("Get session for artists");
            return Ok(response);
        }

        [HttpGet("artist/genre/{genreId}")]
        [Authorize(Policy = "PermissionRead")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtistsByGenreId(int genreId)
        {
            var request = new GetArtistsByGenre
            {
                GenreId = genreId
            };

            var response = await _mediator.Send(request);

            if (response == null)
                _logger.LogError("Failed Get session for artist");

            _logger.LogInformation("Get session for artist");
            return Ok(response);
        }

        [HttpGet("artist/{songId}")]
        [Authorize(Policy = "PermissionRead")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtistBySongId(int songId)
        {
            var request = new GetArtistBySongIdQuery
            {
                SongId = songId
            };

            var response = await _mediator.Send(request);
            if (response == null)
                _logger.LogError("Failed Get session for artist");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Get session for artist");
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "PermissionCreate")]
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
            {
                _logger.LogError("Failed Create session for artist: {Id}", request.CreateArtist.Id);
                return BadRequest();
            }

            _logger.LogInformation("Create session for artist: {Id}", request.CreateArtist.Id);
            return NoContent();
        }

        [HttpPut("{artistId}")]
        [Authorize(Policy = "PermissionUpdate")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateArtist(
            [FromQuery] int genreId,
            [FromBody] ArtistDto updatedArtist)
        {
            if (updatedArtist == null)
                return BadRequest(ModelState);


            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed Update session for artist");
                return BadRequest();
            }

            var request = new UpdateArtistCommand
            {
                GenreId = genreId,
                ArtistUpdate = updatedArtist
            };

            await _mediator.Send(request);

            _logger.LogInformation("Update session for artist: {Id}", request.ArtistUpdate.Id);
            return NoContent();
        }

        [HttpDelete("{artistId}")]
        [Authorize(Policy = "PermissionDelete")]
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
            {
                _logger.LogError("Failed Delete session for artist: {Id}", request.Id);
                return BadRequest();
            }

            await _mediator.Send(request);

            _logger.LogInformation("Delete session for artist: {Id}", request.Id);
            return NoContent();
        }
    }
}