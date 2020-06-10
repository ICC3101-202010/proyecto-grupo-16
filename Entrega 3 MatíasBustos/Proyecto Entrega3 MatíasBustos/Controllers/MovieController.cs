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
    class MovieController
    {
        private List<Movies> DataBaseMovies = new List<Movies>();

        //Lista temporal la cual va servir para verificar antes de que se agrege la data a database
        public List<Movies> TemporaListMovies = new List<Movies>();

        protected List<Movies> KeyWordMovies = new List<Movies> { };
        protected List<Movies> KeyWordRankingigualmovies = new List<Movies> { };
        protected List<Movies> KeyWordRankingmenormovies = new List<Movies> { };
        protected List<Movies> KeyWordRankingmayormovies = new List<Movies> { };
        protected List<Movies> KeyWordCategoria = new List<Movies> { };


        public MovieController()
        {
           
        }


        public string SearchKeywordM(string Key,string type)
        {
            if (type == "Películas") //Lo mismo pero con los métodos de las películas
            {
                foreach (Movies movie in DataBaseMovies)
                {
                    if (Key == movie.Title || Key == movie.Category || Key == movie.Description || Key == movie.Studio || Key == movie.DatePublish1 || Key == movie.TypeFileM)
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
        public void AddMovieToDB()
        {
            
            //Obtiene la data de la pelicula en la lista temporal
            DataBaseMovies.Add(GetTemporaryMovieList());
        }
        public void AddMovieToTemporaryList(string Title, string Category, TimeSpan duration, string YearPublish, string Description, string Studio, string Typefile, double MovieSize, List<WorkerMovie> Cast,string FilePath)
        {
            //Se establece como 0 los likes y reproducciones dado que es una nueva peli
            Movies NewMovie = new Movies(Title, Category, duration, YearPublish, Description, Studio, Typefile, MovieSize, Cast, 0, 0, FilePath);
            TemporaListMovies.Add(NewMovie);
        }
        public Movies GetTemporaryMovieList()
        {
            foreach(Movies movie in TemporaListMovies)
            {
                //Retorna el unico elemento contenido en la lista temporal
                return movie;
            }
            //No deberia pasar a este caso
            Movies EmptyMovie = new Movies();
            return EmptyMovie;
        }
      
        public void ClearTemporaryMovieList()
        {
            TemporaListMovies.Clear();
          
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

        public void SearchEvaligualmovies(float Key) //guardar en lista las peliculas que sean igual al valor del ranking key
        {
            foreach (Movies movie in DataBaseMovies)
            {
                if (Key == movie.RankingM)
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
                if (Key < movie.RankingM)
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
                if (Key > movie.RankingM)
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
                if (categoria == movie.Category)
                {
                    KeyWordCategoria.Add(movie);

                }
            }
            if (KeyWordCategoria.Count == 0)
            {
                Console.WriteLine("No existe esa categoria");
            }

        }

      
        //Se agregan los trabajadores viejos al cast de la pelicula
        public void ModifyAtributtesFromOldWorker(string Title, string DatePublish,List<WorkerMovie> ListOldWorkersMovie)
        {
            foreach(WorkerMovie wk in ListOldWorkersMovie)
            {
                GetMovies(Title, DatePublish).Cast.Add(wk);
            }
            
        }

        public Movies GetMovies(string Title,string DatePublish)
        {
            foreach(Movies movie in DataBaseMovies)
            {
                if(movie.DatePublish1 == DatePublish && movie.Title == Title)
                {
                    return movie;
                }
            }
            Movies EmptyMovie = new Movies();
            return EmptyMovie;

        }
        public void ShowMovies()
        {
            foreach(Movies movies in DataBaseMovies)
            {
                Console.WriteLine(movies.InfoMovie()); 
            }
        }

    }
}
