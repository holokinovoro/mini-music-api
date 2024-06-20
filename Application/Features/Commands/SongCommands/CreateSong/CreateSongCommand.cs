using MediatR;
using Application.Dto;
using Domain.Models;

namespace Application.Features.Commands.SongCommands.CreateSong;

public class CreateSongCommand : IRequest<int>
{
    public int artistId { get; set; }
    public SongDto createSong { get; set; }
}
