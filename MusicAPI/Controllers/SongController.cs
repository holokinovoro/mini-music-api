﻿using AutoMapper;
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

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/songs")]
    [Authorize]
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
        [Authorize(Policy = "PermissionRead")]
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
                return BadRequest(ModelState);
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
                return BadRequest(ModelState);
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
                return BadRequest(ModelState);
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
                return BadRequest(ModelState);

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
                return BadRequest(ModelState);

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
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}