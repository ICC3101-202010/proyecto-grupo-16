﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models__Proyecto_2_
{
    [Serializable]
    public class Movies
    {
        private string title;
        private string category;

        private float rankingM;
        private string DatePublish;
        private string description;
        private string studio;

        private double movieSize;
        private string typeFileM;
        private TimeSpan durationM;
        private string FilePath;

        //Faltan agregar estos atrubutos al contructor!!
        public bool Candownload;
        public int Downloads;

        private List<WorkerMovie> cast = new List<WorkerMovie>();

        private int likesM;
        private int reproductionsM;

        public string Title { get => title; set => title = value; }
        public string Category { get => category; set => category = value; }
        public float RankingM { get => rankingM; set => rankingM = value; }
    
        public string Description { get => description; set => description = value; }
        public string Studio { get => studio; set => studio = value; }
        public double MovieSize { get => movieSize; set => movieSize = value; }
        public string TypeFileM { get => typeFileM; set => typeFileM = value; }
        public TimeSpan DurationM { get => durationM; set => durationM = value; }
        public List<WorkerMovie> Cast { get => cast; set => cast = value; }
        public int LikesM { get => likesM; set => likesM = value; }
        public int ReproductionsM { get => reproductionsM; set => reproductionsM = value; }
        public string DatePublish1 { get => DatePublish; set => DatePublish = value; }
        public string FilePath1 { get => FilePath; set => FilePath = value; }

        public Movies(string Title, string Category, TimeSpan DurationM, string DatePublish, string Description, string Studio, string TypeFileM, double MovieSize,  List<WorkerMovie> Cast, int LikesM, int ReproductionsM,string FilePath)
        {
            this.Title = Title;
            this.Category = Category;
            this.Title = Title;
            this.DatePublish1 = DatePublish;
            this.Description = Description;
            this.Studio = Studio;

            this.DurationM = DurationM;
            this.MovieSize = MovieSize;
            this.TypeFileM = TypeFileM;
            this.FilePath1 = FilePath;

            this.LikesM = LikesM;
            this.ReproductionsM = ReproductionsM;

            this.Cast = Cast;

        }
        public Movies()
        {

        }

        

        public string InfoMovie()
        {

            return "Titulo: " + Title + ", Categoria: "+ Category+", TypeFile: " + TypeFileM + ", Duracion: " + DurationM + ", Ranking: " + RankingM + ", Año de publicacion: " + DatePublish1 + ", Likes: " + LikesM + ", Reproducciones: " + ReproductionsM + ", Estudio: " + Studio;

        }
        public string Descripcion()
        {
            return Description;
        }

    }
}
