using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace E3POO
{
    public partial class Admin : Form   //     []   \
    {
        string[] listado_Artistas;
        string[] listado_Actores;
        string[] listado_Usuarios;
        string[] listado_Canciones;
        string[] listado_Playlists;
        string[] listado_Peliculas;
        public Admin()
        {
            InitializeComponent();

            //Agrega Artistas a lstArtistas
            string listadoArtistas = Properties.Resources.artistas.ToString();
            listado_Artistas = listadoArtistas.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var artista in listado_Artistas)
            {
                lstArtistas.Items.Add(artista);
            }

            //Agrega Actores a lstVerActores
            string listadoActores = Properties.Resources.actores.ToString();
            listado_Actores = listadoActores.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var actor in listado_Actores)
            {
                lstVerActores.Items.Add(actor);
            }

            //Agrega usuario a lstUsuarios
            string listadoUsuarios = Properties.Resources.usuarios.ToString();
            listado_Usuarios = listadoUsuarios.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var usuario in listado_Usuarios)
            {
                lstUsuarios.Items.Add(usuario);
            }

            //Agrega Cancion a lstCanciones
            string listadoCanciones = Properties.Resources.canciones.ToString();
            listado_Canciones = listadoCanciones.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var cancion in listado_Canciones)
            {
                lstCanciones.Items.Add(cancion);
            }

            //Agrega Playlist a lstVerPlaylists
            string listadoPlaylists = Properties.Resources.playlists.ToString();
            listado_Playlists = listadoPlaylists.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var playlist in listado_Playlists)
            {
                lstVerPlaylists.Items.Add(playlist);
            }

            //Agrega Pelicula a lstPeliculas
            string listadoPeliculas = Properties.Resources.peliculas.ToString();
            listado_Peliculas = listadoPeliculas.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pelicula in listado_Peliculas)
            {
                lstPeliculas.Items.Add(pelicula);
            }
        }

        private void btnExitfromAdm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private void txtBoxNUser_TextChanged(object sender, EventArgs e)
        {
            controlBotones();
        }

        private void button1_Click(object sender, EventArgs e)  //Boton Ingresar
        {
            if (txtBoxNUser.Text == "Admin" && Password.Text == "1234")
            {
                panelMenuAdmin.Visible = true;

            }
            else
            {
                lblErrorLogIn.Text = "Usuario/contraseña incorrectos";
            }
        }
        private void controlBotones()
        {
            if (txtBoxNUser.Text.Trim() != string.Empty && txtBoxNUser.Text.All(Char.IsLetter)) //&& Password.Text.Trim() != string.Empty
            {
                button1.Enabled = true;
                errorProvider1.SetError(txtBoxNUser, "");
            }
            else
            {
                if (!(txtBoxNUser.Text.All(Char.IsLetter)))
                {
                    errorProvider1.SetError(txtBoxNUser, "Solo debe contener letras");
                }
                //if(Password.Text==string.Empty)
                //{
                //    errorProvider2.SetError(Password, "Ingrese una contraseña");
                //}
                else
                {
                    errorProvider1.SetError(txtBoxNUser, "Ingrese su nombre de usuario");
                }
                button1.Enabled = false;
                //txtBoxNUser.Focus();
                Password.Focus();
            }
        }

        private void btnSalirAdm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVerCanciones_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = true;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }

        private void btnSalirPanelVerCanciones_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }

        private void btnVerPlaylists_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = true;
            panelVerUsuarios.Visible = false;
        }

        private void btnVerPeliculas_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = true;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }

        private void btnVerArtistas_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = true;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }

        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = true;
        }

        private void btnVerActores_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = true;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }

        private void btnVolverVerArtistas_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }

        private void btnVolverdeVerArtistas_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }




        //Artistas
        private void btnEliminarArtista_Click(object sender, EventArgs e)  //Faltan arreglos
        {
            lstArtistas.Items.Remove(lstArtistas.SelectedItem);
            //StreamReader sr = new StreamReader(@"C:\Users\Matias Rojas\source\repos\E3POO\E3POO\Resources\aaa.txt", true);
            //sr.Close();
            //StreamWriter escribir = new StreamWriter(@"C:\Users\Matias Rojas\source\repos\E3POO\E3POO\Resources\artistas.txt", true);
            //escribir.WriteLine("Se eliminará a: ");
            //escribir.Close();
            
        }

        private void btnCargarArtista1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\archivostxt\\artistas.txt", true);
            sw.WriteLine("\n"+ "\n" + "Nombre: "+txtNombreArtista.Text+ "Fecha de Nacimiento: "+txtDateArtista.Text);
            sw.Close();
            lstArtistas.Items.Add("Nombre: " + txtNombreArtista.Text + "Fecha de Nacimiento: " + txtDateArtista.Text);
            txtNombreArtista.Clear();
            txtDateArtista.Clear();
        }


        //Actores
        private void btnCargarArtista_Click(object sender, EventArgs e) //Cargar Actor
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\archivostxt\\actores.txt", true);
            sw.WriteLine("\n" + "\n" + "Nombre: " +txtNombreActor.Text+" Fecha de Nacimiento: "+txtDateActor.Text);
            sw.Close();
            lstVerActores.Items.Add("Nombre: " + txtNombreActor.Text + " Fecha de Nacimiento: " + txtDateActor.Text);
            txtNombreActor.Clear();
            txtDateActor.Clear();
        }

        private void btnEliminarArtistas_Click(object sender, EventArgs e) //Faltan Arreglos
        {
            lstArtistas.Items.Remove(lstArtistas.SelectedItem);
        }

        //Usuarios
        private void btnCargarUsuario_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\archivostxt\\usuarios.txt", true);
            sw.WriteLine("\n" + "\n"+"Nombre: " +txtNombreUsuario.Text+" Mail: "+txtMailUsuario.Text);
            sw.Close();
            lstUsuarios.Items.Add("Nombre: " + txtNombreUsuario.Text + " Mail: " + txtMailUsuario.Text);
            txtNombreUsuario.Clear();
            txtMailUsuario.Clear();
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e) //Faltan arreglos
        {
            lstUsuarios.Items.Remove(lstUsuarios.SelectedItem);
        }

        //Cancion
        private void button2_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\archivostxt\\canciones.txt", true);
            sw.WriteLine("\n" +"\n"+ "Nombre: " +txtNombreCancion.Text+ " Album: " + txtAlbumCancion.Text+" Artista: "+txtArtistaCancion.Text+" Fecha de Lanzamiento: "+txtDateCancion.Text);
            sw.Close();
            StreamWriter sw1 = new StreamWriter(Application.StartupPath + "\\archivostxt\\NombreCancion.txt", true);
            sw1.WriteLine(txtNombreCancion.Text+ " - " + txtArtistaCancion.Text);
            sw1.Close();
            lstCanciones.Items.Add("Nombre: " + txtNombreCancion.Text + " Album: " + txtAlbumCancion.Text + " Artista: " + txtArtistaCancion.Text + " Fecha de Lanzamiento: " + txtDateCancion.Text);
            
            txtNombreCancion.Clear();
            txtAlbumCancion.Clear();
            txtArtistaCancion.Clear();
            txtDateCancion.Clear();
        }

        //Playlists
        private void btnCargarPlaylist_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\archivostxt\\playlists.txt", true);//fdlkjfldjgldfkjgfdlkgjfdlkgjflkgjflkgjfdlkgjflkjgfdlkjgdflkjg
            sw.WriteLine("\n" + "\n" + "Nombre: " +txtNombrePlaylist.Text+ " Canciones: "+txtCancionesInPlaylist.Text);
            sw.Close();
            lstCanciones.Items.Add(txtNombreCancion.Text);
            txtNombrePlaylist.Clear();
            txtCancionesInPlaylist.Clear();
        }

        private void btnEliminarPlaylist_Click(object sender, EventArgs e) //Faltan arreglos
        {
            lstVerPlaylists.Items.Remove(lstVerPlaylists.SelectedItem);
        }

        private void btnVolverdeVerUsuarios_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }

        private void btnVolverdeVerPlaylists_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }

        //Peliculas
        private void btnCargarPelicula_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\archivostxt\\peliculas.txt", true);
            sw.WriteLine("\n" + "\n" + "Nombre: " +txtNombrePelicula.Text+" Actor Principal: "+txtActorPelicula.Text+" Fecha Lanzamiento: "+txtDatePelicula.Text);
            sw.Close();
            StreamWriter sw1 = new StreamWriter(Application.StartupPath + "\\archivostxt\\NombrePeliculas.txt", true);
            sw1.WriteLine(txtNombrePelicula.Text);
            sw1.Close();
            lstPeliculas.Items.Add("Nombre: " + txtNombrePelicula.Text + " Actor Principal: " + txtActorPelicula.Text + " Fecha Lanzamiento: " + txtDatePelicula.Text);
            txtNombrePelicula.Clear();
            txtActorPelicula.Clear();
            txtDatePelicula.Clear();
        }

        private void btnEliminarPelicula_Click(object sender, EventArgs e)
        {
            lstPeliculas.Items.Remove(lstPeliculas.SelectedItem);
        }

        private void btnVolverdeVerPeliculas_Click(object sender, EventArgs e)
        {
            panelVerCanciones.Visible = false;
            panelVerActores.Visible = false;
            panelVerArtistas.Visible = false;
            panelVerPeliculas.Visible = false;
            panelVerPlaylists.Visible = false;
            panelVerUsuarios.Visible = false;
        }

        private void txtRutaCancion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
