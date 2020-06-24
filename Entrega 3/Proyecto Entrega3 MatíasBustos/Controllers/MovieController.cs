using Models__Proyecto_2_;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEntrega3MatíasB_MatíasR.Controllers
{
    class MovieController
    {
        private List<Movies> DataBaseMovies = new List<Movies>();

        //Lista temporal la cual va servir para verificar antes de que se agrege la data a database
        private List<WorkerMovie> WorkersMInOneMovie = new List<WorkerMovie>();
        private List<Movies> TemporarySearchMoviesList = new List<Movies>();
        private List<List<string>> SearchStringListInfo = new List<List<string>>();

        public MovieController()
        {
           
        }
        public void AddMovieToDB(string Title, string Category, TimeSpan duration, string YearPublish, string Description, string Studio, string Typefile, double MovieSize, List<WorkerMovie> Cast, string FilePath)
        {
            Movies NewMovie = new Movies(Title, Category, duration, YearPublish, Description, Studio, Typefile, MovieSize, Cast, 0, 0, FilePath,0.0);
            DataBaseMovies.Add(NewMovie);
        }

        public List<WorkerMovie> GetWorkerMovie()
        {
            return WorkersMInOneMovie;
        }
        public bool CheckWorkerMInDB(string nombre,string rol,string Gender,int age)
        {
            foreach(Movies movie in DataBaseMovies)
            {
                foreach(WorkerMovie workerMovie in movie.Cast)
                {
                    if(workerMovie.Name == nombre && workerMovie.WorkerMovieRol1 == rol && workerMovie.Gender1 == Gender && workerMovie.Age1 == age )
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public void AddNEWWorkerMToList(string nombre, string rol, string Gender, int age)
        {
            WorkerMovie workerMovie = new WorkerMovie(nombre, age, Gender, rol, 0);
            WorkersMInOneMovie.Add(workerMovie);
        }
        public void AddOldWorkerMFromDBToList(string name, string rol, string Gender, int age)
        {
            WorkersMInOneMovie.Add(GetOldWorkerMFromDB(name, rol, Gender, age));
        }
        
       
        public WorkerMovie GetOldWorkerMFromDB(string name,string rol,string Gender,int age)
        {
            foreach(Movies movie in DataBaseMovies)
            {
                foreach(WorkerMovie workerMovie in movie.Cast)
                {
                    if(workerMovie.Name == name && workerMovie.WorkerMovieRol1 == rol && workerMovie.Gender1 == Gender && workerMovie.Age1 == age )
                    {
                        return workerMovie;
                    }
                }
            }
            WorkerMovie EmptyWorkerM = new WorkerMovie();
            return EmptyWorkerM;
        }

       
        public void ClearWorkerMInList()
        {
            WorkersMInOneMovie.Clear();
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
        public int MovieChecker(string Title, string YearPublish)
        {
            foreach (Movies movie in DataBaseMovies)
            {
                if (movie.Title == Title && YearPublish == movie.DatePublish1)
                {
                    return 1;
                }
            }
            return 0;
        }

        //Metodos Busqueda peliculas por nombre peli

        public void AddtoSearchMovieByNameList(string Title)
        {

            foreach (Movies movie in DataBaseMovies)
            {
                //Si es que el string Songname esta en la subcadena del objeto devuelve true
                //song.Name_Song.ToLower().Contains(SongName.ToLower())
                if (movie.Title.ToLower().Contains(Title.ToLower()))
                {
                    TemporarySearchMoviesList.Add(movie);

                }
            }

        }
        //Busqueda por categoria
        public void AddtoSearchMovieByCategory(string Category)
        {

            foreach (Movies movie in DataBaseMovies)
            {
                //Si es que el string Songname esta en la subcadena del objeto devuelve true
                //song.Name_Song.ToLower().Contains(SongName.ToLower())
                if (movie.Category.ToLower().Contains(Category.ToLower()))
                {
                    TemporarySearchMoviesList.Add(movie);

                }
            }

        }
        public void AddtoSearchMovieByStudioName(string Studio)
        {

            foreach (Movies movie in DataBaseMovies)
            {
                //Si es que el string Songname esta en la subcadena del objeto devuelve true
                //song.Name_Song.ToLower().Contains(SongName.ToLower())
                if (movie.Studio.ToLower().Contains(Studio.ToLower()))
                {
                    TemporarySearchMoviesList.Add(movie);

                }
            }

        }
        public void AddtoSearchMovieByAge(int identificador , int AgeInput)
        {
            Movies LastMovieAdded = new Movies();
            if (identificador == 4)
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    foreach (WorkerMovie wm in movie.Cast)
                    {
                        if (wm.Age1 > AgeInput)
                        {

                            if (LastMovieAdded == movie)
                            {
                                //No se realiza nada
                            }
                            else
                            {
                                TemporarySearchMoviesList.Add(movie);
                            }

                            LastMovieAdded = movie;
                        }
                    }
                }
            }
            if (identificador == 5)
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    foreach (WorkerMovie wm in movie.Cast)
                    {
                        if (wm.Age1 < AgeInput)
                        {

                            if (LastMovieAdded == movie)
                            {
                                //No se realiza nada
                            }
                            else
                            {
                                TemporarySearchMoviesList.Add(movie);
                            }

                            LastMovieAdded = movie;
                        }
                    }
                }
            }
            if (identificador == 6)
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    foreach (WorkerMovie wm in movie.Cast)
                    {
                        if (wm.Age1 == AgeInput)
                        {

                            if (LastMovieAdded == movie)
                            {
                                //No se realiza nada
                            }
                            else
                            {
                                TemporarySearchMoviesList.Add(movie);
                            }

                            LastMovieAdded = movie;
                        }
                    }
                }
            }
        }

        public void AddtoSearchMovieBySize(int identificador, int sizemb)
        {
            if (identificador == 7)
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    if (movie.MovieSize > sizemb)
                    {
                        TemporarySearchMoviesList.Add(movie);
                    }
                }
            }
            else if (identificador == 8)
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    if (movie.MovieSize < sizemb)
                    {
                        TemporarySearchMoviesList.Add(movie);
                    }
                }
            }
            else if (identificador == 9)
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    if (movie.MovieSize == sizemb)
                    {
                        TemporarySearchMoviesList.Add(movie);
                    }
                }
            }


        }

        public void AddtoSearchMovieByRankingM(int identificador, float rating)
        {
            if (identificador == 10)
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    Console.WriteLine(movie.RankingM);
                    if (movie.RankingM > rating)
                    {
                        TemporarySearchMoviesList.Add(movie);
                    }
                }
            }
            if (identificador == 11)
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    Console.WriteLine(movie.RankingM);
                    if (movie.RankingM < rating)
                    {
                        TemporarySearchMoviesList.Add(movie);
                    }
                }
            }
            if (identificador == 12)
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    Console.WriteLine(movie.RankingM);
                    if (movie.RankingM == rating)
                    {
                        TemporarySearchMoviesList.Add(movie);
                    }
                }
            }
        }
        public void AddtoSeachMovieByWorkerName(string WorkerName)
        {
            Movies LastMovieAdded = new Movies();
            foreach (Movies movie in DataBaseMovies)
            {

                //Si es que el string Songname esta en la subcadena del objeto devuelve true
                //song.Name_Song.ToLower().Contains(SongName.ToLower())
                foreach (WorkerMovie wm in movie.Cast)
                {
                    if (wm.Name.ToLower().Contains(WorkerName.ToLower()))
                    {
                        if (LastMovieAdded == movie)
                        {
                            //No se realiza nada
                        }
                        else
                        {
                            TemporarySearchMoviesList.Add(movie);
                        }

                        LastMovieAdded = movie;
                    }

                }
            }
        }

        public void AddToSearchStringListOfList()
        {
            foreach (Movies movie in TemporarySearchMoviesList)
            {


                SearchStringListInfo.Add(new List<string> { movie.Title, movie.Studio });

            }
        }
        //Limpieza de las listas de busquedas
        public void ClearSearchLists()
        {
            SearchStringListInfo.Clear();
            TemporarySearchMoviesList.Clear();
        }
        public List<List<string>> ReturnSearchListOfListstringInfo()
        {
            return SearchStringListInfo;
        }
        public string ReturnFilePathMovieSearch(string Title, string Studio)
        {
            foreach (Movies movie in DataBaseMovies)
            {
                if (movie.Title == Title && movie.Studio == Studio)
                {
                    return movie.FilePath1;
                }
            }
            //Si es que no se encuentra la cancion se devuelve un string vacio (no deberia pasar)
            string EmptyString = "";
            return EmptyString;
        }
        public Movies ReturnMovieByTitleNameAndStudioName(string Title, string Studio)
        {
            foreach (Movies movie in DataBaseMovies)
            {
                if (movie.Title == Title && movie.Studio == Studio)
                {
                    return movie;
                }
            }
            Movies EmptyMovie = new Movies();
            return EmptyMovie;
        }

     

        public void AddNewMovieToDBWithImag(string Title, string Category, TimeSpan duration, string YearPublish, string Description, string Studio, string Typefile, double MovieSize, List<WorkerMovie> Cast, string FilePath,string ImageMovie)
        {
            //Se establece como 0 los likes y reproducciones dado que es una nueva peli
            Movies NewMovie = new Movies(Title, Category, duration, YearPublish, Description, Studio, Typefile, MovieSize, Cast, 0, 0, FilePath, 0.0);
            NewMovie.MovieImgFilePath1 = ImageMovie;
            DataBaseMovies.Add(NewMovie);
        }
    }
}
