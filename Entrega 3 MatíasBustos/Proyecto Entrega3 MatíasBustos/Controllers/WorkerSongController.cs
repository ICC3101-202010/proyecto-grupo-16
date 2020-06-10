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
    class WorkerSongController
    {
        private List<WorkerSong> DataBaseWorkersS = new List<WorkerSong>();

        private List<WorkerSong> TemporaryWorkerSongs = new List<WorkerSong>();

        private List<WorkerSong> TemporaryOldWorkerS = new List<WorkerSong>();

        //Graba el contenido de la lista temporal (para asi no arroje error por guardar informacion que ya se habia borrado)
        private List<WorkerSong> NotEmptyWorkersS = new List<WorkerSong>();

        public void AddWokerS()
        {
            foreach (WorkerSong workerSong in TemporaryWorkerSongs)
            {
                DataBaseWorkersS.Add(workerSong);
            }
        }

        public bool CheckWorkerSong(string Name,int Age,string Gender,string Rol)
        {
            foreach(WorkerSong ws in DataBaseWorkersS)
            {
                if(ws.Name == Name && ws.Age == Age && ws.Gender1 == Gender && ws.Rol == Rol)
                {
                    return true;
                }
            }
            return false;
        }

       
        //Agrega trabajadores de camnciones a la lista temporal (solo agrega nuevos trabajadores a la lista temporal)
        public void AddNewWorkerSToTemporaryList(string name, int age, string Gender, string Rol)
        {
            //TemporaryListMoviesWorked es una lista vacia en un comienzo.
            WorkerSong workerSong = new WorkerSong(name, Gender, age, Rol,  0);
            TemporaryWorkerSongs.Add(workerSong);

        }
        public List<WorkerSong> GetTemporaryWorkerSongList()
        {
            //Copia el contenido de la lista temporal
            NotEmptyWorkersS = new List<WorkerSong>(TemporaryWorkerSongs);
            return NotEmptyWorkersS;
        }

        public void AddOldWorkerSToTemporaryList(WorkerSong workerSong)
        {
            TemporaryOldWorkerS.Add(workerSong);
        }
        public WorkerSong GetWorkerS(string name, int age, string gender, string Rol)
        {
            foreach (WorkerSong workerSong in DataBaseWorkersS)
            {
                if (workerSong.Name == name && workerSong.Age == age && workerSong.Gender1 == gender && workerSong.Rol == Rol)
                {
                    return workerSong;
                }
            }
            //NULL WORKER
            WorkerSong NullWorkerSong = new WorkerSong();
            return NullWorkerSong;
        }
        public List<WorkerSong> GetOldWorkersSongFromTemporary()
        {
            return TemporaryOldWorkerS;
        }
        
        public void SaveWorkersSong()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DataBaseWorkersS.bin", FileMode.Open, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DataBaseWorkersS);
            stream.Close();
        }

        public List<WorkerSong> LoadWorkerSongs()
        {
            Stream stream = new FileStream("DataBaseWorkersS.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            //Si es que el stream esta vacio , devuelvo la lista vacia de la BDD de usuarios
            if (stream.Length == 0)
            {
                stream.Close();
                return DataBaseWorkersS;
            }
            //En caso de que el archivo no este vacio , obtenemos sus datos almacenados
            IFormatter formatter = new BinaryFormatter();
            List<WorkerSong> DataBaseStoredWorkersS = (List<WorkerSong>)formatter.Deserialize(stream);
            //Guardamos los datos almacenados en el archivo en la lista de la APP
            DataBaseWorkersS = DataBaseStoredWorkersS;
            stream.Close();
            return DataBaseWorkersS;
        }

    
        public void ClearTemporaryNewWorkersS()
        {
            TemporaryWorkerSongs.Clear();
        }

        public void ShowWorkerssFromDb()
        {
            foreach(WorkerSong WS in DataBaseWorkersS)
            {
                Console.WriteLine(WS.Name +" "+WS.Gender1 + " " +WS.Rol + " " +WS.RankingWorkerS);
            }
        }


    }
}
