using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_APP_SpotiNetflix
{
    [Serializable]
    class Profile
    {
        private string Email;
        private string UserName;
        private string Password;
        private string BornDate; 
        private bool Privacy;
        private bool Premium;


        //Listas
        List<PlaylistS> PlaylistS = new List<PlaylistS> { };
        List<Songs> FavSong = new List<Songs> { };
        List<PlaylistS> PlaylistFollowingS = new List<PlaylistS> { };
        List<Singer> SingerFollowing = new List<Singer> { };

        

        List<WorkerMovie> WorkerMovieFollowing = new List<WorkerMovie>();

        protected List<Movies> KeyWordMovies = new List<Movies> { };
        protected List<Songs> KeyWordSongs = new List<Songs> { };

        List<PlaylistM> PlaylistM = new List<PlaylistM> { }; //lista de playlist del usuario
        List<Movies> FavMovies = new List<Movies> { };//lista de peliculas favoritas del usuario
        List<PlaylistM> PlaylistFollowingM = new List<PlaylistM> { };//lista de playlist de peliculas q sigue el usuario
        List<WorkerMovie> WorkermovieFollowing = new List<WorkerMovie> { }; //lista de trabajadores de peliculas q sigue el usuario



        public Profile(string Email,string Password, string UserName,string BornDate,bool Privacy,bool Premium)
        {
            this.Email1 = Email;
            this.Password1 = Password;
            this.UserName1 = UserName;
            this.BornDate1 = BornDate;
            this.Privacy1 = Privacy;
            this.Premium1 = Premium;
        }

        public string Email1 { get => Email; set => Email = value; }
        public string UserName1 { get => UserName; set => UserName = value; }
        public string Password1 { get => Password; set => Password = value; }
        public string BornDate1 { get => BornDate; set => BornDate = value; }
        public bool Privacy1 { get => Privacy; set => Privacy = value; }
        public bool Premium1 { get => Premium; set => Premium = value; }


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


        public void AddFavMovie(Movies movie)
        {
            FavMovies.Add(movie);
        }

        public void AddPlaylistM(PlaylistM playlist)
        {
            PlaylistM.Add(playlist);
        }

        public void AddPlaylistFollowingM(PlaylistM playlist)
        {
            PlaylistFollowingM.Add(playlist);
        }

        public void AddSingerFollowing(WorkerMovie worker)
        {
            WorkerMovieFollowing.Add(worker);
        }


    }
}
