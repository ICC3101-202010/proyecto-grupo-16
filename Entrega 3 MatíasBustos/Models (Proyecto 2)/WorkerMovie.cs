using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models__Proyecto_2_
{
    [Serializable]
    public class WorkerMovie
    {
        //Real Name
        public string Name;
     
        public int Age;
        public string Gender;
        public string WorkerMovieRol;
        public float RankingWM;

        
        public WorkerMovie()
        {

        }

        public WorkerMovie(string Name, int Age, string Gender, string WorkerMovieRol, float RankingWM)
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
