using System;

using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Management.Instrumentation;
using System.Security.AccessControl;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Models__Proyecto_2_;

namespace ProyectoEntrega3MatíasB_MatíasR.Controllers
{
    class UserController
    {
        //Conoce a la BDD de Users 
        private List<profile> DataBaseUsers = new List<profile>();
        private PlaylistSController PlaylistSController = new PlaylistSController();
        public UserController()
        {
            //Se crean las playlist vacias 
            List<PlaylistM> playlistMUser = new List<PlaylistM>();
            List<PlaylistS> playlistSUser = new List<PlaylistS>();
            List<Songs> FavSongs = new List<Songs>();
            List<Movies> FavMovies = new List<Movies>();

            profile Admin = new profile("Admin@gmai.com", "123", "ADMIN", true, playlistSUser, FavSongs, playlistMUser, FavMovies);
            DataBaseUsers.Add(Admin);
        }

        //Obtiene un prefil y le agrega una cancion a su lista de favs
        public void AddFavSong(profile profile,Songs song)
        {
            profile.FavSong1.Add(song);
        }

        public void AddPlaylistS(profile profile, PlaylistS playlist)
        {
            profile.PlaylistS1.Add(playlist);
        }

        public void AddFavMovie(profile profile, Movies movie)
        {
            profile.FavMovies1.Add(movie);
        }

        public void AddPlaylistM(profile profile, PlaylistM playlist)
        {
            profile.PlaylistM1.Add(playlist);
        }


        //Metodos de verificacion y manejo de acciones
        public void AddProfile(string Email,string pass,string username)
        {
            //Listas vacias para inicializar el usuario
            List<Songs> FavS = new List<Songs>();
            List<PlaylistS> playlistS = new List<PlaylistS>();
            List<Movies> FavMov = new List<Movies>();
            List<PlaylistM> playlistM = new List<PlaylistM>();

            profile profile = new profile(Email, pass, username, true, playlistS, FavS, playlistM, FavMov);
            DataBaseUsers.Add(profile);
            
        }

        public int VerifyAccountCreation(string email, string pass, string repass, string user)
        {
            if (pass != repass)
            {
                return 2;
            }
            foreach (profile profile in DataBaseUsers)
            {
                if (profile.Email1 == email)
                {
                    return 1;
                }
                else if (profile.UserName1 == user)
                {
                    return 3;
                }

            }
            //Si es que no hay datos ya existentes en la base de datos, se devuelve 0
            return 0;

        }

        public int VerifyLogin(string UserName, string Password)
        {
            foreach (profile profile in DataBaseUsers)
            {
                if (profile.UserName == UserName && profile.Password == Password)
                {
                    return 1;
                }


            }
            //Error de logeo (para contraseña email no valido, o contraseña no valida c/r a un email correcto)
            return 0;
        }
        public int VerifyAdminUser(string UserName)
        {
            if (UserName == "ADMIN")
            {
                return 1;
            }
            return 0;
        }

        public void SaveUsers()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DataBaseUsers.bin", FileMode.Open, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DataBaseUsers);
            stream.Close();

        }
        public List<profile> LoadUsers()
        {
            Stream stream = new FileStream("DataBaseUsers.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios
            if (stream.Length == 0)
            {
                stream.Close();
                return DataBaseUsers;
            }
            //En caso de que el archivo no este vacio , obtenemos sus datos almacenados
            IFormatter formatter = new BinaryFormatter();
            List<profile> DataBaseStoredUsers = (List<profile>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBaseUsers = DataBaseStoredUsers;
            stream.Close();
            return DataBaseUsers;
        }

        public void ShowUsers()
        {
            foreach (profile profile in DataBaseUsers)
            {
                Console.WriteLine("Email: " + profile.Email1+" Username: " + profile.UserName + "Password: " + profile.Password +" Privacy:"+profile.Privacy +"\n");
            }
        }
        //Retorna el perfil con el email 
        public profile ReturnUser(string Email, string Password)
        {
            foreach (profile profile in DataBaseUsers)
            {
                if (profile.Email1 == Email && profile.Password1 == Password)
                {
                    return profile;
                }


            }
            List<PlaylistM> playlistMUser = new List<PlaylistM>();
            List<PlaylistS> playlistSUser = new List<PlaylistS>();
            List<Songs> FavSongs = new List<Songs>();
            List<Movies> FavMovies = new List<Movies>();

            profile NullProfile = new profile("", "", "", false, playlistSUser, FavSongs, playlistMUser, FavMovies);
            return NullProfile;
            //Error de logeo (para contraseña email no valido, o contraseña no valida c/r a un email correcto)

        }
        public profile ReturnUserByUserName(string UserName)
        {
            foreach (profile profile in DataBaseUsers)
            {
                if (profile.UserName == UserName)
                {
                    return profile;
                }


            }
            List<PlaylistM> playlistMUser = new List<PlaylistM>();
            List<PlaylistS> playlistSUser = new List<PlaylistS>();
            List<Songs> FavSongs = new List<Songs>();
            List<Movies> FavMovies = new List<Movies>();

            profile NullProfile = new profile("", "", "", false, playlistSUser, FavSongs, playlistMUser, FavMovies);
            return NullProfile;
            //Error de logeo (para contraseña email no valido, o contraseña no valida c/r a un email correcto)
        }
        
        public void ChangeUserName(string OldUsername,string NewUsername,string password)
        {
            foreach (profile profile in DataBaseUsers)
            {
                if (profile.UserName == OldUsername && profile.Password == password)
                {
                    profile.UserName = NewUsername;
         
                }


            }

        }
        public void ChangePrivacy(string username)
        {
            foreach (profile profile in DataBaseUsers)
            {
                if (profile.UserName == username)
                {
                    profile.Privacy = !profile.Privacy;


                }


            }
        }

        public void AddMoviesP(Movies pelicula,profile profile,PlaylistM playlistM)  //añade peliculas a la playlist de peliculas
        {
            if (pelicula.TypeFileM == "mp4")
            {
                foreach (PlaylistM playlistM1 in profile.PlaylistM1)
                {
                    if (playlistM == playlistM1)
                    {
                        playlistM1.ListM.Add(pelicula);
                    }
                }
            }
   

        }

        public bool VerifyPlaylistSInProfile(profile OwnerUser, string name_playlist)
        {
            foreach(PlaylistS playlistS in OwnerUser.PlaylistS1)
            {
                if(playlistS.Name_PlaylistS1 == name_playlist)
                {
                    return true;
                }
            }
            return false;
        }
        public bool VerifyPlaylistMInProfile(profile OwnerUser, string name_playlist)
        {
            foreach(PlaylistM playlistM in OwnerUser.PlaylistM1)
            {
                if (playlistM.Name_PlaylistM == name_playlist)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddNewPlaylistSToUser(profile User,string name_playlistS,string TypeFile)
        {
            //Empty ListSongs (dado que es una nueva playlist)
            List<Songs> ListS = new List<Songs>();
            PlaylistS playlistS = new PlaylistS(User.UserName, name_playlistS, User.Privacy, TypeFile, ListS);
            User.PlaylistS1.Add(playlistS);
                
            //No se agrega todavia la playlistS dado que no contiene canciones
        }
        public void AddNewPlaylistMToUser(profile User, string name_playlistM, string TypeFile)
        {
            //Empty ListSongs (dado que es una nueva playlist)
            List<Movies> ListM = new List<Movies>();
            PlaylistM playlistM = new PlaylistM(User.UserName, name_playlistM, User.Privacy, TypeFile, ListM);
            User.PlaylistM1.Add(playlistM);

            //No se agrega todavia la playlistS dado que no contiene canciones
        }

        public List<string> ShowPlaylistSNamesFromUser(profile profile)
        {
            List<string> StringList = new List<string>();
            foreach(PlaylistS playlistS in profile.PlaylistS1)
            {
                StringList.Add("Name: "+playlistS.Name_PlaylistS1+", TypeFile: "+playlistS.Type);
            }
            return StringList;
        }
        public List<string> ShowPlaylistMNamesFromUser(profile profile)
        {
            List<string> StringList = new List<string>();
            foreach (PlaylistM playlistM in profile.PlaylistM1)
            {
                StringList.Add("Name: " + playlistM.Name_PlaylistM + ", TypeFile: " + playlistM.TypeM);
            }
            return StringList;
        }



        /*
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
                Console.WriteLine("Pelicula eliminada");
            }
            Console.WriteLine("La pelicula no ha podido ser eliminada");

        }
        */



    }
}
