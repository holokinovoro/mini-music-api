using Application.Dto;
using Application.Features.Commands.ArtistCommands.Create;
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

        /*  [HttpGet("{id}/artist")]
          [ProducesResponseType(200, Type = typeof(Artist))]
          [ProducesResponseType(400)]
          public IActionResult GetArtist(int id)
          {
              if (!_artistRepository.ArtistExists(id))
                  return NotFound();

              var artist = _mapper.Map<ArtistDto>(_artistRepository.GetArtist(id));
              if (!ModelState.IsValid)
                  return BadRequest(ModelState);

              return Ok(artist);
          }*/

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetArtists()
        {
            var request = new GetAllArtistsQuery();
            var response = await _mediator.Send(request);

            return Ok(response);
        }

    /*    [HttpGet("songs/{artistId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Song>))]
        [ProducesResponseType(400)]
        public IActionResult GetSongsByArtistId(int artistId)
        {
            if (!_artistRepository.ArtistExists(artistId))
                return NotFound();
            var songs = _mapper.Map<List<SongDto>>(_artistRepository.GetSongsFromArtist(artistId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(songs);
        }

        [HttpGet("genres/{artistId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        [ProducesResponseType(400)]
        public IActionResult GetGenresByArtistId(int artistId)
        {
            if (!_artistRepository.ArtistExists(artistId))
                return NotFound();
            var genres = _mapper.Map<List<GenreDto>>(_artistRepository.GetGenreByArtist(artistId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genres);
        }

        [HttpGet("artist/{songId}")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public IActionResult GetArtistBySongId(int songId)
        {
            var artist = _mapper.Map<ArtistDto>(_artistRepository.GetArtisyBySong(songId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(artist);
        }*/

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

        /* [HttpPut("{artistId}")]
         [ProducesResponseType(400)]
         [ProducesResponseType(204)]
         [ProducesResponseType(404)]
         public IActionResult UpdateArtist(int artistId,
             [FromQuery] int genreId,
             [FromBody] ArtistDto updatedArtist)
         {
             if (updatedArtist == null)
                 return BadRequest(ModelState);

             if (artistId != updatedArtist.Id)
                 return BadRequest(ModelState);

             if (!_artistRepository.ArtistExists(artistId))
                 return NotFound();

             if (!ModelState.IsValid)
                 return BadRequest();

             var artistMap = _mapper.Map<Artist>(updatedArtist);

             if (!_artistRepository.UpdateArtist(genreId, artistMap))
             {
                 ModelState.AddModelError("", "Something went wrong updating artist");
                 return StatusCode(500, ModelState);
             }

             return NoContent();
         }*/

        /*[HttpDelete("{artistId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteArtist(int artistId)
        {
            if (!_artistRepository.ArtistExists(artistId))
                return NotFound();

            var artistToDelete = _artistRepository.GetArtist(artistId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_artistRepository.DeleteArtist(artistToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting artist");
            }

            return NoContent();
        }*/
    }
}