using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectodeCurso
{
    class APP
    {
        protected List<Songs> DataBaseSongs = new List<Songs>();
        protected List<PlaylistS> DataBasePlaylistS = new List<PlaylistS> { };
        protected List<Singer> DataBaseSingers = new List<Singer>();
        public List<Songs> KeyWordSongs = new List<Songs> { };
        public List<PlaylistS> KeyWordPlaylistS = new List<PlaylistS> { };
        public List<Singer> KeyWordSingers = new List<Singer> { };

        public APP()
        {

        }
        //Buscar cancion
        public string SearchSong(Songs song)
        {
            if (DataBaseSongs.Contains(song))
            {
                return song.InfoSong();
            }
            else //No la contiene
            {
                return "Canción no encontrada";
            }
        }
        public int SongTime(Songs song) //Calculo del tiempo de la cancion
        {

        }
        public string SongsDownloaded() //Canciones descargadas
        {

        }
        public string SearchKeyWord(string Key, string type) //En teoria deberia entregar todas las canciones que tengan la coincidencia
        {
            if (type == "Música" || type == "Musica" || type == "musica" || type == "música")
            {
                foreach (Songs song in DataBaseSongs) //Busca Canciones
                {
                    if (Key == song.Name || Key == song.Album || Key == song.Songgenre || Key == song.NameSinger || Key == song.Composer || Key == song.Yearpublishs
                    || Key == song.TypefileS)
                    {
                        KeyWordSongs.Add(song); //Agrega las canciones que cumplan la coincidencia con el keyword
                    }
                }
                foreach (Songs song1 in KeyWordSongs)
                {
                    return song1.InfoSong(); //Retorna la info de las canciones de la lista (las que coinciden)
                }
                foreach (PlaylistS playlist in DataBasePlaylistS) //Busca Playlists
                {
                    if (Key == playlist.Name_PlaylistS1 || Key == playlist.OwnerUser1 || Key == playlist.Type)
                    {
                        KeyWordPlaylistS.Add(playlist); //Agrega las playlist que cumplan la coincidencia con el keyword
                    }
                }
                foreach (PlaylistS playlist in KeyWordPlaylistS)
                {
                    return playlist.InfoPlaylistS(); //Retorna la info de las playlist de la lista (las que coinciden)
                }
                foreach (Singer singer in DataBaseSingers) //Busca Cantantes
                {
                    if (Key == singer.NameSinger || Key == singer.LastNameSinger || Key == singer.StageNameSinger)
                    {
                        KeyWordSingers.Add(singer); //Agrega las playlist que cumplan la coincidencia con el keyword
                    }
                }
                foreach (Singer singer in KeyWordSingers)
                {
                    return singer.InfoSinger(); //Retorna la info de los cantantes de la lista que coinciden con la busqueda
                }


                KeyWordSongs.Clear(); //Se resetea la lista para una nueva busqueda de Canciones
                KeyWordPlaylistS.Clear(); ////Se resetea la lista para una nueva busqueda de Playlists
                KeyWordSingers.Clear(); //Se resetea la lista para una nueva busqueda de Cantantes
            }
            if (type == "Películas") //Lo mismo pero con los métodos de las películas
            {
                return "";
            }
            if (KeyWordSongs.Count == 0) //Si la lista no contiene canciones ningun tuvo coincidencia con el keyword
            {
                return "No se encontraron coincidencias";
            }
            else
            {
                return "Error, porfavor ingrese una palabra clave válida";
            }
        }


        //Agregar Cancion
        public void AddSong(Songs song)
        {
            DataBaseSongs.Add(song);
        }
        public void EliminatePlaylist(PlaylistS playlist)
        {
            if(DataBasePlaylistS.Contains(playlist))
            {
                DataBasePlaylistS.Remove(playlist);
            }
        }

        //MB vi que ya están los métodos para guardar en la memoria y para verificar las canciones, asi que hago el resto de métodos




        //Busca una playlist
        public string SearchPlaylistS(string NameP) //Arreglar Metodo
        {
            foreach (PlaylistS playlist in DataBasePlaylistS)
            {
                if (playlist.Name_PlaylistS1 == NameP)
                {
                    if (playlist.PrivacyS1 == true) //Privada no se puede ver
                    {
                        return "Lo lamentamos, esta Playlist es privada";
                    }
                    if (playlist.PrivacyS1 == false) //Publica se puede ber
                    {
                        return playlist.InfoPlaylistS();
                    }
                }
                if (playlist.Name_PlaylistS1!=NameP)
                {
                    return "";
                }
            }
            return "";
        }
        public void AddPlaylistStoDataBase(PlaylistS playlist)
        {
            DataBasePlaylistS.Add(playlist);
        }
        public int PlaylistsIn()
        {
            foreach (PlaylistS playlist in DataBasePlaylistS)
            {
                Console.WriteLine(playlist.InfoPlaylistS());
            }
            return DataBasePlaylistS.Count;
        }

    }
}
