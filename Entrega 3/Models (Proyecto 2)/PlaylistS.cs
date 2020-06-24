﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models__Proyecto_2_
{
    [Serializable]
    public class PlaylistS
    {
        //Atributos
        private string OwnerUser;
        private string Name_PlaylistS;
        private bool PrivacyS; //true == privada / false == publica
        private string type;

        private List<Songs> listS = new List<Songs> { }; //Lista de canciones

        public string OwnerUser1 { get => OwnerUser; set => OwnerUser = value; }
        public string Name_PlaylistS1 { get => Name_PlaylistS; set => Name_PlaylistS = value; }
        public bool PrivacyS1 { get => PrivacyS; set => PrivacyS = value; }
        public string Type { get => type; set => type = value; }
        public List<Songs> ListS { get => listS; set => listS = value; }

        public PlaylistS(string ownerser, string name_playlist, bool privacy, string type, List<Songs> ListS)
        {
            this.OwnerUser1 = ownerser;
            this.Name_PlaylistS1 = name_playlist;
            this.PrivacyS1 = privacy;
            this.type = type;
            this.ListS = ListS;
        }
    

        public string InfoPlaylistS()
        {
            Console.WriteLine("Creador :" + OwnerUser1 + " Nombre de la Playlist: " + Name_PlaylistS1);
            Console.WriteLine("Canciones: ");
            foreach (Songs song in ListS)
            {
                return "Nombre: " + song.Name_Song;
            }
            return "...";
        }
        public string NamePlaylistS()
        {
            return Name_PlaylistS;
        }



    }
}