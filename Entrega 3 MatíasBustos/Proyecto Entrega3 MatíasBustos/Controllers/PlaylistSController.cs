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
    class PlaylistSController
    {
        private List<PlaylistS> DataBasePlaylistS = new List<PlaylistS>();


        //Añadir cancion y establecer typefile
        public void AddListS(Songs song,PlaylistS ListS)
        {
            if (song.TypeFileS == "mp3")
            {
                ListS.ListS.Add(song);
            }
            else
            {
                Console.WriteLine("Las canciones deben tener el mismo Filetype");
            }
        }

        public void AddPLaylistSToDB(PlaylistS playlistS)
        {
            DataBasePlaylistS.Add(playlistS);
        }


        //Buscar Cancion dentro de la playlist
        public string SongSearchinPlaylist(Songs song,PlaylistS playlistS)
        {
            if (playlistS.ListS.Contains(song))
            {
                return song.InfoSong();
            }
            else
            {
                return "Canción no encontrada dentro de la playlist " + playlistS.Name_PlaylistS1;
            }

        }


        public void SavePlaylistS()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DataBasePlaylistM.bin", FileMode.Open, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DataBasePlaylistS);
            stream.Close();
        }
        public List<PlaylistS> LoadPlaylistS()
        {
            Stream stream = new FileStream("DataBasePlaylistM.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios
            if (stream.Length == 0)
            {
                stream.Close();
                return DataBasePlaylistS;
            }
            //En caso de que el archivo no este vacio , obtenemos sus datos almacenados
            IFormatter formatter = new BinaryFormatter();
            List<PlaylistS> DataBaseStoredPlaylistS = (List<PlaylistS>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBasePlaylistS = DataBaseStoredPlaylistS;
            stream.Close();
            return DataBasePlaylistS;
        }

        


    }
}
