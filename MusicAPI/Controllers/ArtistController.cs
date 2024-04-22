using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Domain.Models;

namespace MusicAPI.Controllers
{
   /* [ApiController]
    [Route("api/artists")]
    public class ArtistController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ArtistController(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }
*/
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

        /*[HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        [ProducesResponseType(400)]
        public IActionResult GetArtists()
        {
            var artists = _mapper.Map<List<ArtistDto>>(_artistRepository.GetArtists());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(artists);
        }

        [HttpGet("songs/{artistId}")]
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

        /*[HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateArtist([FromQuery] int genreId, [FromBody] ArtistDto artistCreate)
        {
            if (artistCreate == null)
                return BadRequest(ModelState);

            var artists = _artistRepository.GetArtistTrimToUpper(artistCreate);

            if (artists != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var artistMap = _mapper.Map<Artist>(artistCreate);

            if(!_artistRepository.CreateArtist(genreId, artistMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }*/

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
/*

    }*/
}
