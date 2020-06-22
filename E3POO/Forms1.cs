using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace E3POO
{
    public partial class Inicio : Form
    {
        string[] Manual;
        public Inicio()
        {
            InitializeComponent();
            try
            {
                StreamReader sw5 = new StreamReader(Application.StartupPath + "\\archivostxt\\ManualdeUsuario.txt", true);
                Manual = sw5.ToString().Split(new[] { "\n\n" }, StringSplitOptions.None);
                for (int i = 0; i <= Manual.Length + 1000; i++)
                {
                    lstManual.Items.Add(sw5.ReadLine());
                }
                sw5.Close();
            }
            catch
            { }
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

        private void button1_Click(object sender, EventArgs e)  //Abrir forms admin
        {
            //ActiveForm.Size = new Size(10, 10);
            using (Admin ventanaAdmin = new Admin())
                ventanaAdmin.ShowDialog();  
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            StreamReader lector = new StreamReader(Application.StartupPath + "\\archivostxt\\private-UsPss.txt", true);
            string Nombre = "";
            string Contraseña = "";
            while(!lector.EndOfStream)
            {
                string x = lector.ReadLine();
                if(x==txtNUsuario.Text)
                {
                    Nombre = x.ToString();
                    Contraseña = lector.ReadLine();
                    if(Nombre==txtNUsuario.Text && Contraseña==txtPassword1.Text)
                    {
                        panelInicio.Visible = false;
                    }
                    else
                    {
                        lblUsuario.BringToFront();
                        lblUsuario.Text = "Nombre de Usuario/Incorrectos";
                    }
                }
                else
                {

                }
                
            }
            lector.Close();
        }

        private void btnExitAll_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            panelManual.BringToFront();
            panelManual.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelManual.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDia_Click(object sender, EventArgs e) //Modo Día
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;

            lblTitulo2.BackColor = Color.LightBlue;
            lblTitulo2.ForeColor = Color.Black;

            lblSlogan1.BackColor = Color.LightBlue;
            lblSlogan1.ForeColor = Color.Black;

            lblAdm1.BackColor = Color.White;
            lblAdm1.ForeColor = Color.Black;
            lblAdm2.BackColor = Color.White;
            lblAdm2.ForeColor = Color.Black;

            lblSong1.BackColor = Color.LightGreen;
            lblSong1.ForeColor = Color.Black;
            lblSong2.BackColor = Color.LightGreen;
            lblSong2.ForeColor = Color.Black;
            lblSong3.BackColor = Color.LightGreen;
            lblSong3.ForeColor = Color.Black;
            lblSong4.BackColor = Color.LightGreen;
            lblSong4.ForeColor = Color.Black;

            lblMovie1.BackColor = Color.LightGreen;
            lblMovie1.ForeColor = Color.Black;
            lblMovie2.BackColor = Color.LightGreen;
            lblMovie2.ForeColor = Color.Black;
            lblMovie3.BackColor = Color.LightGreen;
            lblMovie3.ForeColor = Color.Black;

            lblVersion.BackColor = Color.LightGreen;
            lblVersion.ForeColor = Color.Black;



        }

        private void button4_Click(object sender, EventArgs e)  //Modo Noche
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;

            lblTitulo2.BackColor = Color.Black;
            lblTitulo2.ForeColor = Color.White;

            lblSlogan1.BackColor = Color.Black;
            lblSlogan1.ForeColor = Color.White;

            lblAdm1.BackColor = Color.Black;
            lblAdm1.ForeColor = Color.White;
            lblAdm2.BackColor = Color.Black;
            lblAdm2.ForeColor = Color.White;

            lblSong1.BackColor = Color.Black;
            lblSong1.ForeColor = Color.White;
            lblSong2.BackColor = Color.Black;
            lblSong2.ForeColor = Color.White;
            lblSong3.BackColor = Color.Black;
            lblSong3.ForeColor = Color.White;
            lblSong4.BackColor = Color.Black;
            lblSong4.ForeColor = Color.White;

            lblMovie1.BackColor = Color.Black;
            lblMovie1.ForeColor = Color.White;
            lblMovie2.BackColor = Color.Black;
            lblMovie2.ForeColor = Color.White;
            lblMovie3.BackColor = Color.Black;
            lblMovie3.ForeColor = Color.White;

            lblVersion.BackColor = Color.Black;
            lblVersion.ForeColor = Color.White;
        }

        private void button6_Click(object sender, EventArgs e)  //Boton crea un nuevo usuario
        {
            try
            {
                if (txtPasswordNewUser.Text == txtConfPss.Text)
                {
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\archivostxt\\private-UsPss.txt", true);
                    sw.WriteLine("\n" + txtNombreUsuarioNuevo.Text + "\n" + txtPasswordNewUser.Text);
                    sw.Close();
                    StreamWriter sw1 = new StreamWriter(Application.StartupPath + "\\archivostxt\\usuarios.txt", true);
                    sw1.WriteLine("\n" + "\n" + "Nombre: " + txtNombreUsuarioNuevo.Text + " Mail: " + txtMailUsuarioNuevo.Text);
                    sw1.Close();
                    lblSeCreoUsuario.Text = "Se creó exitosamente el usuario. Bienvenido";
                    txtMailUsuarioNuevo.Clear();
                    txtNombreUsuarioNuevo.Clear();
                    txtPasswordNewUser.Clear();
                    txtConfPss.Clear();
                }
                else
                {
                    lblSeCreoUsuario.Text = "Las contraseñas no son iguales!";
                }
            }
            catch
            {
                lblNoSePudoCrearUsuario.Text = "No se pudo crear el usuario";
            }

        }

        private void button3_Click_2(object sender, EventArgs e)  //Boton Sign In
        {
            panelCrearUsuario.Visible = true;
            panelCrearUsuario.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)  //Boton Salir de Sign In panel
        {
            panelCrearUsuario.Visible = false;
            panelCrearUsuario.SendToBack();
        }

        private void btnExitApp_Click(object sender, EventArgs e)  //Cerrar App
        {
            this.Close();
        }
    }
}
