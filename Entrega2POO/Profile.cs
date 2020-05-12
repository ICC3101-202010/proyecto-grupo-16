using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectodeCurso
{
    public class Profile
    {
        private string UserName;
        //Listas
        List<PlaylistS> PlaylistS = new List<PlaylistS> { };
        List<Songs> FavSong = new List<Songs> { };
        List<PlaylistS> PlaylistFollowingS = new List<PlaylistS> { };
        List<Singer> SingerFollowing = new List<Singer> { };


        //Metodos para agregar a las listas playlist/cantantes
        public void AddFavSong(Songs song)
        {
            FavSong.Add(song);
        }

        public void AddPlaylistS(PlaylistS playlist)
        {
            PlaylistS.Add(playlist);
        }

        public void AddPlaylistFollowingS(PlaylistS playlist)
        {
            PlaylistFollowingS.Add(playlist);
        }

        public void AddSingerFollowing(Singer singer)
        {
            SingerFollowing.Add(singer);
        }




        public Profile(string username)
        {
            this.UserName = username;
        }
        public string UserName1 { get => UserName; set => UserName = value; }
    }
}
