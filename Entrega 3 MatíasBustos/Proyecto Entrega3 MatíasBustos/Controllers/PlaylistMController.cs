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
    class PlaylistMController
    {

        private List<PlaylistM> DataBasePlaylistM = new List<PlaylistM>();

        public void SavePlaylistM()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DataBasePlaylistM.bin", FileMode.Open, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DataBasePlaylistM);
            stream.Close();
        }
        public List<PlaylistM> LoadPlaylistM()
        {
            Stream stream = new FileStream("DataBasePlaylistM.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios
            if (stream.Length == 0)
            {
                stream.Close();
                return DataBasePlaylistM;
            }
            //En caso de que el archivo no este vacio , obtenemos sus datos almacenados
            IFormatter formatter = new BinaryFormatter();
            List<PlaylistM> DataBaseStoredPlaylistM = (List<PlaylistM>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBasePlaylistM = DataBaseStoredPlaylistM;
            stream.Close();
            return DataBasePlaylistM;
        }

        public void AddPlaylistToDBB(PlaylistM playlistM)
        {
            DataBasePlaylistM.Add(playlistM);
        }



    }
}
