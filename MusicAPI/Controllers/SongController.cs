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
        private readonly IMapper _mapper;

        public SongController(ISongRepository songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
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

    }
}