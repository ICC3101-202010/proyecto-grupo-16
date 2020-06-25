using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoEntrega3MatíasBustos
{
    public partial class PlaylistFormatUserController : UserControl
    {
        public PlaylistFormatUserController()
        {
            InitializeComponent();
        }


        #region Properties

        private string _PlaylistName;
        private string _OwnerUsername;
        private string _IdentificadorPlaylist;
        private string _TypePlaylistStorage;
        private Image _UsernamePic;
        [Category("Custom Props")]
        public string PlaylistName
        {
            get { return _PlaylistName; }
            set { _PlaylistName = value; PlaylistSongNamelbl.Text = value; }
        }
        [Category("Custom Props")]
        public string OwnerUsername
        {
            get { return _OwnerUsername; }
            set { _OwnerUsername = value; OwnerUsernamelbl.Text = value; }
        }
        [Category("Custom Props")]
        public string IdentificadorPlaylist
        {
            get { return _IdentificadorPlaylist; }
            set { _IdentificadorPlaylist = value; }
        }
        [Category("Custom Props")]
        public string TypePlaylistStorage
        {
            get { return _TypePlaylistStorage; }
            set { _TypePlaylistStorage = value; }
        }
        [Category("Custom Props")]
        public Image UsernamePic
        {
            get { return _UsernamePic; }
            set { _UsernamePic = value; OwnerUserPictureBox.Image = value; }
        }

        #endregion

        private void PlaylistFormatUserController_Load(object sender, EventArgs e)
        {

        }
    }
}
