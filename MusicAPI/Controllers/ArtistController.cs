using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Dto;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/artists")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistController(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}/artist")]
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
        }

        [HttpGet]
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
        }


    }
}
