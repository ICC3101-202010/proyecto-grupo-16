using Models__Proyecto_2_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ProyectoEntrega3MatíasB_MatíasR.Controllers
{
    class SongsController
    {
        private List<Songs> DataBaseSongs = new List<Songs>();
        private List<Songs> TemporaryListSongs = new List<Songs>();

        //Va agregando canciones a una lista temporal dependiendo del filtro de busqueda c/r
        //Una vez que se ejecuta otra vez la busqueda se resetea esta lista y se vuelve a agregar elementos c/r a otro parametro de busqueda
        private List<Songs> TemporarySearchSongList = new List<Songs>();


        private List<WorkerSong> TrabajadoresCancion = new List<WorkerSong>();

        private List<List<string>> SearchStringListInfo = new List<List<string>>();

        public SongsController()
        {
            
            
        }

        //Agrega un nuevo trabajador a una lista temporal
        public void AddNEWWorkerSToTemporaryList(string name,string rol,string gender,int age)
        {
            WorkerSong workerSong = new WorkerSong(name, gender, age, rol, 0);
            TrabajadoresCancion.Add(workerSong);
        }
        //Agrega un viejo trabajador a una lista temporal
        public void AddOLDWorkerSToTemporaryList(string name, string rol, string gender, int age)
        {

            TrabajadoresCancion.Add(ReturnOLDWorkerSFromDB( name,  rol,  gender,  age));
        }
        //Devuelve un trabajador ya existente en la BDD
        public WorkerSong ReturnOLDWorkerSFromDB(string name, string rol, string gender, int age)
        {
            foreach(Songs songs in DataBaseSongs)
            {
                foreach(WorkerSong workerSong in songs.WorkersSong1)
                {
                    if(workerSong.Name == name && workerSong.Rol == rol && workerSong.Gender1 == gender && workerSong.Age == age)
                    {
                        return workerSong;

                    }
                }
            }
            WorkerSong EmptyWorkerS = new WorkerSong();
            return EmptyWorkerS;
            
        }
        public void ClearWorkerSList()
        {
            TrabajadoresCancion.Clear();
        }
        public List<WorkerSong> RetunWorkerSFromSong()
        {
            return TrabajadoresCancion;
        }


        //SONGS
        //Metodos para las acciones de los usuarios
        
       

        //Checkea si la canción se encuentra en la BDD de canciones
        public bool SongChecker(string name_song,string album,string lyrics,string songGenre,string DatePublish,string NameBand)
        {
            foreach(Songs song in DataBaseSongs)
            {
                if(name_song == song.Name_Song && album == song.Album && lyrics == song.Lyrics && songGenre == song.SongGenre && song.DatePublish == DatePublish && song.NameBand1 == NameBand)
                {
                    return true;
                }
               
            }
            return false;
        }
        public void AddNewSongToDB(string name_song,string Album, string GenreSong, TimeSpan durationS, string DatePublish, string Lyrics,  string TypefileS, double SongSize, List<WorkerSong> workerSongs, string FilePathS,string NameBand)
        {
            //Se establece como 0 los likes y reproducciones dado que es una nueva peli
            Songs NewSong = new Songs(name_song, Album, GenreSong, 0, Lyrics, DatePublish, workerSongs, FilePathS, TypefileS, SongSize, durationS, 0, 0, 0, NameBand);
            DataBaseSongs.Add(NewSong);
        }
        public void AddNewSongToDBWithImagAlbum(string name_song, string Album, string GenreSong, TimeSpan durationS, string DatePublish, string Lyrics, string TypefileS, double SongSize, List<WorkerSong> workerSongs, string FilePathS, string NameBand,string AlbumFilePath)
        {
            //Se establece como 0 los likes y reproducciones dado que es una nueva peli
            Songs NewSong = new Songs(name_song, Album, GenreSong, 0, Lyrics, DatePublish, workerSongs, FilePathS, TypefileS, SongSize, durationS, 0, 0, 0, NameBand);
            NewSong.AlbumfilePath1 = AlbumFilePath;
            DataBaseSongs.Add(NewSong);
        }

        public void SaveSongs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DataBaseSongs.bin", FileMode.Open, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DataBaseSongs);
            stream.Close();
        }
        public long LoadSongs()
        {
            
            Stream stream = new FileStream("DataBaseSongs.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios

            long sizestream = stream.Length;
            if (sizestream == 0)
            {
                stream.Close();
                return sizestream;
                
            }
            //En caso de que el archivo no este vacio , obtenemos sus datos almacenados
            IFormatter formatter = new BinaryFormatter();
            List<Songs> DataBaseStoredSongs = (List<Songs>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBaseSongs = DataBaseStoredSongs;

            
            stream.Close();
            return sizestream;
        }
        
        //Busca las canciones con respecto al nombre de la cancion
  
        public void AddtoSearchSongByNameList(string SongName)
        {
        
            foreach (Songs song in DataBaseSongs)
            {
                //Si es que el string Songname esta en la subcadena del objeto devuelve true
                //song.Name_Song.ToLower().Contains(SongName.ToLower())
                if (song.Name_Song.ToLower().Contains(SongName.ToLower()))
                {
                    TemporarySearchSongList.Add(song);
           
                }
            }

        }

       


        //Ls es una lista de string con 2 valores (hay que editarlo despues para que acepte el filepath del album si es que se alcanza)
        public void AddToSearchStringListOfList()
        {
            foreach(Songs song in TemporarySearchSongList)
            {  

                
                SearchStringListInfo.Add(new List<string>{ song.Name_Song, song.NameBand1 });
                
            }
        }
        //Limpieza de las listas de busquedas
        public void ClearSearchLists()
        {
            SearchStringListInfo.Clear();
            TemporarySearchSongList.Clear();
        }
        public List<List<string>> ReturnSearchListOfListstringInfo()
        {
            return SearchStringListInfo;
        }
        public void AddtoSearchSongByCategory(string Category)
        {
            foreach (Songs song in DataBaseSongs)
            {
                //Si es que el string Songname esta en la subcadena del objeto devuelve true
                //song.Name_Song.ToLower().Contains(SongName.ToLower())
                if (song.SongGenre.ToLower().Contains(Category.ToLower()))
                {
                    TemporarySearchSongList.Add(song);
                }
            }

        }
        public void AddtoSeachSongByBandName(string BandName)
        {
            foreach (Songs song in DataBaseSongs)
            {
                //Si es que el string Songname esta en la subcadena del objeto devuelve true
                //song.Name_Song.ToLower().Contains(SongName.ToLower())
                if (song.NameBand1.ToLower().Contains(BandName.ToLower()))
                {
                    TemporarySearchSongList.Add(song);
                }
            }
        }
        
        public void AddtoSearchSongByNameArtistOrComposer(string name)
        {
            Songs LastSongAdded = new Songs();
            foreach (Songs song in DataBaseSongs)
            {
         
                //Si es que el string Songname esta en la subcadena del objeto devuelve true
                //song.Name_Song.ToLower().Contains(SongName.ToLower())
                foreach(WorkerSong workerSong in song.WorkersSong1)
                {
                    if(workerSong.Name.ToLower().Contains(name.ToLower()))
                    {
                        if(LastSongAdded == song)
                        {
                            //No se realiza nada
                        }
                        else
                        {
                            TemporarySearchSongList.Add(song);
                        }
                        
                        LastSongAdded = song;
                    }
                  
                }
            }

        }
        //int identificador señala si la busqueda es > or < or = (id = 4,5,6 respectivamente)
        public void AddtoSearchSongByAge(int identificador,int AgeInput)
        {
            Songs LastSongAdded = new Songs();
            if (identificador == 4)
            {
                foreach(Songs song in DataBaseSongs)
                {
                    foreach(WorkerSong ws in song.WorkersSong1)
                    {
                        if(ws.Age > AgeInput)
                        {
                            
                            if (LastSongAdded == song)
                            {
                                //No se realiza nada
                            }
                            else
                            {
                                TemporarySearchSongList.Add(song);
                            }

                            LastSongAdded = song;
                        }
                    }
                }
            }
            else if(identificador == 5)
            {
                foreach (Songs song in DataBaseSongs)
                {
                    foreach (WorkerSong ws in song.WorkersSong1)
                    {
                        if (ws.Age < AgeInput)
                        {

                            if (LastSongAdded == song)
                            {
                                //No se realiza nada
                            }
                            else
                            {
                                TemporarySearchSongList.Add(song);
                            }

                            LastSongAdded = song;
                        }
                    }
                }
            }
            else if (identificador == 6)
            {
                foreach (Songs song in DataBaseSongs)
                {
                    foreach (WorkerSong ws in song.WorkersSong1)
                    {
                        if (ws.Age == AgeInput)
                        {

                            if (LastSongAdded == song)
                            {
                                //No se realiza nada
                            }
                            else
                            {
                                TemporarySearchSongList.Add(song);
                            }

                            LastSongAdded = song;
                        }
                    }
                }
            }

        }
        public void AddtoSearchSongBySize(int identificador, int sizemb)
        {
            if(identificador == 7)
            {
                foreach(Songs song in DataBaseSongs)
                {
                    if (song.SongSize > sizemb)
                    {
                        TemporarySearchSongList.Add(song);
                    }
                }
            }
            else if(identificador == 8)
            {
                foreach (Songs song in DataBaseSongs)
                {
                    if (song.SongSize < sizemb)
                    {
                        TemporarySearchSongList.Add(song);
                    }
                }
            }
            else if (identificador == 9)
            {
                foreach (Songs song in DataBaseSongs)
                {
                    if (song.SongSize == sizemb)
                    {
                        TemporarySearchSongList.Add(song);
                    }
                }
            }


        }
        public void AddtoSearchSongByRankingS(int identificador,float rating)
        {
            if (identificador == 10)
            {
                foreach (Songs song in DataBaseSongs)
                {
                    Console.WriteLine(song.RankingS);
                    if (song.RankingS > rating)
                    {
                        TemporarySearchSongList.Add(song);
                    }
                }
            }
            else if (identificador == 11)
            {
                foreach (Songs song in DataBaseSongs)
                {
                    Console.WriteLine(song.RankingS);
                    if (song.RankingS < rating)
                    {
                        TemporarySearchSongList.Add(song);
                    }
                }
            }
            else if (identificador == 12)
            {
                foreach (Songs song in DataBaseSongs)
                {
                    Console.WriteLine(song.RankingS);
                    if (song.RankingS == rating)
                    {
                        TemporarySearchSongList.Add(song);
                    }
                }
            }
        }

        //Chekea los trabajadores directamente desde las canciones
        public bool CheckWorkerSong(string name,string rol,int age,string Gender)
        {
            
            foreach(Songs songs in DataBaseSongs)
            {
                foreach(WorkerSong workerSong in songs.WorkersSong1)
                {
                    if(workerSong.Name == name && workerSong.Rol == rol && workerSong.Age == age && workerSong.Gender1 == Gender)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        //Obtiene el filepath desde el nombre de la canción y el nombre de la banda
        public string ReturnFilePathMp3FromNameSandB(string SongName,string BandName)
        {
            foreach(Songs song in DataBaseSongs)
            {
                if(song.NameBand1 == BandName && song.Name_Song == SongName)
                {
                    return song.FilePath;
                }
            }
            //Si es que no se encuentra la cancion se devuelve un string vacio (no deberia pasar)
            string EmptyString = "";
            return EmptyString;
        }
  
        
        public Songs ReturnSongBySongNameAndBandName(string SongName,string BandName)
        {
            foreach(Songs song in DataBaseSongs)
            {
                if(song.Name_Song == SongName && song.NameBand1 == BandName)
                {
                    return song;
                }
            }
            Console.WriteLine("Empty");
            Songs EmptySong = new Songs();
            return EmptySong;
        }
        //Devuelve la informacion de los trabajadores en una list of list string , en la posicion 0 esta el nombre, en 1 rol , 2 edad, 3 genero,4 rnking
        public List<List<string>> ReturnWorkersSFromSelectedSong(string SongName,string BandName)
        {
            List<List<string>> InfoWS = new List<List<string>>();
            foreach(WorkerSong ws in ReturnSongBySongNameAndBandName(SongName, BandName).WorkersSong1)
            {
                InfoWS.Add(new List<string> { ws.Name,ws.Rol,ws.Age.ToString(),ws.Gender1,ws.RankingWorkerS.ToString()});
            }
            return InfoWS;
        }
  

    }
}
