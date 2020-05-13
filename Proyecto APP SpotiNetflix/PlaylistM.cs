using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_APP_SpotiNetflix
{
    class PlaylistM
    {
        private string OwnerUser;
        public string Name_PlaylistM;
        public bool PrivacyM; //true == privada / false == publica
        private string TypeM;

        public List<Profile> FollowersPM = new List<Profile> { }; //Lista seguidore de playlist
        public List<Movies> ListM = new List<Movies> { }; //Lista de peliculas de playlist

        public PlaylistM(string owneruser, string name_playlistm, bool privacym, string typem)
        {
            this.OwnerUser = owneruser;
            this.Name_PlaylistM = name_playlistm;
            this.PrivacyM = privacym;
            this.TypeM = typem;
        }
        public string PlaylistMinfo()
        {
            return "Creador: " + OwnerUser + ", NombrePlaylist: " + Name_PlaylistM + ", tipo: " + TypeM;
        }
        public void AddMoviesP(Movies pelicula)  //añade peliculas a la playlist de peliculas
        {
            if (TypeM == pelicula.TypeFileM1)
            {
                ListM.Add(pelicula);
            }
            else
            {
                Console.WriteLine("Error, las peliculas deben ser del mismo tipo");
            }


        }
        public void AddfollowerM(Profile seguidor)  // añade seguidores a la lista de seguidores de la playlist
        {
            FollowersPM.Add(seguidor);
        }
        public string MovieSearchingPlaylist(Movies pelicula)
        {
            if (ListM.Contains(pelicula))
            {
                return pelicula.InfoMovie();
            }
            else
            {
                return "Pelicula no encontrada dentro de la playlist ";
            }

        }
        public void EliminateMovies(Movies movie)
        {
            if (ListM.Contains(movie))
            {
                ListM.Remove(movie);
            }
            Console.WriteLine("Pelicula eliminada");
        }
    }
}
