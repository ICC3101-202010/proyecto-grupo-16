using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Proyecto_APP_SpotiNetflix
{
    class Program
    {
       
        static void Main(string[] args)
        {

           

           

            string LoginOption="0";

            APP app = new APP();

            //Con el email AdminSpotiflix y la constraseña 123 se puede ingresar al menu del Administrador.
            Profile StartAdminUSER = new Profile("AdminSpotiflix","123","ADMIN","",true,true);


            app.AddProfile(StartAdminUSER);

            //Carga la BDD de los usuarios creados (si es que no han sido agregado nada , crea la data vacia)
            app.LoadUsers();
            app.LoadMovies();
            app.LoadWorkersM();
            app.LoadSongs();
            app.LoadSinger();

            //Graba el admin user , el cual esta almacendado en DataBaseUsers.
            app.SaveUsers();

            





            while (LoginOption != "3")
            {
                Console.WriteLine("--------------------------------------------------------------------\n");
                Console.WriteLine("Bienvenido a ESPOTI-NETFLIX\n");
                Console.WriteLine("--------------------------------------------------------------------\n");
                Console.WriteLine("Ingrese una de las siguientes opciones para continuar\n");
                Console.WriteLine("1. Iniciar sesion\n");
                Console.WriteLine("2. ¿No tienes cuenta? presiona 2 para suscribirte a Spoti-Netflix! \n");
                Console.WriteLine("3. Salir del programa\n");

                LoginOption = Console.ReadLine();

                //Filtro de opcion ingresada invalida del usuario.

                if (LoginOption != "1" && LoginOption != "2" && LoginOption != "3")
                {
                    Console.WriteLine("ERROR: Ingrese una opcion valida para continuar!\n");
                }
                //Opcion valida!
                else
                {
                    if (LoginOption == "1")
                    {
                        Console.WriteLine("--------------------------------------------------------------------\n");
                        Console.WriteLine("ESPOTI-NETFLIX\n"); 
                        Console.WriteLine("MENU INICIO SESIÓN\n");
                        Console.WriteLine("--------------------------------------------------------------------\n");

                        Console.WriteLine("Email:");
                        string Email = Console.ReadLine();

                        Console.WriteLine("\nPassword:");
                        string Password = Console.ReadLine();


                        //Metodo que verifica que los parametros de inicio de sesion sean correctos 
                        //El logeo puede ser incorrecto si es que el usuario ya existe , o los parametros no concuerdan
                        //Con ninguna cuenta.

                        int VerifyLogin = app.VerifyLogin(Email,Password);

                        //Inicio de sesion valida , para usurios normales
                        if (VerifyLogin == 1)
                        {
                            Console.WriteLine("\nSESIÓN INICIADA CORRECTAMENTE !\n");

                            var profile = app.ReturnUser(Email, Password);

                          

                            //Menu de administrador
                            if (app.VerifyAdminUser(Email)==1)
                            {


                                string AdminOption = "0";

                                while (AdminOption!="4")
                                {
                                    Console.WriteLine("--------------------------------------------------------------------\n");
                                    Console.WriteLine("ESPOTI-NETFLIX\n");
                                    Console.WriteLine("[ADMIN] MENU\n");
                                    Console.WriteLine("--------------------------------------------------------------------\n");

                                    Console.WriteLine("Ingrese una de las siguientes opciones para continuar\n");
                                    Console.WriteLine("1. Agregar canciones\n");
                                    Console.WriteLine("2. Agregar peliculas\n");
                                    Console.WriteLine("3. Volver al menu de inicio de sesion/creacion sesion\n");
                                    Console.WriteLine("4. Salir del programa\n");

                                    AdminOption = Console.ReadLine();
                                    //Caso en que el usuario se equivoca en ingresar un parametro correcto
                                    if (AdminOption != "1" && AdminOption != "2" && AdminOption != "3" && AdminOption != "4")
                                    {
                                        Console.WriteLine("ERROR: Ingrese una opcion valida para continuar!\n");
                                    }
                                    //Opcion de agregado de canciones
                                    else if (AdminOption == "1")
                                    {
                                        int SongsContinueAdding = 1;
                                        while (SongsContinueAdding == 1)
                                        {
                                            string OpcionAdminSongADD;

                                            Console.WriteLine("--------------------------------------------------------------------\n");
                                            Console.WriteLine("ESPOTI-NETFLIX\n");
                                            Console.WriteLine("[ADMIN] Agregado de canciones\n");
                                            Console.WriteLine("--------------------------------------------------------------------\n");

                                            Console.WriteLine("[-*Cancion*-]\nNombre:");
                                            string name_song = Console.ReadLine();
                                            Console.WriteLine("\nAlbum:");
                                            string Album = Console.ReadLine();
                                            Console.WriteLine("\nGenero:");
                                            string SongGenre = Console.ReadLine();
                                            Console.WriteLine("\nCantante o Nombre de Banda:");
                                            string Singer = Console.ReadLine();
                                            Console.WriteLine("\nEdad del cantante:");
                                            string Edads = Console.ReadLine();

                                            int Edad=0;
                                            //Tome que solo es un cantante
                                            try
                                            {
                                                Edad = Int32.Parse(Edads);
                                            }
                                            catch(Exception)
                                            {
                                                Console.WriteLine("Error: Porfavor Ingrese un numero correcto!.");
                                                
                                                break;
                                            }
                                            Console.WriteLine("\nSexo del cantante (Ingrese H si es hombre y M si es mujer):");
                                            string Sexo = Console.ReadLine();
                                            while(Sexo!="H" && Sexo!="M")
                                            {
                                                Console.WriteLine("Error: Sexo ingresado ");
                                                Console.WriteLine("\nSexo del cantante:");
                                                Sexo = Console.ReadLine();
                                                if(Sexo=="H" || Sexo =="M")
                                                {
                                                    break;
                                                }
                                            }

                                            Console.WriteLine("\nComposer:");
                                            string Composer = Console.ReadLine();
                                            Console.WriteLine("\nAño de publicación:");
                                            string YearPublish = Console.ReadLine();
                                            Console.WriteLine("\nLyrics:");
                                            string Lyrics = Console.ReadLine();

                                            // Por el momento estoy asumiendo que el ayudante va a agregar los archivos de canciones a la carpeta
                                            //Debug
                                            Console.WriteLine("Ingrese el nombre del archivo, incluyendo el formato (mp3,wav,etc)\nFileName:");
                                            string FileNameS = Console.ReadLine();
                                            
                                            Console.WriteLine("\n");



                                            //VerifySong devuelve 1 si es que la cancion ya esta en la BDD de canciones y 0 si es que no
                                            int SongChecker = app.VerifySong(name_song, Singer, Album, Composer);
                                            //Chekea si es que el archivo existe (arroja true si existe)
                                            bool FileChecker = app.VerifyFilePath(FileNameS);

                                            if (SongChecker == 0 && FileChecker == true)
                                            {

                                                float FileSize = app.GetFileSize(FileNameS);
                                                
                                                

                                                //Asumimos que la calidad de la musica tiene como estandar 320 kps (Las canciones de spotify suelen estar en esta calidad)
                                                //Por lo cual la duracion en minutos de la cancion se calcularia con el peso en MB/2.4MB = []minutos no va ser para nada exacto pero servira
                                                //para esta entrega.




                                                while (true)
                                                {
                                                    Console.WriteLine("¿Desea agregar una imagen al album de la cancion?(Si o No)\n");
                                                    string OptionAddImageSong = Console.ReadLine();

                                                    if (OptionAddImageSong != "Si" && OptionAddImageSong != "No")
                                                    {
                                                        Console.WriteLine("Porfavor ingrese un parametro c/r a la pregunta\n");
                                                    }
                                                    else
                                                    {
                                                        if (OptionAddImageSong == "Si")
                                                        {
                                                            Console.WriteLine("Ingrese el nombre del archivo de imagen\n");
                                                            string FileNameImageS = Console.ReadLine();

                                                            bool VerifyFileName = app.VerifyFilePath(FileNameImageS);

                                                            if (VerifyFileName == true)
                                                            {
                                                                double Song_size = app.GetFileSize(FileNameS);
                                                                //Por el momento agregaremos la imagen como la direccion en string de la ubicacion de la imagen.
                                                                Songs song = new Songs(name_song, Album, SongGenre, Composer, Singer, YearPublish, Lyrics, app.GetFileType(FileNameS), Song_size, Song_size / 2.4, FileNameImageS,"320");
                                                                Singer singer = new Singer(Singer, Singer, 0, Sexo, Edad);
                                                                app.AddSong(song);
                                                                app.AddSinger(singer);
                                                                singer.AddSongsSinger(song);

                                                                app.SaveSongs();
                                                                app.SaveSinger();
                                                                

                                                                Console.WriteLine("Canción agregada con exito!\n");
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("ERROR: El nombre del archivo es erroneo o la ubicación del archivo no esta en la carpeta Debug \n");
                                                            }

                                                        }

                                                        else if (OptionAddImageSong == "No")
                                                        {
                                                            double Song_size = app.GetFileSize(FileNameS);
                                                            //Si es que no se agrega imagen, se pone un string vacio en FileNameImage
                                                            Songs song = new Songs(name_song, Album, SongGenre, Composer, Singer, YearPublish, Lyrics, app.GetFileType(FileNameS), Song_size, Song_size / 2.4, "","320");
                                                            Singer singer = new Singer(Singer, Singer, 0, Sexo, Edad);

                                                            app.AddSong(song);
                                                            singer.AddSongsSinger(song);

                                                            app.AddSinger(singer);

                                                            app.SaveSongs();
                                                            app.SaveSinger();
                                                            
                                                            Console.WriteLine("Canción agregada con exito\n");
                                                            break;
                                                        }

                                                    }


                                                }

                                                while (true)
                                                {
                                                    Console.WriteLine("Ingrese entre una de las siguientes opciones\n");
                                                    Console.WriteLine("1. Volver al menu de [ADMIN]\n");
                                                    Console.WriteLine("2. Volver al menu de [ADMIN] agregado de canciones\n");
                                                    Console.WriteLine("3. Volver al menu de Incio de sesion / Creacion de usarios\n");
                                                    Console.WriteLine("4. Salir del programa\n");


                                                    OpcionAdminSongADD = Console.ReadLine();

                                                    if (OpcionAdminSongADD != "1" && OpcionAdminSongADD != "2" && OpcionAdminSongADD != "3" && OpcionAdminSongADD != "4")
                                                    {
                                                        Console.WriteLine("ERROR: Porfavor ingrese una opcion valida del menu\n");

                                                    }
                                                    else
                                                    {
                                                        if (OpcionAdminSongADD == "1")
                                                        {
                                                            SongsContinueAdding = 0;
                                                            break;
                                                        }
                                                        else if (OpcionAdminSongADD == "2")
                                                        {
                                                            SongsContinueAdding = 1;
                                                            break;
                                                        }
                                                        else if (OpcionAdminSongADD == "3")
                                                        {
                                                            SongsContinueAdding = 0;
                                                            AdminOption = "4";
                                                            break;
                                                        }
                                                        else if (OpcionAdminSongADD == "4")
                                                        {
                                                            LoginOption = "3";
                                                            AdminOption = "4";
                                                            SongsContinueAdding = 0;
                                                            break;
                                                        }

                                                    }
                                                }


                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR: Estos parametros de cancion ya existen en la BDD de canciones o el nombre del archivo no exite!\n");
                                                
                                                while (true)
                                                {
                                                    Console.WriteLine("Ingrese entre una de las siguientes opciones\n");
                                                    Console.WriteLine("1. Volver al menu de [ADMIN]\n");
                                                    Console.WriteLine("2. Volver al menu de [ADMIN] agregado de canciones\n");
                                                    Console.WriteLine("3. Volver al menu de Incio de sesion / Creacion de usarios\n");
                                                    Console.WriteLine("4. Salir del programa\n");


                                                    OpcionAdminSongADD = Console.ReadLine();

                                                    if (OpcionAdminSongADD != "1" && OpcionAdminSongADD != "2" && OpcionAdminSongADD != "3" && OpcionAdminSongADD != "4")
                                                    {
                                                        Console.WriteLine("ERROR: Porfavor ingrese una opcion valida del menu\n");

                                                    }
                                                    else
                                                    {
                                                        if (OpcionAdminSongADD == "1")
                                                        {
                                                            SongsContinueAdding = 0;
                                                            break;
                                                        }
                                                        else if (OpcionAdminSongADD == "2")
                                                        {
                                                            SongsContinueAdding = 1;
                                                            break;
                                                        }
                                                        else if (OpcionAdminSongADD == "3")
                                                        {
                                                            AdminOption = "4";
                                                            SongsContinueAdding = 0;
                                                            break;
                                                        }
                                                        else if (OpcionAdminSongADD == "4")
                                                        {
                                                            SongsContinueAdding = 0;
                                                            LoginOption = "3";
                                                            AdminOption = "4";
                                                            break;
                                                        }

                                                    }
                                                }


                                            }
                                        }

                                    }
                                    //Opcion de agregado de pelicula
                                    else if (AdminOption == "2")
                                    {
                                        int MoviesContinueAdding = 1;
                                        while (MoviesContinueAdding == 1)
                                        {
                                            string OpcionAdminMovieADD;

                                            Console.WriteLine("--------------------------------------------------------------------\n");
                                            Console.WriteLine("ESPOTI-NETFLIX\n");
                                            Console.WriteLine("[ADMIN] Agregado de peliculas\n");
                                            Console.WriteLine("--------------------------------------------------------------------\n");

                                            Console.WriteLine("[-*Pelicula*-]\nTitulo:");
                                            string Title = Console.ReadLine();
                                            Console.WriteLine("\nCategoria:");
                                            string Category = Console.ReadLine();

                                            Console.WriteLine("\nAño de publicación");
                                            string YearPublish = Console.ReadLine();
                                            Console.WriteLine("\nDescripcion:");
                                            string Description = Console.ReadLine();
                                            Console.WriteLine("\nEstudio");
                                            string Studio = Console.ReadLine();

                                            Console.WriteLine("Ingrese el nombre del archivo de la pelicula, y luego del nombre de archivo agregue un .TypeFile\nFileNameMovie:");
                                            string FileNameM = Console.ReadLine();

                                            Console.WriteLine("\nA continuación ingrese el elenco para la pelicula \n");
                                            string ContinueAddingWorkers = "1";
                                            List<WorkerMovie> ListWorkers = new List<WorkerMovie>();
                                            while (ContinueAddingWorkers=="1")
                                            {
                                                

                                                Console.WriteLine("\nIngrese el Rol del trabajador:");
                                                string RolActor = Console.ReadLine();

                                                //Falta filtrar un edad menor a una cierta edad limite 
                                                int Age = 0;
                                                while (true)
                                                {
                                                    
                                                    string ErrorMessage="";
                                                    try
                                                    {
                                                        Console.WriteLine("\nEdad:");
                                                        string Ages = Console.ReadLine();
                                                        Age = Int32.Parse(Ages);
                                                    }
                                                    catch (Exception)
                                                    {
                                                        ErrorMessage = "Error: Ingrese un numero correcto!\n";
                                                        Console.WriteLine(ErrorMessage);
                                                    }
                                                    if(ErrorMessage=="")
                                                    {
                                                        break;
                                                    }
                                                }
                                                string Sexo;
                                                while (true)
                                                {
                                                    Console.WriteLine("\nSexo (Ingrese H o M si es hombre o mujer c/r):");
                                                    Sexo = Console.ReadLine();
                                                    if (Sexo != "H" && Sexo != "M")
                                                    {
                                                        Console.WriteLine("ERROR: Ingrese un parametro correcto para el genero de la persona\n");
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }

                                                }
                                                Console.WriteLine("\nNombre:");
                                                string Nombre = Console.ReadLine();

                                                while(true)
                                                {
                                                    Console.WriteLine("¿Quiere seguir agregando mas actores a la pelicula (Ingrese 1 o 0 si quiere continuar o no respectivamente)?\n");
                                                    ContinueAddingWorkers = Console.ReadLine();
                                                    if(ContinueAddingWorkers!="1" && ContinueAddingWorkers!="0")
                                                    {
                                                        Console.WriteLine("Error: Ingrese una opcion del menu\n");
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                                

                                                WorkerMovie workerMovie = new WorkerMovie(Nombre,Age,Sexo,RolActor,0);

                                                ListWorkers.Add(workerMovie);


                                            }
                                                
                                            

                                            int MovieChecker = app.MovieCheker(Title,YearPublish);
                                            bool VerifyFileNameM = app.VerifyFilePath(FileNameM);
                                            
                                            //Se puede agregar a la BDD de movies
                                            if (MovieChecker==0 && VerifyFileNameM==true)
                                            {
                                                while(true)
                                                {   
                                                    Console.WriteLine("¿Desea agregar una imagen a la pelicula?(Si o No)\n");
                                                    string OptionAddImageMovie = Console.ReadLine();

                                                    if (OptionAddImageMovie != "Si" && OptionAddImageMovie != "No")
                                                    {
                                                        Console.WriteLine("Porfavor ingrese un parametro c/r a la pregunta\n");
                                                    }
                                                    else
                                                    {
                                                        if (OptionAddImageMovie == "Si")
                                                        {
                                                            Console.WriteLine("Ingrese el nombre del archivo de imagen\n");
                                                            string FileNameImageM = Console.ReadLine();

                                                            bool VerifyFileName = app.VerifyFilePath(FileNameImageM);

                                                            if (VerifyFileName == true)
                                                            {
                                                                double movie_size = app.GetFileSize(FileNameM);
                                                                //Por el momento agregaremos la imagen como la direccion en string de la ubicacion de la imagen.
                                                                Movies movie = new Movies(Title, Category, movie_size/256, YearPublish, Description, Studio, app.GetFileType(FileNameM), movie_size, FileNameImageM, ListWorkers);

                                                                app.AddWokerM(ListWorkers);
                                                                app.AddMovie(movie);
                                                                app.SaveMovies();
                                                                app.SaveWorkersM();

                                                                Console.WriteLine("Pelicula agregada con exito!\n");
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("ERROR: El nombre del archivo es erroneo o la ubicación del archivo no esta en la carpeta Debug \n");
                                                            }

                                                        }

                                                        else if (OptionAddImageMovie == "No")
                                                        {
                                                            double movie_size = app.GetFileSize(FileNameM);
                                                            //Si es que no se agrega imagen, se pone un string vacio en FileNameImage
                                                            Movies movie = new Movies(Title, Category, movie_size/256, YearPublish, Description, Studio, app.GetFileType(FileNameM), movie_size, "",ListWorkers);

                                                            app.AddMovie(movie);
                                                            app.AddWokerM(ListWorkers);

                                                            app.SaveMovies();
                                                            app.SaveWorkersM();

                                                            Console.WriteLine("Pelicula agregada con exito\n");
                                                            break;
                                                        }

                                                    }
                                                }
                                                

                                                

                                                while (true)
                                                {
                                                    Console.WriteLine("Ingrese entre una de las siguientes opciones\n");
                                                    Console.WriteLine("1. Volver al menu de [ADMIN]\n");
                                                    Console.WriteLine("2. Volver al menu de [ADMIN] agregado de peliculas\n");
                                                    Console.WriteLine("3. Volver al menu de Incio de sesion / Creacion de usarios\n");
                                                    Console.WriteLine("4. Salir del programa\n");


                                                    OpcionAdminMovieADD = Console.ReadLine();

                                                    if (OpcionAdminMovieADD != "1" && OpcionAdminMovieADD != "2" && OpcionAdminMovieADD != "3" && OpcionAdminMovieADD != "4")
                                                    {
                                                        Console.WriteLine("ERROR: Porfavor ingrese una opcion valida del menu\n");

                                                    }
                                                    else
                                                    {
                                                        if (OpcionAdminMovieADD == "1")
                                                        {
                                                            MoviesContinueAdding = 0;
                                                            break;
                                                        }
                                                        else if (OpcionAdminMovieADD == "2")
                                                        {
                                                            MoviesContinueAdding = 1;
                                                            break;
                                                        }
                                                        else if (OpcionAdminMovieADD == "3")
                                                        {
                                                            MoviesContinueAdding = 0;
                                                            AdminOption = "4";
                                                            break;
                                                        }
                                                        else if (OpcionAdminMovieADD == "4")
                                                        {
                                                            LoginOption = "3";
                                                            AdminOption = "4";
                                                            MoviesContinueAdding = 0;
                                                            break;
                                                        }

                                                    }
                                                }




                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR: Esta pelicula ya ha sido agregada o no se encuentra el archivo en Debug\n");
                                                while (true)
                                                {
                                                    Console.WriteLine("Ingrese entre una de las siguientes opciones\n");
                                                    Console.WriteLine("1. Volver al menu de [ADMIN]\n");
                                                    Console.WriteLine("2. Volver al menu de [ADMIN] agregado de peliculas\n");
                                                    Console.WriteLine("3. Volver al menu de Incio de sesion / Creacion de usarios\n");
                                                    Console.WriteLine("4. Salir del programa\n");


                                                    OpcionAdminMovieADD = Console.ReadLine();

                                                    if (OpcionAdminMovieADD != "1" && OpcionAdminMovieADD != "2" && OpcionAdminMovieADD != "3" && OpcionAdminMovieADD != "4")
                                                    {
                                                        Console.WriteLine("ERROR: Porfavor ingrese una opcion valida del menu\n");

                                                    }
                                                    else
                                                    {
                                                        if (OpcionAdminMovieADD == "1")
                                                        {
                                                            MoviesContinueAdding = 0;
                                                            break;
                                                        }
                                                        else if (OpcionAdminMovieADD == "2")
                                                        {
                                                            MoviesContinueAdding = 1;
                                                            break;
                                                        }
                                                        else if (OpcionAdminMovieADD == "3")
                                                        {
                                                            MoviesContinueAdding = 0;
                                                            AdminOption = "4";
                                                            break;
                                                        }
                                                        else if (OpcionAdminMovieADD == "4")
                                                        {
                                                            LoginOption = "3";
                                                            AdminOption = "4";
                                                            MoviesContinueAdding = 0;
                                                            break;
                                                        }

                                                    }
                                                }
                                            }




                                        }
                                    }
                                    //Volver al menu de inicio de sesion/creacion sesion
                                    else if (AdminOption == "3")
                                    {
                                        LoginOption = "0";
                                        break;
                                    }
                                    //Salida del programa
                                    else if (AdminOption == "4")
                                    {
                                        LoginOption = "3";
                                        AdminOption = "4";
                                        break;
                                    }
                                }
                                
                            }
                            //Menu de usuarios no administradores
                            //LUGAR DONDE VA ESTAR LOS MENU de las acciones de los usuarios!
                            else
                            {
                                Console.WriteLine("--------------------------------------------------------------------\n");
                                Console.WriteLine("ESPOTI-NETFLIX\n");
                                Console.WriteLine("--------------------------------------------------------------------\n");

                                while (true)
                                {
                                    

                                    //Menu de usuario (profile)
                                    Console.WriteLine("Menú: Elija una opcion numerica");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("1) Crear Playlist");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("2) Ver Playlist");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("3) Añadir Canción a Playlist");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("4) Buscar Canción");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("5) Buscar Canción dentro de una Playlist");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("6) Eliminar una cancion de la playlist");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("7) Eliminar una playlist de canciones");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("8) Añadir Pelicula a Playlist");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("9) Buscar Pelicula");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("10) Buscar Pelicula dentro de una Playlist");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("11) Elminar una pelicula de la playlist");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("12) Eliminar una Playlist de peliculas");
                                    Console.WriteLine(" ");
                                    Console.WriteLine("8) Búsqueda general"); //keyword
                                    Console.WriteLine(" ");
                                    Console.WriteLine("9) Salir de Spotify");
                                    Console.WriteLine(" ");
                                    Console.WriteLine(" ");

                                    string OptionMenuUser = Console.ReadLine();

                                    if(OptionMenuUser=="1")
                                    {
                                        Console.WriteLine("\nIngrese el nombre de la playlist");
                                        string NamePlaylist = Console.ReadLine();
                                        Console.WriteLine("\nIngrese el tipo de playlist (Ingrese 1 si es de canciones o 2 si es de peliculas)");
                                        string tipo = Console.ReadLine();

                                        while(true)
                                        {
                                            if(tipo!="1" && tipo!="2")
                                            {
                                                Console.WriteLine("ERROR: Porfavor ingrese un el parameto correcto\n");
                                                Console.WriteLine("\nTipo:");
                                                tipo = Console.ReadLine();
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        //Listas Canciones
                                        if (tipo == "1")
                                        {
                                            string FileType;

                                            while (true)
                                            {
                                                Console.WriteLine("Ingrese el tipo de archivo que va a permitir esta playlist (ingrese mp3 o wav)\n");
                                                FileType = Console.ReadLine();

                                                if (FileType != "mp3" && FileType != "wav")
                                                {
                                                    Console.WriteLine("Error: Porfavor ingrese un tipo valido\n");
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                                

                                            }
                                            PlaylistS playlistS = new PlaylistS(profile.Email1, NamePlaylist, profile.Privacy1, FileType);
                                            
                                            Console.WriteLine("Playlist creada con exito!\n");


                                        }
                                            
                                        //Listas Peliculas
                                        else
                                        {

                                        }

                                    }
                                    else if(OptionMenuUser=="3")
                                    {

                                    }
                                    else if (OptionMenuUser == "4")
                                    {

                                    }
                                    else if (OptionMenuUser == "5")
                                    {

                                    }
                                    else if (OptionMenuUser == "6")
                                    {

                                    }
                                    else if(OptionMenuUser=="7")
                                    {

                                    }
                                    else if (OptionMenuUser == "8")
                                    {

                                    }
                                    else if (OptionMenuUser == "9")
                                    {
                                        LoginOption = "3";
                                       
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR: Escojan una de las opciones del menu\n");
                                    }
                                    






                                }

                            }
                            

                            



                        }
                        
                        //Caso de contraseña erronea (email existente)
                        else
                        {
                            Console.WriteLine("ERROR: Nombre de usuario o contraseña incorrectos.\n");
                        }
                    }
                    else if (LoginOption == "2")
                    {   
                        //Menu de creacion de cuenta
                        Console.WriteLine("--------------------------------------------------------------------\n");
                        Console.WriteLine("ESPOTI-NETFLIX\n");
                        Console.WriteLine("--------------------------------------------------------------------\n");

                        //Cero significa que hay error de creacion de usuario
                        int ValidAccountCreation=0;

                        //Cero significa que aun no se va salir del menu de creacion
                        string ExitMenuCreation="0";

                        while (ValidAccountCreation==0 && ExitMenuCreation=="0")
                        {
                            string Email;
                            string Password;
                            string RePassword;
                            string UserName;
                            string BornDate;

                            Console.WriteLine("--------------------------------------------------------------------\n");
                            Console.WriteLine("MENU CREACIÓN USUARIO \n");
                            Console.WriteLine("--------------------------------------------------------------------\n");

                            Console.WriteLine("Email:");
                            Email=Console.ReadLine();
                            Console.WriteLine("\nContraseña:");
                            Password = Console.ReadLine();
                            Console.WriteLine("\nConfirmación de contraseña:");
                            RePassword = Console.ReadLine();

                            Console.WriteLine("--------------------------------------------------------------------\n");
                            Console.WriteLine("AVISO\n");
                            Console.WriteLine("Con su nombre de Usuario sera identificado por otros usuarios en la plataforma solo si es que usted clasifica su perfil como publico.\nLa configuración de privacidad puede ser cambiada a publica en configuración cuando usted vea conveniente (se establece como estandar de creacion de usuarios como privados!)\n)");

                            Console.WriteLine("--------------------------------------------------------------------\n");

                            Console.WriteLine("\nNombre Usuario:");
                            UserName = Console.ReadLine();

                            Console.WriteLine("\nFecha de nacimiento:");
                            BornDate = Console.ReadLine();

                            /*Verification_Error tiene un 0 si es que no encuentra error , un 1 si es que el email ya exite,
                            un 2 si que las contraseñas no coinciden y un 3 si es que el nombre de usuario ya existe!*/
                            int Verification_Error = app.VerifyAccountCreation(Email, Password, RePassword, UserName);


                            //Una creación de cuenta puede no ser valida si es que no coinciden las contraseña, el UserName ya existe
                            //o el email no existe o ya ha sido registrado en la aplicacion (se puede integrar la necesidad de confirmación de email )

                    
                            if (Verification_Error == 2)
                            {
                                Console.WriteLine("ERROR : Las contraseñas no coinciden!\n");
                                
                                //Esto sera repetido en los otros tipos de errores!
                                while(true)
                                {
                                    Console.WriteLine("Ingrese entre una de las siguientes opcines\n");
                                    Console.WriteLine("0. Vover al menu de creación de usuario\n");
                                    Console.WriteLine("1. Vover al menu de incio-creación de usuario\n");

                                    ExitMenuCreation = Console.ReadLine();


                                    //Mal ingresado de opcion de salida o vuelta
                                    if (ExitMenuCreation != "0" && ExitMenuCreation != "1")
                                    {
                                        Console.WriteLine("ERROR: Ingrese una opcion valida para continuar!\n");
                                    }
                                    //Correcta opcion de salida o vuelta
                                    else
                                    {
                                        //Vuelta al menu de creacion del usuario 
                                        if (ExitMenuCreation == "0")
                                        {
                                            ValidAccountCreation = 0;
                                            ExitMenuCreation = "0";
                                            break;

                                        }
                                        //salida de loop de creación de usuario !
                                        else if (ExitMenuCreation == "1")
                                        {
                                            ValidAccountCreation = 0;
                                            ExitMenuCreation = "1";
                                            break;
                                        }
                                    }

                                }

                                

                            }
                            //Error de email ya existente 
                            else if(Verification_Error == 1)
                            {
                                Console.WriteLine("ERROR:Este email ya se encuentra registrado!\n");
                                while (true)
                                {
                                    Console.WriteLine("Ingrese entre una de las siguientes opcines\n");
                                    Console.WriteLine("0. Vover al menu de creación de usuario\n");
                                    Console.WriteLine("1. Vover al menu de incio-creación de usuario\n");

                                    ExitMenuCreation = Console.ReadLine();


                                    //Mal ingresado de opcion de salida o vuelta
                                    if (ExitMenuCreation != "0" && ExitMenuCreation != "1")
                                    {
                                        Console.WriteLine("ERROR: Ingrese una opcion valida para continuar!\n");
                                    }
                                    //Correcta opcion de salida o vuelta
                                    else
                                    {
                                        //Vuelta al menu de creacion del usuario 
                                        if (ExitMenuCreation == "0")
                                        {
                                            ValidAccountCreation = 0;
                                            ExitMenuCreation = "0";
                                            break;

                                        }
                                        //salida de loop de creación de usuario !
                                        else if (ExitMenuCreation == "1")
                                        {
                                            ValidAccountCreation = 0;
                                            ExitMenuCreation = "1";
                                            break;
                                        }
                                    }

                                }

                            }
                            //Error de UserName ya exitente
                            else if(Verification_Error == 3)
                            {
                                Console.WriteLine("ERROR:Este nombre de usuario ya existe!");
                                while (true)
                                {
                                    Console.WriteLine("Ingrese entre una de las siguientes opcines\n");
                                    Console.WriteLine("0. Vover al menu de creación de usuario\n");
                                    Console.WriteLine("1. Vover al menu de incio-creación de usuario\n");

                                    ExitMenuCreation = Console.ReadLine();


                                    //Mal ingresado de opcion de salida o vuelta
                                    if (ExitMenuCreation != "0" && ExitMenuCreation != "1")
                                    {
                                        Console.WriteLine("ERROR: Ingrese una opcion valida para continuar!\n");
                                    }
                                    //Correcta opcion de salida o vuelta
                                    else
                                    {
                                        //Vuelta al menu de creacion del usuario 
                                        if (ExitMenuCreation == "0")
                                        {
                                            ValidAccountCreation = 0;
                                            ExitMenuCreation = "0";
                                            break;

                                        }
                                        //salida de loop de creación de usuario !
                                        else if (ExitMenuCreation == "1")
                                        {
                                            ValidAccountCreation = 0;
                                            ExitMenuCreation = "1";
                                            break;
                                        }
                                    }

                                }
                            }
                           
                            //Caso en que la creación de usuario es exitosa!
                            else
                            {
                                //Creacion del prefil

                                Profile profile = new Profile(Email,Password,UserName,BornDate,true,false);

                                ValidAccountCreation = 1;

                                //Agregar a la BDD de la APP

                                app.AddProfile(profile);
                                Console.WriteLine("Cuenta agregada con exito !\n");

                                app.SaveUsers();
                                //Luego de esto vuelve al menu de donde se puede volver al manu de creasion , o se puede iniciar
                                //sesion.
                                


                            }

                        }


                        
                    }

                }




            }

            
        }
    }
}
