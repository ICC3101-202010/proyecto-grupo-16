using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_APP_SpotiNetflix
{
    [Serializable]
    class WorkerMovie
    {
        //Real Name
        private string Name;
        private int Age;
        private string Gender;

        

        private string WorkerMovieRol;
        private float RankingWM;

        private List<Movies> MoviesIn;
        private List<Profile> WorkerMovieFollowers;

        public WorkerMovie(string Name,int Age,string Gender, string WorkerMovieRol, float RankingWM)
        {
            this.Name = Name;
            this.WorkerMovieRol = WorkerMovieRol;
            this.RankingWM = RankingWM;

            this.Age = Age;
            this.Gender = Gender;


        }

        public string Name1 { get => Name; set => Name = value; }
        public int Age1 { get => Age; set => Age = value; }
        public string Gender1 { get => Gender; set => Gender = value; }
        public string WorkerMovieRol1 { get => WorkerMovieRol; set => WorkerMovieRol = value; }
        public float RankingWM1 { get => RankingWM; set => RankingWM = value; }
    }
}
