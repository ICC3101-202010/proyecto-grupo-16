using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_APP_SpotiNetflix
{
    [Serializable]
    class Singer
    {
        private string Name;

        private string Gender;
        private int Age;

        private string stagename;
        private float rankingsinger;

        private List<Songs> SongS = new List<Songs>();
        private List<Profile> SingerFollowers = new List<Profile>();

        public string Name1 { get => Name; set => Name = value; }
        public string Gender1 { get => Gender; set => Gender = value; }
        public int Age1 { get => Age; set => Age = value; }
        public string Stagename { get => stagename; set => stagename = value; }
        public float Rankingsinger { get => rankingsinger; set => rankingsinger = value; }





        //Constructor
        public Singer(string Name, string StageName, float RankingSinger,string Gender,int Age)
        {
            this.Name = Name;
          
            this.stagename = StageName;
            this.rankingsinger = RankingSinger;

            this.Gender = Gender;
            this.Age = Age;
        }



        //Info del cantante
        public string InfoSinger()
        {
            return "Artista: " + Name + "Nombre Artístico: " + stagename + " " + "Ranking: " + rankingsinger;
        }
        //Agrega todas las canciones del artista
        public void AddSongsSinger(Songs song)
        {
            SongS.Add(song);
        }
    }
}
