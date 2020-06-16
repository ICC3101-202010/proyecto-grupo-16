using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace E3POO
{
    public partial class Movies : Form
    {
        string[] Peliculas_Disponibles;
        public Movies()
        {
            InitializeComponent();
            StreamReader sw = new StreamReader(Application.StartupPath + "\\archivostxt\\NombrePeliculas.txt", true);
            Peliculas_Disponibles = sw.ToString().Split(new[] { "\n\n" }, StringSplitOptions.None);
            for (int i = 0; i <= Peliculas_Disponibles.Length + 100; i++)
            {
                try
                { cbPeliculas.Items.Add(sw.ReadLine()); }
                catch
                { }
            }
            sw.Close();
            
        }

        private void btnCargarMovie_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = Application.StartupPath + "\\archivostxt\\Movies\\" + cbPeliculas.SelectedItem+".mp4";

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Movies_Load(object sender, EventArgs e)
        {
        }
    }
}
