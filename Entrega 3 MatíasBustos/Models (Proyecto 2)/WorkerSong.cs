using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models__Proyecto_2_
{
    [Serializable]
    public class WorkerSong
    {
        private string name;
        private string Gender;

        private int age;


        private string rol;
        private float rankingWorkerS;

        public string Name { get => name; set => name = value; }
        public string Gender1 { get => Gender; set => Gender = value; }
        public int Age { get => age; set => age = value; }
        public string Rol { get => rol; set => rol = value; }
        public float RankingWorkerS { get => rankingWorkerS; set => rankingWorkerS = value; }

        public WorkerSong(string name, string gender, int age, string rol, float rankingWorkerS)
        {
            this.name = name;
            this.Gender = gender;
            this.age = age;
            this.Rol = rol;
            this.RankingWorkerS = rankingWorkerS;
        }
        public WorkerSong()
        {

        }












        //Info del cantante
        public string InfoSinger()
        {
            return "Artista: " + Name + "Nombre Artístico: " + Rol + " " + "Ranking: " + RankingWorkerS;
        }


    }
}
