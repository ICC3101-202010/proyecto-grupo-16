using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models__Proyecto_2_
{
    [Serializable]
    public class profile
    {
        public string Email;
        public string UserName;
        public string Password;
        public bool Privacy;
        private string profilePicPath;


        private List<PlaylistS> PlaylistS = new List<PlaylistS> { };
        private List<Songs> FavSong = new List<Songs> { };
        private List<PlaylistM> PlaylistM = new List<PlaylistM> { }; 
        private List<Movies> FavMovies = new List<Movies> { };

        //El contructor recibe tambn listas
        public profile(string Email, string Password, string UserName, bool Privacy, List<PlaylistS> PlaylistS, List<Songs> FavSong, List<PlaylistM> PlaylistM, List<Movies> FavMovies)
        {
            this.Email1 = Email;
            this.Password1 = Password;
            this.UserName1 = UserName;

            this.Privacy1 = Privacy;

            this.PlaylistM1 = PlaylistM;
            this.PlaylistS1 = PlaylistS;
            this.FavMovies1 = FavMovies;
            this.FavSong1 = FavSong;

            
        }

       

        public string Email1 { get => Email; set => Email = value; }
        public string UserName1 { get => UserName; set => UserName = value; }
        public string Password1 { get => Password; set => Password = value; }

        public bool Privacy1 { get => Privacy; set => Privacy = value; }

        public List<PlaylistS> PlaylistS1 { get => PlaylistS; set => PlaylistS = value; }
        public List<Songs> FavSong1 { get => FavSong; set => FavSong = value; }
        public List<PlaylistM> PlaylistM1 { get => PlaylistM; set => PlaylistM = value; }
        public List<Movies> FavMovies1 { get => FavMovies; set => FavMovies = value; }
        public string ProfilePicPath { get => profilePicPath; set => profilePicPath = value; }
    }
}
