using System;
namespace ProyectodeCurso
{
    public class Singer
    {
        //Atributos
        private string name;
        private string lastname;
        private string stagename;
        private float rankingsinger;



        //Constructor
        public Singer(string Name, string LastName, string StageName, float RankingSinger)
        {
            this.name = Name;
            this.lastname = LastName;
            this.stagename = StageName;
            this.rankingsinger = RankingSinger;
        }
        public string NameSinger{ get => name; set => name = value; }
        public string LastNameSinger { get => lastname; set => lastname = value; }
        public string StageNameSinger { get => stagename; set => stagename = value; }
        public float RankingSinger { get => rankingsinger; set => rankingsinger = value; }


        //Info del cantante
        public string InfoSinger()
        {
            return "Artista: " + name + " " + lastname + "Nombre Artístico: " + stagename + " " + "Ranking: " + rankingsinger;
        }
    }
}
