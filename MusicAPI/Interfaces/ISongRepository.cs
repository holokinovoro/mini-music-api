﻿using Microsoft.Identity.Client;
using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface ISongRepository 
    {
        ICollection<Song> GetSongs();

        Song GetSong(int songId);

        Artist GetArtistBySong(int songId);

        ICollection<Genre> GetGenreOfSong(int songId);

        bool SongExists(int songId);

        bool CreateSong(Song song);

        bool UpdateSong(Song song);

        bool DeleteSong(Song song);

        bool Save();
    }
}
