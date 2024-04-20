using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Dto;
using MusicAPI.Features.Commands.CreateSong;
using MusicAPI.Features.Queries.GetSong;
using MusicAPI.Interfaces;
using MusicAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SongController(ISongRepository songRepository,
            IArtistRepository artistRepository,
            IMapper mapper,
            IMediator mediator)
        {
            _songRepository = songRepository;
            _artistRepository = artistRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
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
        public async Task<IActionResult> CreateSong(CreateSongCommand command)
        {
            var response = await _mediator.Send(command);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
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

      /*  [HttpDelete("{songId}")]
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
        }*/
    }
}