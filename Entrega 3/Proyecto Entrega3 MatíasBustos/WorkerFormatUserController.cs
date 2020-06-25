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
    public partial class WorkerFormatUserController : UserControl
    {
        public WorkerFormatUserController()
        {
            InitializeComponent();
        }

        #region Properties

        private string _NombreW;
        private string _RolW;
        private string _EdadW;
        private string _Sexo;
        private string _RankingW;
        private string _BandaOEstudio;
        private string _Titulo;
        private string _Identificador;
        [Category("Custom Props")]
        public string NombreW
        {
            get { return _NombreW; }
            set { _NombreW = value; WorkerNamelbl.Text = value; }
        }
        [Category("Custom Props")]
        public string RolW
        {
            get { return _RolW; }
            set { _RolW = value; WorkerRollbl.Text = value; }
        }
        public string EdadW
        {
            get { return _EdadW; }
            set { _EdadW = value; WorkerAgelbl.Text = value; }
        }
        public string Sexo
        {
            get { return _Sexo; }
            set { _Sexo = value; WorkerGenrelbl.Text = value; }
        }

        [Category("Custom Props")]
        public string RankingW
        {
            get { return _RankingW; }
            set { _RankingW = value; WorkerRankinglbl.Text = value; }

        }
        [Category("Custom Props")]
        public string Identificador
        {
            get { return _Identificador; }
            set { _Identificador = value; }

        }
        public string BandaOEstudio
        {
            get { return _BandaOEstudio; }
            set { _BandaOEstudio = value; }

        }
        public string Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; }

        }

        #endregion

        private void WorkerFormatUserController_Load(object sender, EventArgs e)
        {

        }
    }
}
