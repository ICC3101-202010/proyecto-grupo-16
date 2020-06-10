using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models__Proyecto_2_
{
    [Serializable]
    public class Songs
    {
        /* Atributos que deben ser sacados cuando se agregan canciones desde el archivo c/r
        
        string AudioQuality;
        
       

        */
        private string name_Song;
        private string album;
        private string songGenre;

        private float rankingS;
        private string lyrics;
        private string datePublish;
        private List<WorkerSong> WorkersSong = new List<WorkerSong>();


        private string filePath;
        private string typeFileS;
        private double songSize;
        private TimeSpan durationS;

        private int likesS;
        private int reproductionsS;
        private int downloadsS;

        public string Name_Song { get => name_Song; set => name_Song = value; }
        public string Album { get => album; set => album = value; }
        public string SongGenre { get => songGenre; set => songGenre = value; }
        public float RankingS { get => rankingS; set => rankingS = value; }
        public string Lyrics { get => lyrics; set => lyrics = value; }
        public string DatePublish { get => datePublish; set => datePublish = value; }
        public List<WorkerSong> WorkersSong1 { get => WorkersSong; set => WorkersSong = value; }
        public string FilePath { get => filePath; set => filePath = value; }
        public string TypeFileS { get => typeFileS; set => typeFileS = value; }
        public double SongSize { get => songSize; set => songSize = value; }
        public TimeSpan DurationS { get => durationS; set => durationS = value; }
        public int LikesS { get => likesS; set => likesS = value; }
        public int ReproductionsS { get => reproductionsS; set => reproductionsS = value; }
        public int DownloadsS { get => downloadsS; set => downloadsS = value; }

        public Songs(string name_Song, string album, string songGenre, float rankingS, string lyrics, string datePublish, List<WorkerSong> workersSong, string filePath, string typeFileS, double songSize, TimeSpan durationS, int likesS, int reproductionsS, int downloadsS)
        {
            Name_Song = name_Song;
            Album = album;
            SongGenre = songGenre;
            RankingS = rankingS;
            Lyrics = lyrics;
            this.DatePublish = datePublish;
            WorkersSong1 = workersSong;
            this.FilePath = filePath;
            TypeFileS = typeFileS;
            SongSize = songSize;
            this.DurationS = durationS;
            LikesS = likesS;
            ReproductionsS = reproductionsS;
            DownloadsS = downloadsS;
        }
        public Songs()
        {

        }

        /*
Este atributo no es valido dado que los usarios premium pueden solo descargar las canciones, por lo tanto
de perfiles se define con si es premium o no si lo pueden descargar.
private bool Candownload;
*/







        //Informacion de la Cancion
        public string InfoSong()
        {
            if (true)
            {
                return "Album: " +Album + " Nombre Canción: " + Name_Song + " Artista: " + " Compositor: "  + "Ranking: " + RankingS + " Duración: " +DurationS + " Likes: " + LikesS + " Descargas: " + downloadsS +" Reproducciones: " + ReproductionsS + "Type file:" +TypeFileS;
            }
           
        }



    }
}
