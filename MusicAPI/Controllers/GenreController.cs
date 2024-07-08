using Application.Dto;
using Application.Features.Commands.GenreCommands.Create;
using Application.Features.Commands.GenreCommands.Delete;
using Application.Features.Commands.GenreCommands.Update;
using Application.Features.Queries.Genre;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicAPI.Controllers;

[ApiController]
[Route("api/genres")]
[Authorize]
public class GenreController : ControllerBase
{
    private readonly ILogger<GenreController> _logger;
    private readonly IMediator _mediator;

    public GenreController(
        ILogger<GenreController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = "PermissionRead")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetGenres()
    {
        var request = new GetAllGenres { };

        var response = await _mediator.Send(request);
        if (!ModelState.IsValid)
        {
            _logger.LogError("Failed Get session for genres");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Get session for genres");
        return Ok(response);
    }

    [Authorize(Policy = "PermissionRead")]
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
        {
            _logger.LogError("Failed Get sessiong for genre: {Id}", request.Id);
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Get session for genre: {Id}", request.Id);
        return Ok(response);
    }

    [Authorize(Policy = "PermissionRead")]
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
        if (!ModelState.IsValid)
        {
            _logger.LogError("Failed Get sessiong for genres");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Get session for genres");
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "PermissionCreate")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateGenre([FromQuery] int artistId, [FromBody] GenreDto genreCreate)
    {
        if (genreCreate == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
        {
            _logger.LogError("Failed to Create sessiong for genre");
            return BadRequest(ModelState);
        }

        var request = new CreateGenreCommand
        {
            artistId = artistId,
            createGenre = genreCreate
        };

        await _mediator.Send(request);

        _logger.LogInformation("Create session for genres");
        return NoContent();
    }

    [HttpPut("{genreId}")]
    [Authorize(Policy = "PermissionUpdate")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateGenre(
        [FromQuery] int artistId,
        [FromBody] GenreDto updatedGenre)
    {
        if (updatedGenre == null)
            return BadRequest(ModelState);



        var request = new UpdateGenreCommand
        {
            ArtistId = artistId,
            Genre = updatedGenre
        };

        await _mediator.Send(request);

        if (!ModelState.IsValid)
        {
            _logger.LogError("Failed to Update sessiong for genre: {Id}", updatedGenre.Id);
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Update sessiong for genre: {Id}", updatedGenre.Id);
        return NoContent();
    }

    [HttpDelete("{genreId}")]
    [Authorize(Policy = "PermissionDelete")]
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
        {
            _logger.LogError("Failed to Delete sessiong for genre: {genreId}", genreId);
            return BadRequest(ModelState);
        }

        await _mediator.Send(request);

        _logger.LogInformation("Delete sessiong for genre: {genreId}", genreId);

        return NoContent();
    }
}