using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_APP_SpotiNetflix
{
    [Serializable]
    class Movies
    {
        
        private string Title;
        private string Category;
        
        private float RankingM;
        private string YearPublish;
        private string Description;
        private string Studio;

        private double MovieSize;
  
        private string TypeFileM;
        private double DurationM;
        private string FileNameImage;

        private List<WorkerMovie> Cast = new List<WorkerMovie>();

        private int LikesM;
        private int ReproductionsM;

        

        public Movies(string Title, string Category ,double DurationM , string YearPublish,string Description,string Studio,string TypeFileM,double MovieSize,string FileNameImage, List<WorkerMovie> Cast)
        {
            this.Title = Title;
            this.Category = Category;
            this.Title = Title;
            this.YearPublish = YearPublish;
            this.Description = Description;
            this.Studio = Studio;

            this.DurationM = DurationM;
            this.MovieSize = MovieSize;
            this.TypeFileM = TypeFileM;
            this.FileNameImage = FileNameImage;
            this.Cast = Cast;

        }

        public string Title1 { get => Title; set => Title = value; }
        public string Category1 { get => Category; set => Category = value; }
        public float RankingM1 { get => RankingM; set => RankingM = value; }
        public string YearPublish1 { get => YearPublish; set => YearPublish = value; }
        public string Description1 { get => Description; set => Description = value; }
        public string Studio1 { get => Studio; set => Studio = value; }
        public double MovieSize1 { get => MovieSize; set => MovieSize = value; }
        public string TypeFileM1 { get => TypeFileM; set => TypeFileM = value; }
        public double DurationM1 { get => DurationM; set => DurationM = value; }
        public string FileNameImage1 { get => FileNameImage; set => FileNameImage = value; }
        public int LikesM1 { get => LikesM; set => LikesM = value; }
        public int ReproductionsM1 { get => ReproductionsM; set => ReproductionsM = value; }

        public void Anadirtrabajador(WorkerMovie trabajador)
        {
            Cast.Add(trabajador);
        }
        public string InfoMovie()
        {

            return "Titulo: " + Title + ", Categoria: " + Category + ", Duracion: " + DurationM + ", Ranking: " + RankingM + ", Año de publicacion: " + YearPublish + ", Likes: " + LikesM + ", Reproducciones: " + ReproductionsM + ", Estudio: " + Studio;

        }
        public string Descripcion()
        {
            return Description;
        }
        
    }
}
