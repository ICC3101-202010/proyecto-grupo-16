using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E3POO
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSongs_Click(object sender, EventArgs e)
        {
            using (Songs ventanaCanciones = new Songs())
                ventanaCanciones.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMovies_Click(object sender, EventArgs e)
        {
            using (Movies ventanaPeliculas = new Movies())
                ventanaPeliculas.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Admin ventanaAdin = new Admin())
                ventanaAdin.ShowDialog();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtNUsuario.Text=="Admin" && txtPassword1.Text=="1234")
            {
                panelInicio.Visible = false;
            }
            else
            {
                lblUsuario.BringToFront();
                lblUsuario.Text = "Nombre de Usuario/Incorrectos";
            }
        }

        private void btnExitAll_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
