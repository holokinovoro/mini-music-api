using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Domain.Models;

namespace MusicAPI.Controllers
{
    /*[ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly IMapper _mapper;

        public GenreController()
        {
            
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        [ProducesResponseType(400)]
        public IActionResult GetGenres()
        {
            var genres = _mapper.Map<List<GenreDto>>(_genreRepository.GetGenres());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genres);
        }

        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public IActionResult GetGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();
            var genre = _mapper.Map<GenreDto>(_genreRepository.GetGenre(genreId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genre);
        }

        [HttpGet("{genreId}/artists")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        [ProducesResponseType(400)]
        public IActionResult GetArtistsByGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();
            var artists = _mapper.Map<List<ArtistDto>>(_genreRepository.GetArtistsByGenre(genreId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(artists);
        }

        [HttpGet("{genreId}/songs")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Song>))]
        [ProducesResponseType(400)]
        public IActionResult GetSongsByGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();
            var songs = _mapper.Map<List<SongDto>>(_genreRepository.GetSongsByGenre(genreId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(songs);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre([FromQuery] int artistId, [FromBody] GenreDto genreCreate)
        {
            if (genreCreate == null)
                return BadRequest(ModelState);

            var genres = _genreRepository.GetGenreTrimToUpper(genreCreate);

            if (genres != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(genreCreate);

            if (!_genreRepository.CreateGenre(artistId, genreMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{genreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGenre(int genreId,
            [FromQuery] int artistId,
            [FromBody] GenreDto updatedGenre)
        {
            if (updatedGenre == null)
                return BadRequest(ModelState);

            if (genreId != updatedGenre.Id)
                return BadRequest(ModelState);

            if (!_genreRepository.GenreExists(artistId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var genreMap = _mapper.Map<Genre>(updatedGenre);

            if (!_genreRepository.UpdateGenre(artistId, genreMap))
            {
                ModelState.AddModelError("", "Something went wrong updating genre");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{genreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();

            var genreToDelete = _genreRepository.GetGenre(genreId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_genreRepository.DeleteGenre(genreToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting genre");
            }

            return NoContent();
        }

    }*/
}
