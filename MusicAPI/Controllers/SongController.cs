using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Dto;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public SongController(ISongRepository songRepository,
            IArtistRepository artistRepository,
            IMapper mapper)
        {
            _songRepository = songRepository;
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Song>))]
        [ProducesResponseType(400)]
        public IActionResult GetSongs()
        {
            var songs = _mapper.Map<List<SongDto>>(_songRepository.GetSongs());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(songs);
        }

        [HttpGet("{songId}")]
        [ProducesResponseType(200, Type = typeof(Song))]
        [ProducesResponseType(400)]
        public IActionResult GetSong(int songId)
        {
            if (!_songRepository.SongExists(songId))
                return NotFound();

            var song = _mapper.Map<SongDto>(_songRepository.GetSong(songId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(song);
        }

        [HttpGet("{songId}/artist")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public IActionResult GetArtistBySong(int songId)
        {
            if (!_songRepository.SongExists(songId))
                return NotFound();

            var artist = _mapper.Map<ArtistDto>(_songRepository.GetArtistBySong(songId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(artist);
        }

        [HttpGet("{songId}/genres")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        [ProducesResponseType(400)]
        public IActionResult GetGenresOfSong(int songId)
        {
            if (!_songRepository.SongExists(songId))
                return NotFound();
            var genres = _mapper.Map<List<GenreDto>>(_songRepository.GetGenreOfSong(songId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genres);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSong([FromQuery] int artistId, [FromBody] SongDto songCreate)
        {
            if (songCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var songMap = _mapper.Map<Song>(songCreate);
            songMap.Artist = _artistRepository.GetArtist(artistId);
            if (!_songRepository.CreateSong(songMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{songId}")]
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
        }

        [HttpDelete("{songId}")]
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
        }
    }
}