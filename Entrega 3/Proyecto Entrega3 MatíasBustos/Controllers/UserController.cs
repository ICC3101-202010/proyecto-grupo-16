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
using System.Drawing.Printing;

namespace ProyectoEntrega3MatíasB_MatíasR.Controllers
{
    class UserController
    {
        //Conoce a la BDD de Users 
        private List<profile> DataBaseUsers = new List<profile>();

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


        //Metodos de verificacion y manejo de acciones
        public void AddProfile(string Email, string pass, string username)
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

       

        public bool CheckSongInFavSongsUsr(string UserName, string SongName, string BandName)
        {
            SongsController songsController = new SongsController();
            songsController.LoadSongs();
            Songs SongToCheck = songsController.ReturnSongBySongNameAndBandName(SongName, BandName);
            foreach (Songs song in ReturnUserByUserName(UserName).FavSong1)
            {
                if (song.Name_Song == SongToCheck.Name_Song && song.NameBand1 == SongToCheck.NameBand1)
                {

                    return true;
                }
            }
            return false;
        }
        public bool CheckMovieInFavMoviesUsr(string UserName, string TitleMovie, string Studio)
        {
            MovieController moviecontroller = new MovieController();
            moviecontroller.LoadMovies();
            Movies MovieToCheck = moviecontroller.ReturnMovieByTitleNameAndStudioName(TitleMovie, Studio);
            foreach (Movies movie in ReturnUserByUserName(UserName).FavMovies1)
            {
                if (movie.Title == MovieToCheck.Title && movie.Studio == MovieToCheck.Studio)
                {

                    return true;
                }
            }
            return false;
        }

        public int RemoveSongFromFavSongListFromUser(string UserName, string SongName, string BandName)
        {
            foreach (Songs song in ReturnUserByUserName(UserName).FavSong1)
            {
                if (song.Name_Song == SongName && song.NameBand1 == BandName)
                {
                    ReturnUserByUserName(UserName).FavSong1.Remove(song);
                    return 0;
                }
            }
            return 1;
        }
        public int RemoveMovieFromFavMovieListFromUser(string UserName, string TitleMovie, string Studio)
        {
            foreach (Movies movie in ReturnUserByUserName(UserName).FavMovies1)
            {
                if (movie.Title == TitleMovie && movie.Studio == Studio)
                {
                    ReturnUserByUserName(UserName).FavMovies1.Remove(movie);
                    return 0;
                }
            }
            return 1;
        }

        
        //En la posición 0 estara el nombre de la cancion , en la posicion 1 el nombre de la banda y en la ultima posicion estara el filepathdelalbum
        public List<List<string>> ReturnFavSongInfoUsr(string Username)
        {
            List<List<string>> InfoFavSongs = new List<List<string>>();
            foreach (Songs song in ReturnUserByUserName(Username).FavSong1)
            {
                InfoFavSongs.Add(new List<string> { song.Name_Song, song.NameBand1, song.AlbumfilePath1 });
            }
            return InfoFavSongs;
        }
        public List<List<string>> ReturnFavMovieInfoUsr(string Username)
        {
            List<List<string>> InfoFavMovies = new List<List<string>>();
            foreach (Movies movie in ReturnUserByUserName(Username).FavMovies1)
            {
                InfoFavMovies.Add(new List<string> { movie.Title, movie.Studio, movie.MovieImgFilePath1 });
            }
            return InfoFavMovies;
        }
        public bool CheckPLaylistSongInUsr(string Username, string NewPlaylistSNAME)
        {
            foreach (PlaylistS playlistS in ReturnUserByUserName(Username).PlaylistS1)
            {
                if (playlistS.Name_PlaylistS1 == NewPlaylistSNAME)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckPLaylistMovieInUsr(string Username, string NewPlaylistMNAME)
        {
            foreach (PlaylistM playlistM in ReturnUserByUserName(Username).PlaylistM1)
            {
                if (playlistM.Name_PlaylistM == NewPlaylistMNAME)
                {
                    return true;
                }
            }
            return false;
        }
        //Se agrega una lista nueva , por lo tanto es una lista vacia de canciones
        public void AddNewPlaylisSToUsr(string Username, string NamePlaylistS, string TypeFileSongPLaylisS)
        {
            List<Songs> EmptySongList = new List<Songs>();
            bool privacyUsr = ReturnUserByUserName(Username).Privacy;
            PlaylistS playlistS = new PlaylistS(Username, NamePlaylistS, privacyUsr, TypeFileSongPLaylisS, EmptySongList);
            ReturnUserByUserName(Username).PlaylistS1.Add(playlistS);
            SaveUsers();
            LoadUsers();
        }
        public void AddNewPlaylisMToUsr(string Username, string NamePlaylistM, string TypeFileSongPLaylisM)
        {
            List<Movies> EmptyMovieList = new List<Movies>();
            bool privacyUsr = ReturnUserByUserName(Username).Privacy;
            PlaylistM playlistM = new PlaylistM(Username, NamePlaylistM, privacyUsr, TypeFileSongPLaylisM, EmptyMovieList);
            ReturnUserByUserName(Username).PlaylistM1.Add(playlistM);
            SaveUsers();
            LoadUsers();
        }
        public List<List<string>> ReturnPlaylistSInfoForUsr(string Username)
        {
            List<List<string>> InfoPlaylistSongs = new List<List<string>>();
            foreach (PlaylistS playlistS in ReturnUserByUserName(Username).PlaylistS1)
            {
                InfoPlaylistSongs.Add(new List<string> { playlistS.Name_PlaylistS1, playlistS.OwnerUser1, playlistS.Type });
            }
            return InfoPlaylistSongs;
        }
        public List<List<string>> ReturnMoviesPlaylistMInfoForUsr(string Username)
        {
            List<List<string>> InfoPlaylistMovies = new List<List<string>>();
            foreach (PlaylistM playlistM in ReturnUserByUserName(Username).PlaylistM1)
            {
                InfoPlaylistMovies.Add(new List<string> { playlistM.Name_PlaylistM, playlistM.OwnerUser, playlistM.TypeM });
            }
            return InfoPlaylistMovies;
        }
        public List<List<string>> ReturnSongsInfoFromClickedPlaylistUSR(string Username, string PlaylistNameS)
        {
            List<List<string>> InfoPlaylistSongs = new List<List<string>>();
            foreach (PlaylistS playlistS in ReturnUserByUserName(Username).PlaylistS1)
            {
                if (playlistS.Name_PlaylistS1 == PlaylistNameS)
                {
                    foreach (Songs song in playlistS.ListS)
                    {
                        InfoPlaylistSongs.Add(new List<string> { song.Name_Song, song.NameBand1, song.AlbumfilePath1 });
                    }
                    return InfoPlaylistSongs;
                }

            }
            return InfoPlaylistSongs;
        }
        public List<List<string>> ReturnMoviesInfoFromClickedPlaylistMUSR(string Username, string PlaylistNameM)
        {
            List<List<string>> InfoPlaylistSongs = new List<List<string>>();
            foreach (PlaylistM playlistM in ReturnUserByUserName(Username).PlaylistM1)
            {
                if (playlistM.Name_PlaylistM == PlaylistNameM)
                {
                    foreach (Movies movie in playlistM.ListM)
                    {
                        InfoPlaylistSongs.Add(new List<string> { movie.Title, movie.Studio, movie.MovieImgFilePath1 });
                    }
                    return InfoPlaylistSongs;
                }

            }
            return InfoPlaylistSongs;
        }

        public int AddSelectedSToPlySForUsr(string SongName,string BandName,string Username,string PlaylistSName)
        {
            SongsController songsController = new SongsController();
            songsController.LoadSongs();
            foreach (PlaylistS playlistS in ReturnUserByUserName(Username).PlaylistS1)
            {
                if(playlistS.Name_PlaylistS1 == PlaylistSName)
                {
                    playlistS.ListS.Add(songsController.ReturnSongBySongNameAndBandName(SongName, BandName));
                    return 1;
                }
            }
            return 0;
        }
        public int AddSelectedMToPlyMForUsr(string MovieTitle, string StudioName, string Username, string PlaylistMName)
        {
            MovieController movieController = new MovieController();
            movieController.LoadMovies();
            foreach (PlaylistM playlistM in ReturnUserByUserName(Username).PlaylistM1)
            {
                if (playlistM.Name_PlaylistM == PlaylistMName)
                {
                    playlistM.ListM.Add(movieController.ReturnMovieByTitleNameAndStudioName(MovieTitle, StudioName));
                    return 1;
                }
            }
            return 0;
        }
        public bool CheckSongInPlySInUsr(string PlaylistSName,string SongName,string BandaName,string Username)
        {
            foreach (PlaylistS playlistS in ReturnUserByUserName(Username).PlaylistS1)
            {
                if (playlistS.Name_PlaylistS1 == PlaylistSName)
                {
                    foreach(Songs song in playlistS.ListS)
                    {
                        if(song.Name_Song == SongName && song.NameBand1 == BandaName)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool CheckMovieInPlyMInUsr(string PlaylistMName, string MovieTitle, string StudioName, string Username)
        {
            foreach (PlaylistM playlistM in ReturnUserByUserName(Username).PlaylistM1)
            {
                if (playlistM.Name_PlaylistM == PlaylistMName)
                {
                    foreach (Movies movie in playlistM.ListM)
                    {
                        if (movie.Title == MovieTitle && movie.Studio == StudioName)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
       
     



    }
}
