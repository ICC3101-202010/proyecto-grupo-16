using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models__Proyecto_2_
{
    [Serializable]
    public class PlaylistM
    {
        public string OwnerUser;
        public string Name_PlaylistM;
        public bool PrivacyM; //true == privada / false == publica
        public string TypeM;

        public List<Movies> ListM = new List<Movies>();
        
        public PlaylistM(string owneruser, string name_playlistm, bool privacym, string typem, List<Movies> ListM)
        {
            this.OwnerUser = owneruser;
            this.Name_PlaylistM = name_playlistm;
            this.PrivacyM = privacym;
            this.TypeM = typem;
            this.ListM = ListM;
        }
        public string PlaylistMinfo()
        {
            return "Creador: " + OwnerUser + ", NombrePlaylist: " + Name_PlaylistM + ", tipo: " + TypeM;
        }

    }
}
