using Models__Proyecto_2_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEntrega3MatíasB_MatíasR.Controllers
{
    class SongsController
    {
        private List<Songs> DataBaseSongs = new List<Songs>();

        private List<Songs> TemporaryListSongs = new List<Songs>();

        private List<Songs> TemporarySearchSongList = new List<Songs>();

        protected List<Songs> KeyWordRankingigualsongs = new List<Songs> { };//lista para en las cuales son igual la key y el ranking de songs
        protected List<Songs> KeyWordRankingmayorsongs = new List<Songs> { };
        protected List<Songs> KeyWordRankingmenorsongs = new List<Songs> { };
        protected List<Songs> KeyWordSongs = new List<Songs> { };

        //SONGS
        //Metodos para las acciones de los usuarios
        public void AddSongToDB()
        {
            //Obtiene la data de la pelicula en la lista temporal
            DataBaseSongs.Add(GetTemporarySongList());
        }
        public Songs GetTemporarySongList()
        {
            foreach (Songs song in TemporaryListSongs)
            {
                //Retorna el unico elemento contenido en la lista temporal
                return song;
            }
            //No deberia pasar a este caso
            Songs EmptySong = new Songs();
            return EmptySong;
        }
        //Checkea si la canción se encuentra en la BDD de canciones
        public bool SongChecker(string name_song,string album,string lyrics,string songGenre,string DatePublish)
        {
            foreach(Songs song in DataBaseSongs)
            {
                if(name_song == song.Name_Song && album == song.Album && lyrics == song.Lyrics && songGenre == song.SongGenre )
                {
                    return true;
                }
               
            }
            return false;
        }
        public void AddNewSongToTemporaryList(string name_song,string Album, string GenreSong, TimeSpan durationS, string DatePublish, string Lyrics,  string TypefileS, double SongSize, List<WorkerSong> workerSongs, string FilePathS)
        {
            //Se establece como 0 los likes y reproducciones dado que es una nueva peli
            Songs NewSong = new Songs(name_song, Album, GenreSong, 0, Lyrics, DatePublish, workerSongs, FilePathS, TypefileS, SongSize, durationS, 0, 0, 0);
            TemporaryListSongs.Add(NewSong);
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

        //Se agregan los trabajadores viejos al cast de la pelicula
        public void ModifyAtributtesFromOldWorkerS(string name_song, string DatePublish,string Album ,string Genre,List<WorkerSong> ListOldWorkersSong)
        {
            foreach (WorkerSong ws in ListOldWorkersSong)
            {
                GetSongs(name_song, DatePublish, Album, Genre).WorkersSong1.Add(ws);
            }

        }
        public Songs GetSongs(string name_song,string Album,string Genre, string DatePublish)
        {
            foreach (Songs song in DataBaseSongs)
            {
                if (song.DatePublish == DatePublish && song.Name_Song == name_song && song.Album == Album && song.SongGenre == Genre)
                {
                    return song;
                }
            }
            Songs EmptySong = new Songs();
            return EmptySong;

        }
        public void ClearTemporarySongList()
        {
            TemporaryListSongs.Clear();

        }
        public void ShowSongs()
        {
            foreach(Songs songs in DataBaseSongs)
            {
                Console.WriteLine(songs.InfoSong()); 
            }
        }
        public List<string> ShowSongsInListString()
        {
            List<string> ListString = new List<string>();

            foreach (Songs songs in DataBaseSongs)
            {
                ListString.Add(songs.InfoSong());
            }
            return ListString;
        }
        public string ReturnFilePathFromDB(int index)
        {
            int count = 0;
            foreach(Songs song in DataBaseSongs)
            {
                if(count == index)
                {
                    return song.FilePath;
                }
                count++;
            }
            return "";
        }



    }
}
