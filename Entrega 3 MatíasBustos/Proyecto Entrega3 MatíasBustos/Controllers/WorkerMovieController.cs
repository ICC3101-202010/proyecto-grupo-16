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
    class WorkerMovieController
    {
        //DataBaseWorkersM Guarda todos los trabajadores de pelis
        private List<WorkerMovie> DataBaseWorkersM = new List<WorkerMovie>();

        //TemporaryListWorkersM es una lista temporal la cual va agegando trabajadores a un pelicula en particular
        //Y despues se resetea para agregar trabajadores a otra peli
        private List<WorkerMovie> TemporaryListWorkersM = new List<WorkerMovie>();
        private List<WorkerMovie> OldWorkersTemporaryList = new List<WorkerMovie>();

        //Graba el contenido de la lista temporal (para asi no arroje error por guardar informacion que ya se habia borrado)
        private List<WorkerMovie> NotEmptyWorkers = new List<WorkerMovie>();





        public void AddWokerM()
        {
            foreach (WorkerMovie worker in TemporaryListWorkersM)
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

        public WorkerMovieController()
        {

        }


        public bool CheckWorkerInDB(string name, int age, string Gender, string Rol)
        {
            foreach(WorkerMovie workerMovie in DataBaseWorkersM)
            {
                if(workerMovie.Name == name && workerMovie.Age ==age && workerMovie.Gender == Gender && workerMovie.WorkerMovieRol == Rol)
                {
                    return true;
                }
            }
            return false;

        }




        //Agrega trabajadores de pelis a la lista temporal (solo agrega nuevos trabajadores a la lista temporal)
        public void AddNewWorkerMToTemporaryList(string name,int age,string Gender,string Rol)
        {
            //TemporaryListMoviesWorked es una lista vacia en un comienzo.
            WorkerMovie workerMovie = new WorkerMovie(name,age,Gender, Rol,0);
            TemporaryListWorkersM.Add(workerMovie);
            
        }
        public void AddOldWorkerToTemporaryList(WorkerMovie workerMovie)
        {
            OldWorkersTemporaryList.Add(workerMovie);
        }
        
        public List<WorkerMovie> GetOldWorkersMoviesFromTemporary()
        {
            return OldWorkersTemporaryList;
        }
        
        public WorkerMovie GetWorker(string name, int age, string Gender, string Rol)
        {
            foreach (WorkerMovie workerMovie in DataBaseWorkersM)
            {
                if (workerMovie.Name == name && workerMovie.Age == age && workerMovie.Gender == Gender && workerMovie.WorkerMovieRol == Rol)
                {
                    return workerMovie;
                }
            }
            //NULL WORKER
            WorkerMovie workerMovie1 = new WorkerMovie();
            return workerMovie1;
        }
        //Devuelve la lista temporal de trabajadores
        public List<WorkerMovie> GetTemporaryWorkerMovies()
        {
            //Se crea una nueva lista , y se copia la info de la lista temporal
            
            NotEmptyWorkers = new List<WorkerMovie>(TemporaryListWorkersM);

            return NotEmptyWorkers;
        }
        //Limpia la lista temporal de trabajadores pelis y crea una nueva lista temporal (si no tira error)
        
        //Carga los trabajadores de la lista temporal a la BDD de workers
        public void LoadWorkersFromTemporaryListToDBB()
        {
            foreach(WorkerMovie workerMovie in TemporaryListWorkersM)
            {
                DataBaseWorkersM.Add(workerMovie);
            }
        }

        

        public void ClearTemporaryOldWorkers()
        {
            OldWorkersTemporaryList.Clear();
        }
        public void ClearTemporaryNewWorkers()
        {
            TemporaryListWorkersM.Clear();
        }

        public void ShowDataBaseWorkersM()
        {
            foreach(WorkerMovie workerMovie in DataBaseWorkersM)
            {
                Console.WriteLine(workerMovie.Name +" "+ workerMovie.Age + " " +workerMovie.WorkerMovieRol1);
            }
        }


        
    }   

}
