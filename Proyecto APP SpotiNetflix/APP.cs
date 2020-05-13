using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Proyecto_APP_SpotiNetflix
{
    class APP
    {

        private List<Songs> DataBaseSongs = new List<Songs>();
        private List<Movies> DataBaseMovies = new List<Movies>();
        private List<Profile> DataBaseUsers = new List<Profile>();

        private List<Singer> DataBaseSingers = new List<Singer>();
        private List<WorkerMovie> DataBaseWorkersM = new List<WorkerMovie>();

        private List<List<PlaylistM>> DataBasePlaylistM = new List<List<PlaylistM>>();


        private List<List<PlaylistS>> DataBasePlaylistS = new List<List<PlaylistS>>();



        protected List<Movies> KeyWordMovies = new List<Movies> { };
        protected List<Songs> KeyWordSongs = new List<Songs> { };

        protected List<Songs> KeyWordRankingigualsongs = new List<Songs> { };//lista para en las cuales son igual la key y el ranking de songs
        protected List<Songs> KeyWordRankingmayorsongs = new List<Songs> { };
        protected List<Songs> KeyWordRankingmenorsongs = new List<Songs> { };
        protected List<Movies> KeyWordRankingigualmovies = new List<Movies> { };
        protected List<Movies> KeyWordRankingmenormovies = new List<Movies> { };
        protected List<Movies> KeyWordRankingmayormovies = new List<Movies> { };
        protected List<Movies> KeyWordCategoria = new List<Movies> { };




        public APP()
        {

        }

        public void AddProfile(Profile profile)
        {
            DataBaseUsers.Add(profile);
            
            
        }
        public int VerifyAccountCreation(string email, string pass, string repass , string user)
        {
            if(pass != repass)
            {
                return 2;
            }
            foreach(Profile profile in DataBaseUsers)
            {
                if(profile.Email1== email)
                {
                    return 1;
                }
                else if(profile.UserName1 == user)
                {
                    return 3;
                }
               
            }
            //Si es que no hay datos ya existentes en la base de datos, se devuelve 0
            return 0;

        }
        
        
        public int VerifyLogin(string Email, string Password)
        {
            foreach(Profile profile in DataBaseUsers)
            {
                if(profile.Email1==Email && profile.Password1 == Password)
                {
                    return 1;
                }

               
            }
            //Error de logeo (para contraseña email no valido, o contraseña no valida c/r a un email correcto)
            return 0;
        }
        public int VerifyAdminUser(string Email)
        {
            if(Email == "AdminSpotiflix")
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
        public List<Profile> LoadUsers()
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
            List<Profile> DataBaseStoredUsers = (List<Profile>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBaseUsers = DataBaseStoredUsers;
            stream.Close();
            return DataBaseUsers;
        }

        public void ShowUsers()
        {
            foreach (Profile profile in DataBaseUsers)
            {
                Console.WriteLine("Email: "+profile.Email1+"Password: "+profile.Password1+"\n");
            }
        }

        public float GetFileSize(string FileName)
        {

            FileStream fs = new FileStream(FileName,FileMode.Open,FileAccess.Read);

            long Byte_size = fs.Length;

            float Mb_size = Byte_size/1048576;
            fs.Close();

            return Mb_size;
        }
        public bool VerifyFilePath(string FileName)
        {

            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios
          
            bool Exist = File.Exists(FileName);

            return Exist;

        }
        public string GetFileType(string FileName)
        {
            //Solo funciona con los tipos de archivos que el tipo de archivo tiene solo 3 char m p 3, m p 4, etc.
            string TypeFile = FileName.Substring(FileName.Length - 3);
            return TypeFile;
        }





        //SONGS
        //Metodos para las acciones de los usuarios
        public void AddSong(Songs song)
        {
            DataBaseSongs.Add(song);
        }
        public void SaveSongs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DataBaseSongs.bin", FileMode.Open, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DataBaseSongs);
            stream.Close();
        }
        public List<Songs> LoadSongs()
        {
            Stream stream = new FileStream("DataBaseSongs.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios
            if (stream.Length == 0)
            {
                stream.Close();
                return DataBaseSongs;
            }
            //En caso de que el archivo no este vacio , obtenemos sus datos almacenados
            IFormatter formatter = new BinaryFormatter();
            List<Songs> DataBaseStoredSongs = (List<Songs>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBaseSongs = DataBaseStoredSongs;
            stream.Close();
            return DataBaseSongs;
        }
        public int VerifySong(string name_song,string Singer ,string Album, string Composer)
        {
            foreach(Songs song in DataBaseSongs)
            {
               if(song.Name_Song1 == name_song && song.Album1 == Album && song.Singer1 == Singer && song.Composer1 == Composer)
                {
                    return 1;
                }
            }
            return 0;
        }

        //Singers Save and load (to list and .dat archive)
        public void AddSinger(Singer singer)
        {
            DataBaseSingers.Add(singer);
        }

        public void SaveSinger()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DataBaseSingers.bin", FileMode.Open, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DataBaseSingers);
            stream.Close();
        }

        public List<Singer> LoadSinger()
        {
            Stream stream = new FileStream("DataBaseSingers.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios
            if (stream.Length == 0)
            {
                stream.Close();
                return DataBaseSingers;
            }
            //En caso de que el archivo no este vacio , obtenemos sus datos almacenados
            IFormatter formatter = new BinaryFormatter();
            List<Singer> DataBaseStoredSingers = (List<Singer>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBaseSingers = DataBaseStoredSingers;
            stream.Close();
            return DataBaseSingers;
        }




        //MOVIES
        //Metodos Peliculas de grabado de peliculas en "DataBaseMovies.bin"
        public void AddMovie(Movies movie)
        {
            DataBaseMovies.Add(movie);
        }
        public void AddWokerM(List<WorkerMovie> ListWorkerMovie)
        {
            foreach(WorkerMovie worker in ListWorkerMovie)
            {
                DataBaseWorkersM.Add(worker);
            }
        }
        public void SaveWorkersM()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DataBaseWorkersM.bin", FileMode.Open, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DataBaseWorkersM);
            stream.Close();
        }
        public List<WorkerMovie> LoadWorkersM()
        {
            Stream stream = new FileStream("DataBaseWorkersM.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios
            if (stream.Length == 0)
            {
                stream.Close();
                return DataBaseWorkersM;
            }
            //En caso de que el archivo no este vacio , obtenemos sus datos almacenados
            IFormatter formatter = new BinaryFormatter();
            List<WorkerMovie> DataBaseStoredWorkersM = (List<WorkerMovie>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBaseWorkersM = DataBaseStoredWorkersM;
            stream.Close();
            return DataBaseWorkersM;
        }
        public void SaveMovies()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DataBaseMovies.bin", FileMode.Open, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DataBaseMovies);
            stream.Close();
        }
        public List<Movies> LoadMovies()
        {
            Stream stream = new FileStream("DataBaseMovies.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios
            if (stream.Length == 0)
            {
                stream.Close();
                return DataBaseMovies;
            }
            //En caso de que el archivo no este vacio , obtenemos sus datos almacenados
            IFormatter formatter = new BinaryFormatter();
            List<Movies> DataBaseStoredMovies = (List<Movies>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBaseMovies = DataBaseStoredMovies;
            stream.Close();
            return DataBaseMovies;
        }
        public int MovieCheker(string Title, string YearPublish )
        {
            foreach(Movies movie in DataBaseMovies)
            {
                if(movie.Title1 == Title && YearPublish == movie.YearPublish1)
                {
                    return 1;
                }
            }
            return 0;
        }







        //METODOS PLAYLIST SONGS

        //Buscar cancion
        public string SearchKeyWord(string Key, string type)
        {
            if (type == "Música")
            {
                foreach (Songs song in DataBaseSongs)
                {
                    if (Key == song.Name_Song1 || Key == song.Album1 || Key == song.SongGenre1 || Key == song.Singer1 || Key == song.Composer1 || Key == song.YearPublishS1
                    || Key == song.TypeFileS1)
                    {
                        KeyWordSongs.Add(song); //Agrega las canciones que cumplan la coincidencia con el keyword
                    }
                }
                if (KeyWordSongs.Count == 0) //Si la lista no contiene canciones ningun tuvo coincidencia con el keyword
                {
                    return "No se encontraron coincidencias";
                }
                foreach (Songs song1 in KeyWordSongs)
                {
                    return song1.InfoSong(); //Retorna la info de las canciones de la lista (las que coinciden)
                }
                KeyWordSongs.Clear(); //Se resetea la lista para una nueva busqueda
            }
            if (type == "Películas") //Lo mismo pero con los métodos de las películas
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    if (Key == movie.Title1 || Key == movie.Category1 || Key == movie.Description1 || Key == movie.Studio1 || Key == movie.YearPublish1 || Key == movie.TypeFileM1)
                    {
                        KeyWordMovies.Add(movie);
                    }
                }
                if (KeyWordMovies.Count == 0) //Si la lista no contiene canciones ningun tuvo coincidencia con el keyword
                {
                    return "No se encontraron coincidencias";
                }
                for (int i = 0; i < KeyWordMovies.Count; ++i)
                {
                    return KeyWordMovies[i].InfoMovie();
                }
                KeyWordMovies.Clear();

            }
            return "";
        }
        public void AddMovies(Movies movie) //añadir a database playlist peliculas
        {
            DataBaseMovies.Add(movie);
        }
        public int EliminatePlaylistM(PlaylistM playlist) // para eliminar playlsit de peliculas
        {

            foreach(List<PlaylistM> playlistM in DataBasePlaylistM)
            {
                if (playlistM.Contains(playlist))
                {
                    playlistM.Remove(playlist);
                    return 1;
                    
                }
            }
            return 0;
        }
        public void SearchEvaligualsong(float Key)// guardar el lista las canciones que sean igual al valor del ranking key
        {
            foreach (Songs song in DataBaseSongs)
            {
                if (Key == song.RankingS1)
                {
                    KeyWordRankingigualsongs.Add(song);
                }
            }
            if (KeyWordRankingigualsongs.Count == 0)
            {
                Console.WriteLine("No hay ningun valor encontrado para su busqueda");
            }
        }
        public void SearchEvalmayorsong(float Key) //guardar en lista las canciones que mayor al vlaor del ranking key
        {
            foreach (Songs song in DataBaseSongs)
            {
                if (Key < song.RankingS1)
                {
                    KeyWordRankingmayorsongs.Add(song);
                }
            }
            if (KeyWordRankingmayorsongs.Count == 0)
            {
                Console.WriteLine("No hay ningun valor encontrado para su busqueda");
            }
        }
        public void SearchEvalmenorsong(float Key)//guardas en lista las cancoines que sean menor al valor del ranking key
        {
            foreach (Songs song in DataBaseSongs)
            {
                if (Key > song.RankingS1)
                {
                    KeyWordRankingmenorsongs.Add(song);
                }
            }
            if (KeyWordRankingmenorsongs.Count == 0)
            {
                Console.WriteLine("No hay ningun valor encontrado para su busqueda");
            }
        }

        public void SearchEvaligualmovies(float Key) //guardar en lista las peliculas que sean igual al valor del ranking key
        {
            foreach (Movies movie in DataBaseMovies)
            {
                if (Key == movie.RankingM1)
                {
                    KeyWordRankingigualmovies.Add(movie);
                }
            }
            if (KeyWordRankingigualmovies.Count == 0)
            {
                Console.WriteLine("No hay ningun valor encontrado para su busqueda");
            }
        }
        public void SearchEvalmayormovies(float Key) //guardar en lista las peliculas que sean mayores al valor del ranking key
        {
            foreach (Movies movie in DataBaseMovies)
            {
                if (Key < movie.RankingM1)
                {
                    KeyWordRankingmayormovies.Add(movie);
                }
            }
            if (KeyWordRankingmayormovies.Count == 0)
            {
                Console.WriteLine("No hay ningun valor encontrado para su busqueda");
            }
        }
        public void SearchEvalmenormovies(float Key)//guardar en lista las peliculas que sean menor al valor del ranking key
        {
            foreach (Movies movie in DataBaseMovies)
            {
                if (Key > movie.RankingM1)
                {
                    KeyWordRankingmenormovies.Add(movie);
                }
            }
            if (KeyWordRankingmenormovies.Count == 0)
            {
                Console.WriteLine("No hay ningun valor encontrado para su busqueda");
            }
        }
        public void SearchCategoryMovies(string categoria) //para buscar por categoria, si estan las añade a la lista keywordcategoria
        {
            foreach (Movies movie in DataBaseMovies)
            {
                if (categoria == movie.Category1)
                {
                    KeyWordCategoria.Add(movie);

                }
            }
            if (KeyWordCategoria.Count == 0)
            {
                Console.WriteLine("No existe esa categoria");
            }

        }
        public string SearchPlaylistM(string Name) //Arreglar Metodo
        {
            foreach(List<PlaylistM> ListPlaylistM in DataBasePlaylistM)
            {
                foreach(PlaylistM playlistM in ListPlaylistM)
                {
                    if (playlistM.Name_PlaylistM == Name)
                    {
                        if (playlistM.PrivacyM == true) //Privada no se puede ver
                        {
                            return "Lo lamentamos, esta Playlist es privada";
                        }
                        if (playlistM.PrivacyM == false) //Publica se puede ber
                        {
                            return playlistM.PlaylistMinfo();
                        }
                    }
                    if (playlistM.Name_PlaylistM != Name)
                    {
                        return "no existe este nombre";
                    }
                }
            }
            return "";
        }
        public int PlaylistsInM()
        {
            foreach (List<PlaylistM> ListPlaylistM in DataBasePlaylistM)
            {
                foreach (PlaylistM playlistM in ListPlaylistM)
                {
                    Console.WriteLine(playlistM.PlaylistMinfo());
                }
            }
            return DataBasePlaylistM.Count;
        }

        public Profile ReturnUser(string Email,string Password)
        {
            foreach (Profile profile in DataBaseUsers)
            {
                if (profile.Email1 == Email && profile.Password1 == Password)
                {
                    return profile;
                }


            }
            Profile NullProfile = new Profile("", "", "", "", false, false);
            return NullProfile;
            //Error de logeo (para contraseña email no valido, o contraseña no valida c/r a un email correcto)
            
        }
        public void AddPlaylistMToDatabase(List<PlaylistM> playlistM)
        {
            DataBasePlaylistM.Add(playlistM);
        }
        public void AddPlaylistSToDatabase(List<PlaylistS> playlistS)
        {
            DataBasePlaylistS.Add(playlistS);
        }
       

    }


}


