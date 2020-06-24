using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoEntrega3MatíasB_MatíasR.Controllers;
using ProyectoEntrega3MatíasB_MatíasR;
using System.Runtime.CompilerServices;

namespace ProyectoEntrega3MatíasBustos
{
    public partial class SongMovieFormatShow : UserControl
    {



        public SongMovieFormatShow()
        {
            InitializeComponent();
            
        }
     
        #region Properties

        private string _Titulo;
        private string _NombreBanda;
        private string _Identificador;
        private Image _AlbumImage;
        [Category("Custom Props")]
        public string Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; TitleSongLbl.Text = value; }
        }
        [Category("Custom Props")]
        public string NombreBanda
        {
            get { return _NombreBanda; }
            set { _NombreBanda = value; BandLbl.Text = value; }
        }
        public string Identificador
        {
            get { return _Identificador; }
            set { _Identificador = value; }
        }

        [Category("Custom Props")]
        public Image AlbumImage
        {
            get { return _AlbumImage; }
            set { _AlbumImage = value; AlbumImagePictureBox.Image = value; }

        }

        #endregion


        //Clikeo de la Cancion/Pelicula
        private void SongsFormat_Click(object sender, EventArgs e)
        {
            //Se agrega a una lista de string la información de la canción clickeada
            
        }

        private void SongMovieFormatShow_Load(object sender, EventArgs e)
        {

        }
    }
        
}
