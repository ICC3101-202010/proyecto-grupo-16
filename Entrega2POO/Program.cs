using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProyectodeCurso
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string nombreplaylistcancion;
            bool privacidadcancion;
            string privacidadcancion1;
            string tipo;
            string respuesta;
            string nombreplaylistbuscada;
            string nombrecancionbuscada;
            string keyword;

            while (true)
            {
                APP app = new APP();
                Profile profile = new Profile("profile");
                //Menu de usuario
                Console.WriteLine("Menú: Elija una opcion numerica");
                Console.WriteLine(" ");
                Console.WriteLine("1) Crear Playlist");
                Console.WriteLine(" ");
                Console.WriteLine("2) Ver Playlists");
                Console.WriteLine(" ");
                Console.WriteLine("3) Añadir Canción a Playlist");
                Console.WriteLine(" ");
                Console.WriteLine("4) Buscar Canción");
                Console.WriteLine(" ");
                Console.WriteLine("5) Buscar Canción dentro de una Playlist");
                Console.WriteLine(" ");
                Console.WriteLine("6) Eliminar una cancion de la playlist");
                Console.WriteLine(" ");
                Console.WriteLine("7) Eliminar una playlist");
                Console.WriteLine(" ");
                Console.WriteLine("8) Salir de Spotify");
                Console.WriteLine(" ");
                Console.WriteLine(" ");

                respuesta = Console.ReadLine();
                if (respuesta == "1")
                {
                    //Creacion de la playlist
                    Console.WriteLine("Nombre de la playlist");
                    nombreplaylistcancion = Console.ReadLine();
                    Console.WriteLine("Privacidad:");
                    privacidadcancion1 = Console.ReadLine();
                    if (privacidadcancion1=="Privada")
                    {
                        privacidadcancion = true;
                    }
                    else //(privacidadcancion1=="Publica")
                    {
                        privacidadcancion = false;
                    }
                    Console.WriteLine("Tipo de Playlist");
                    tipo = Console.ReadLine();
                    //Console.ReadLine() le damos valores a los atributos
                    PlaylistS Playlist = new PlaylistS(profile.UserName1, nombreplaylistcancion, privacidadcancion, tipo);
                }
                if (respuesta=="2")
                {
                    Console.WriteLine("Ingrese el nombre de la playlist:");
                    nombreplaylistbuscada = Console.ReadLine();
                    app.SearchPlaylistS(nombreplaylistbuscada);
                }
                if (respuesta=="3") //Añadir cancion
                {
                    Console.WriteLine("Ingrese la playlist");
                    nombreplaylistbuscada = Console.ReadLine();
                    //foreach()
                    //Buscar una cancion "cancion" en una playlist "playlist1"
                    //playlists1.SongSearchinPlaylist(cancion);
                }

                if (respuesta=="4")
                {
                    Console.WriteLine("Ingrese el nombre de la cancion:");
                    nombrecancionbuscada = Console.ReadLine();
                    //app.SearchSong(Song); ----> Arreglar, Metodo que busque el nombre de una cancion para agregarla
                    
                }
                if (respuesta=="5") //Buscar una cancion "cancion" en una playlist "playlist1"
                {
                    //playlists1.SongSearchinPlaylist(cancion);

                }

                if (respuesta=="6")//Eliminar una cancion "song" de la "playlist1"
                {
                    //playlist1.EliminateSong(song)
                }
                if (respuesta=="7")
                {
                    Console.WriteLine("Ingrese el nombre de la playlist");
                    nombreplaylistbuscada = Console.ReadLine();
                    //app.EliminatePlaylist(nombreplaylistbuscada-->playlist)
                }
                if (respuesta=="8")
                {
                    Console.WriteLine("Saliendo del menú");
                    break
                }






            }
        }
    }
}
