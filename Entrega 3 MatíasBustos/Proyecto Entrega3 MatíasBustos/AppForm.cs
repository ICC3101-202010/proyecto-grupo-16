using ProyectoEntrega3MatíasB_MatíasR.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace ProyectoEntrega3MatíasB_MatíasR
{
    public partial class AppForm : Form
    {
        Label MessageErrorNewUser = new Label();
        Label MessageConfirmationNewUserCreated = new Label();
        Button BackToLoginMenuButton = new Button();


        UserController Usercontroller = new UserController();
        MovieController Moviecontroller = new MovieController();
        SongsController Songscontroller = new SongsController();
        WorkerSongController Workersongcontroller = new WorkerSongController();
        WorkerMovieController WorkermovieController = new WorkerMovieController();


        //Index y counters necesarios para la creación
        int count = 0;
        int countCreateMovie = 0;
        int countWorkers = 0;
        int indexLastAdded = 0;
        int TotalNewWorkersAdded = 0;


     
        //Se utilizara para establecer la nueva localizacion del archivo con un metodo local
        string DebugPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string filenameM;

        double MoviesSize;
        string MovieExt;
        TimeSpan DurationM;

        //Datos de peliculas Ingresados por el Admin
        string MovieTitle;
        string CategoryMovie;
        string DatePublishMovie;
        string MovieDescrption;
        string MovieStudio;

        //Confirmaciones de que se han agregado todos los datos necesarios para crear la peli
        bool MovieAdded = false;
        bool WorkedAdded = false;
        bool MovieLoaded = false;
        bool ExistTemporaryWorker = false;

        public AppForm()
        {
            InitializeComponent();

            MessageErrorLogin.ForeColor = Color.Red;
            NewUserPanel.Controls.Add(MessageErrorNewUser);
            AdminMenuPanel.Controls.Add(BackToLoginMenuButton);

            LoadSongFileButton.Text = "Cargar archivo canción";

            //Carga de data en los archivo .dat
            Usercontroller.LoadUsers();
            Moviecontroller.LoadMovies();
            WorkermovieController.LoadWorkersM();
            Songscontroller.LoadSongs();
            Workersongcontroller.LoadWorkerSongs();

            LoadWorkerMovieRols();
            LoadWorkersSongRols();

            Usercontroller.ShowUsers();

        }

        //PELICULAS
        private void NewUserButton_Click(object sender, EventArgs e)
        {
            int locationX = CheckNewUserButton.Location.X;
            int locationY = CheckNewUserButton.Location.Y;

            MessageErrorNewUser.Text = "";
            MessageErrorNewUser.ForeColor = Color.Red;
            MessageErrorNewUser.AutoSize = true;
            MessageErrorNewUser.Location = new Point(locationX-30, locationY + 40);
            MessageErrorNewUser.Visible = true;

            
            NewUserPanel.Visible = true;
            
        }

        string LastUsername;
        private void LoginBotton_Click(object sender, EventArgs e)
        {   

            //Checkea si cualquiera de los textbox no estan vacios (si cualquiera de ellos esta vacio se muestra un mensaje para que rellene los parametros)
            if (CheckTextBoxEmpty(UsernameLoginBox) || CheckTextBoxEmpty(PasswordLoginBox))
            {
                MessageErrorLogin.Text = "Porfavor ingrese todos los datos necesarios";
            }
            else
            {
                int value = Usercontroller.VerifyLogin(UsernameLoginBox.Text, PasswordLoginBox.Text);
                if(value==1)
                {
                    int adminvalue = Usercontroller.VerifyAdminUser(UsernameLoginBox.Text);

                    if (adminvalue==1)
                    {
                        //Menu de Amin
                        MessageErrorLogin.Text = "Parametros ingresados correctamente";

                        /*Se tiene que siempre mostrar los paneles envolventes a este para que se muestre el panel que se
                          quiere.*/
                        AdminMenuPanel.Visible = true;
                        NewUserPanel.Visible = true;

                        int locationX = AddNewSongButton.Location.X;
                        int locationY = AddNewSongButton.Location.Y;

                        BackToLoginMenuButton.Text = "Volver";
                        BackToLoginMenuButton.Size = AddNewSongButton.Size;
                        BackToLoginMenuButton.AutoSize = true;
                        BackToLoginMenuButton.Location = new Point(locationX-230, locationY + 180);
                        BackToLoginMenuButton.Visible = true;

                        BackToLoginMenuButton.Click += new EventHandler(BackToLoginMenuButton_Click);



                    }
                    else
                    {
                        LastUsername = UsernameLoginBox.Text;
                        //Menu Usuario Normal

                        NewUserPanel.Visible = true;
                        AddNewSongPanel.Visible = true;
                        AddNewMoviesPanel.Visible = true;
                        AdminMenuPanel.Visible = true;
                        NormalUserPanel.Visible = true;

                        ShowUserNameLabel.Text = LastUsername;



                        UsernameLoginBox.Text = "";
                        PasswordLoginBox.Text = "";
                    }

                }
                else
                {
                    
                    MessageErrorLogin.Text = "Parametros de ingresos invalidos";
                }
            }
          
        }

        //Checkea si el textbox no esta vacio , si es que esta vacio devuelve false de lo contrario devuelve verdadero.
        public bool CheckTextBoxEmpty(TextBox textBox)
        {
            int lentextbox = textBox.Text.Length;
            if(lentextbox==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckNewUserButton_Click(object sender, EventArgs e)
        {
            //Comprobar si es que el usuario no existe en la BDD

            int value = Usercontroller.VerifyAccountCreation(EmailTextBox.Text, PasswordTextBox.Text, RePassTextBox.Text, UsernameTextBox.Text);

            if (value == 1)
            {
                MessageErrorNewUser.Text = "Ya existe una cuenta con este mail";
            }
            else if (value == 2)
            {
                MessageErrorNewUser.Text = "Las contraseñas no coinciden";
            }
            else if (value == 3)
            {
                MessageErrorNewUser.Text = "Este nombre de usuario ya esta ocupado";
            }
            else if (CheckTextBoxEmpty(EmailTextBox) || CheckTextBoxEmpty(PasswordTextBox) || CheckTextBoxEmpty(RePassTextBox) || CheckTextBoxEmpty(UsernameTextBox))
            {
                MessageErrorNewUser.Text = "Porfavor ingrese todos los datos solicitados";
            }
            else
            {
                Usercontroller.AddProfile(EmailTextBox.Text, PasswordTextBox.Text,UsernameTextBox.Text);
                NewUserPanel.Visible = false;
                Usercontroller.SaveUsers();

                EmailTextBox.Text = "";
                UsernameTextBox.Text = "";
                PasswordTextBox.Text = "";
                RePassTextBox.Text = "";



            }


            
        }


        //Botones del menu ADMIN MENU

        //Boton del Menu ADMIN para MOVIES (Muestra el panel de agregado de peliculas)
        private void AddNewMovieButton_Click(object sender, EventArgs e)
        {
            //Muestra el sub menu de agregado de peliculas para el ADMIN
            AddNewMoviesPanel.Visible = true;
            WorkedAdded = false;
            MovieAdded = false;
            MovieLoaded = false;
            ExistTemporaryWorker = false;

  

        }

        //Boton del Menu ADMIN para Canciones (Muestra el panel de agregado de canciones)
        
        //Crea un pelicula
        private void CreateMovieButton_Click(object sender, EventArgs e)
        {

            //Se verifica si los textbox no estan vacios
            bool CheckMovieTitleTextBox = CheckTextBoxEmpty(MovieTitleTextBox);
            bool CheckMovieCategoryTextBox = CheckTextBoxEmpty(MovieCategoryTextBox);
            bool CheckMovieDatePublishTextBox = CheckTextBoxEmpty(MovieDatePublishTextBox);
            bool CheckMovieDescriptionTextBox = CheckTextBoxEmpty(MovieDescriptionTextBox);
            bool CheckMovieStudioTextBox = CheckTextBoxEmpty(MovieStudioTextBox);
            MessageErrorAddingMovie.Text = "";

            if (CheckMovieTitleTextBox || CheckMovieCategoryTextBox || CheckMovieDatePublishTextBox || CheckMovieDescriptionTextBox || CheckMovieStudioTextBox)
            {


                MessageErrorAddingMovie.ForeColor = System.Drawing.Color.Red;
                MessageErrorAddingMovie.Text = "Ingrese todos los parametros necesarios";
            }

            else
            {
                int CheckMovieInBDD = Moviecontroller.MovieChecker(MovieTitleTextBox.Text, MovieDatePublishTextBox.Text);

                if (CheckMovieInBDD == 1)
                {
                    MessageErrorAddingMovie.ForeColor = System.Drawing.Color.Red;
                    MessageErrorAddingMovie.Text = "Esta pelicula ya se encuentra en la BDD";
                }
                //Aqui es donde se agregan las peliculas a la lista temporal de peliculas (que solo debe contener una peli)
                else
                {
                    if (countCreateMovie < 1)
                    {

                        MessageErrorAddingMovie.ForeColor = System.Drawing.Color.Green;
                        MessageErrorAddingMovie.Text = "Pelicula cargada";

                        MovieTitle = MovieTitleTextBox.Text;
                        CategoryMovie = MovieCategoryTextBox.Text;
                        DatePublishMovie = MovieDatePublishTextBox.Text;
                        MovieDescrption = MovieDescriptionTextBox.Text;
                        MovieStudio = MovieStudioTextBox.Text;

                        

                        MovieTitleTextBox.Text = "";
                        MovieCategoryTextBox.Text = "";
                        MovieDatePublishTextBox.Text = "";
                        MovieDescriptionTextBox.Text = "";
                        MovieStudioTextBox.Text = "";

                        MovieAdded = true;

                        countCreateMovie++;
                    }
                    else
                    {
                        MessageErrorAddingMovie.Text = "Ya se ha cargado una pelicula";
                    } 

                }
            }
            

        }
        //Crea un trabajador para pelicula 
        private void CreateWorkerMovieButton_Click(object sender, EventArgs e)
        {
            //Se verifica si los textbox no estan vacios
            bool CheckWorkerNameTextBox = CheckTextBoxEmpty(WorkerNameTextBox);
            bool CheckWorkerAgeTextBox = CheckTextBoxEmpty(WorkerAgeTextBox);
            bool CheckWorkerGenreTextBox = CheckTextBoxEmpty(WorkerGenreTextBox);
            bool CheckWorkerRolTextBox = CheckTextBoxEmpty(WorkerRolTextBox);



            if (CheckWorkerNameTextBox || CheckWorkerAgeTextBox || CheckWorkerGenreTextBox || CheckWorkerRolTextBox)
            {


                MessageErrorWorkerAdd.ForeColor = System.Drawing.Color.Red;
                MessageErrorWorkerAdd.Text = "Ingrese todos los parametros necesarios";
            }
            else if(WorkerGenreTextBox.Text != "H" && WorkerGenreTextBox.Text != "M")
            {
                MessageErrorWorkerAdd.ForeColor = System.Drawing.Color.Red;
                MessageErrorWorkerAdd.Text = "Porfavor ingrese H o M para el genero";
            }
            else if (WorkerRolTextBox.Text != "Director" && WorkerRolTextBox.Text != "Actor" && WorkerRolTextBox.Text != "Computer animator" && WorkerRolTextBox.Text != "Construction coordinator" && WorkerRolTextBox.Text != "Modeler" && WorkerRolTextBox.Text != "Producer" && WorkerRolTextBox.Text != "Set dresser" && WorkerRolTextBox.Text != "Set decorator" && WorkerRolTextBox.Text != "Sound designer")
            {
                MessageErrorWorkerAdd.ForeColor = System.Drawing.Color.Red;
                MessageErrorWorkerAdd.Text = "Ingrese un rol correcto";
            }
            


            //Se debe de verificar que ese tipo existe o no ya en la bdd de workermovies y si existe se debe 
            else
            {
                int Age = ConvertStringToNum(WorkerAgeTextBox);

                if (Age == 0)
                {
                    MessageErrorWorkerAdd.ForeColor = System.Drawing.Color.Red;
                    MessageErrorWorkerAdd.Text = "Porfavor ingrese un numero en edad";
                }
                else
                {
                    if (ValidateAge(Age))
                    {
                       
                        MessageErrorWorkerAdd.ForeColor = System.Drawing.Color.Green;


                        bool CheckWorker = WorkermovieController.CheckWorkerInDB(WorkerNameTextBox.Text, Age, WorkerGenreTextBox.Text, WorkerRolTextBox.Text);

                        //Caso en que el trabajador no se encuentre en la BDD 
                        if (!CheckWorker)
                        {
                            WorkermovieController.AddNewWorkerMToTemporaryList(WorkerNameTextBox.Text, Age, WorkerGenreTextBox.Text, WorkerRolTextBox.Text);
                            MessageErrorWorkerAdd.Text = "Se a agregado un nuevo trabajador a la lista temporal";
                            TotalNewWorkersAdded++;

                        }
                        //Caso en que se encuentre en la BDD
                        else
                        {
                            WorkermovieController.AddOldWorkerToTemporaryList(WorkermovieController.GetWorker(WorkerNameTextBox.Text, Age, WorkerGenreTextBox.Text, WorkerRolTextBox.Text));


                            MessageErrorWorkerAdd.Text = "Se a agregado un trabajador antiguo ";
                            ExistTemporaryWorker = true;
                        }

                        WorkerNameTextBox.Text = "";
                        WorkerAgeTextBox.Text = "";
                        WorkerGenreTextBox.Text = "";
                        WorkerRolTextBox.Text = "";

                        WorkedAdded = true;
                        countWorkers++;

                    }
                    else
                    {
                        MessageErrorWorkerAdd.Text = "Porfavor ingrese una edad valida";
                    }
                    

                }

            }


        }

        //Carga el archivo de pelicula
        private void LoadMovieFileButton_Click(object sender, EventArgs e)
        {
            if (count <1)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "mp4|*.mp4|avi|*.avi";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                string filePath;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    filePath = openFileDialog.FileName;

                    //Lee el contenido del archivo en un stream
                    var file = File.OpenRead(filePath);

                    //Obtiene el nombre del archivo del filePath
                    filenameM = Path.GetFileName(filePath);

                    //Obtiene la extension del archivo usado
                    MovieExt = Path.GetExtension(openFileDialog.FileName);
                    
                    //Se crea una pelicula vacia (de cualquier tipo de formato agregado al filtro)
                    Stream MovieCopy = new FileStream(filenameM, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);

                    //Se copia el stream de la pelicula agregada al archivo vacio en la carpeta debug
                    file.CopyTo(MovieCopy);

                    //Tamaño en mbs
                    MoviesSize = file.Length / 1000000;

                    var player = new WindowsMediaPlayer();
                    var clip = player.newMedia(filePath);

                    DurationM = TimeSpan.FromSeconds(clip.duration);

                    player.close();

                    
                    file.Close();
                    MovieCopy.Close();

                    MessageConfirmationLoadMovie.ForeColor = Color.Green;
                    MessageConfirmationLoadMovie.Text = "Archivo cargado con exito";
                    count++;
                    MovieLoaded = true;
                }
            }
            else
            {
                MessageConfirmationLoadMovie.ForeColor = Color.Red;
                MessageConfirmationLoadMovie.Text = "Ya se ha cargado un archivo de pelicula!";
            }
            

        }

        //Confirma todos los datos ingresados de tanto peliculas como trabajadores.
        private void CheckDataValuesBotton_Click(object sender, EventArgs e)
        {
            if (!WorkedAdded || !MovieAdded || !MovieLoaded)
            {
                MessageErrorNotComplete.Text = "Porfavor Ingrese por lo menos un elemento de cada tipo";
            }
            else
            {   
                


                WorkermovieController.AddWokerM();

                Moviecontroller.AddMovieToTemporaryList(MovieTitle, CategoryMovie, DurationM, DatePublishMovie, MovieDescrption, MovieStudio, MovieExt, MoviesSize, WorkermovieController.GetTemporaryWorkerMovies(),GetNewFilePath(DebugPath,filenameM));

                //Se guarda la pelicula en la bdd (LA PELICULA lo obtiene de la lista temporal de peliculas que solo contiene una pelicula.)
                Moviecontroller.AddMovieToDB();

                //Si es que existe por lo menos un trabajador antiguo en la lista temporal , entonces se modifica
                //el atributo de cast de peliculas agregando los trabajadores antiguos en este
                if (ExistTemporaryWorker)
                {
                    Moviecontroller.ModifyAtributtesFromOldWorker(MovieTitle, DatePublishMovie, WorkermovieController.GetOldWorkersMoviesFromTemporary());
                }


                //Se graban en un archivo .dat los contenidos de la BDD de trabajadores y de peliculas
                WorkermovieController.SaveWorkersM();
                Moviecontroller.SaveMovies();

                //Se limpia los contenidos de las listas temporales para despues agregar nuevas pelis
               
                Moviecontroller.ClearTemporaryMovieList();
                WorkermovieController.ClearTemporaryNewWorkers();
              

                AddNewMoviesPanel.Visible = false;
                ExistTemporaryWorker = false;

                count = 0;
                countCreateMovie = 0;

                MessageErrorAddingMovie.Text = "";
                MessageErrorWorkerAdd.Text = "";
                MessageConfirmationLoadMovie.Text = "";
                MessageErrorNotComplete.Text = "";

                indexLastAdded = TotalNewWorkersAdded;

                WorkermovieController.ShowDataBaseWorkersM();

                Moviecontroller.ShowMovies();

            }
           




        }
        public void LoadWorkerMovieRols()
        {
            List<string> Rols = new List<string> { "Director", "Actor", "Computer animator", "Construction coordinator", "Producer", "Set dresser", "Set decorator", "Sound designer" };

            foreach (string s in Rols)
            {
                SeeMovieRolsList.Items.Add(s);
            }

        }
        //Evento que rellena los roles con el index de la lista clickeado
        private void SeeMovieRolsList_SelectedIndexChanged(object sender, EventArgs e)
        {

            WorkerRolTextBox.Text = SeeMovieRolsList.Items[SeeMovieRolsList.SelectedIndex].ToString();
        }

        


        //Metodos extras utiles
        public int ConvertStringToNum(TextBox textBox)
        {
            try
            {
                return Int16.Parse(textBox.Text);
            }
            catch
            {
                return 0;
            }
        }
        public string GetNewFilePath(string OldFilePath,string FileName)
        {
            int LastIndex = OldFilePath.LastIndexOf("\\");
            int Lenght = OldFilePath.Length;

            string NewFilePath = OldFilePath.Substring(0, LastIndex);
            NewFilePath = NewFilePath + "\\"+ FileName;
            return NewFilePath;

        }
        
        public bool ValidateAge(int age)
        {
            if(age>0 && age<=100)
            {
                return true;
            }
            return false;
        }



        //[SONGS]
        int countSongCreated = 0;
        int countWorkersS = 0;
        int IndexLastAddedSong = 0;
        int TotalNewWorkersSAdded = 0;
        int countSFile = 0;



        bool SongAdded = false;
        bool ExistTemporaryWorkerS = false;
        bool workerSAdded = false;
        bool SongLoaded = false;

        //Contiene el nombre del archivo incluyendo la extension (cancion)
        string filenameS;
        private void AddNewSongButton_Click(object sender, EventArgs e)
        {
            AddNewMoviesPanel.Visible = true;
            AddNewSongPanel.Visible = true;

            SongAdded = false;
            ExistTemporaryWorkerS = false;
            workerSAdded = false;
            SongLoaded = false;

            
        }

        double SongSize;
        string SongExt;
        TimeSpan DurationS;

        //Datos de peliculas Ingresados por el Admin
        string SongName;
        string SongGender;
        string SongDatePublish;
        string SongLyrics;
        string SongAlbum;


        //Carga la data de los atributos de las canciones (nombre cancion , fecha de publicacion ,etc.)
        private void LoadDataSongButton_Click(object sender, EventArgs e)
        {
            //Se verifica si los textbox no estan vacios
            bool CheckNameSongtextBox = CheckTextBoxEmpty(NameSongtextBox);
            bool CheckAlbumSongtextBox = CheckTextBoxEmpty(AlbumSongtextBox);
            bool CheckGenreSongtextBox = CheckTextBoxEmpty(GenreSongtextBox);
            bool CheckLyricsSongtextBox = CheckTextBoxEmpty(LyricsSongtextBox);
            bool CheckDatePublishSongtextBox = CheckTextBoxEmpty(DatePublishSongtextBox);
            MessageErrorAddingMovie.Text = "";

            if (CheckNameSongtextBox || CheckAlbumSongtextBox || CheckGenreSongtextBox || CheckLyricsSongtextBox || CheckDatePublishSongtextBox)
            {


                MessageErrorLoadDataSongLabel.ForeColor = System.Drawing.Color.Red;
                MessageErrorLoadDataSongLabel.Text = "Ingrese todos los parametros necesarios";
            }

            else
            {
                bool CheckSongInDB = Songscontroller.SongChecker(NameSongtextBox.Text, AlbumSongtextBox.Text, LyricsSongtextBox.Text, GenreSongtextBox.Text, DatePublishSongtextBox.Text); 

                if (CheckSongInDB)
                {
                    MessageErrorLoadDataSongLabel.ForeColor = System.Drawing.Color.Red;
                    MessageErrorLoadDataSongLabel.Text = "Esta canción ya se encuentra en la BDD";
                }
                //Aqui es donde se agregan las canciones a la lista temporal de canciones (que solo debe contener una canción)
                else
                {
                    if (countSongCreated < 1)
                    {

                        MessageErrorLoadDataSongLabel.ForeColor = System.Drawing.Color.Green;
                        MessageErrorLoadDataSongLabel.Text = "Canción cargada";

                        SongName = NameSongtextBox.Text;
                        SongAlbum = AlbumSongtextBox.Text;
                        SongGender = GenreSongtextBox.Text;
                        SongLyrics = LyricsSongtextBox.Text;
                        SongDatePublish = DatePublishSongtextBox.Text;



                        NameSongtextBox.Text = "";
                        AlbumSongtextBox.Text = "";
                        GenreSongtextBox.Text = "";
                        LyricsSongtextBox.Text = "";
                        DatePublishSongtextBox.Text = "";

                        SongAdded = true;

                        countSongCreated++;
                    }
                    else
                    {
                        MessageErrorLoadDataSongLabel.Text = "Ya se ha cargado una canción";
                    }

                }
            }
        }
        //Carga la data de los trabajadores de la canción
        private void LoadWorkerSongDataButton_Click(object sender, EventArgs e)
        {
            //Se verifica si los textbox no estan vacios
            bool CheckNameWorkerSongTextBox = CheckTextBoxEmpty(NameWorkerSongTextBox);
            bool CheckAgeWorkerSongTextBox = CheckTextBoxEmpty(AgeWorkerSongTextBox);
            bool CheckGenreWorkerSongTextBox = CheckTextBoxEmpty(GenderWorkerSongTextBox);
            bool CheckRolWorkerSongTextBox = CheckTextBoxEmpty(RolWorkerSongTextBox);



            if (CheckNameWorkerSongTextBox || CheckAgeWorkerSongTextBox || CheckGenreWorkerSongTextBox || CheckRolWorkerSongTextBox)
            {


                MessageErrorLoadWorkerSdata.ForeColor = System.Drawing.Color.Red;
                MessageErrorLoadWorkerSdata.Text = "Ingrese todos los parametros necesarios";
            }
            else if (GenderWorkerSongTextBox.Text != "H" && GenderWorkerSongTextBox.Text != "M")
            {
                MessageErrorLoadWorkerSdata.ForeColor = System.Drawing.Color.Red;
                MessageErrorLoadWorkerSdata.Text = "Porfavor ingrese H o M para el genero";
            }
            else if (RolWorkerSongTextBox.Text != "Composer" && RolWorkerSongTextBox.Text != "Artist" )
            { 
                MessageErrorLoadWorkerSdata.ForeColor = System.Drawing.Color.Red;
                MessageErrorLoadWorkerSdata.Text = "Ingrese un rol correcto";
            }



            //Se debe de verificar que ese tipo existe o no ya en la bdd de workersongs y si existe se debe 
            else
            {
                int Age1 = ConvertStringToNum(AgeWorkerSongTextBox);

                if (Age1 == 0)
                {
                    MessageErrorLoadWorkerSdata.ForeColor = System.Drawing.Color.Red;
                    MessageErrorLoadWorkerSdata.Text = "Porfavor ingrese un numero en edad";
                }
                else
                {
                    if (ValidateAge(Age1))
                    {
                       

                        MessageErrorLoadWorkerSdata.ForeColor = System.Drawing.Color.Green;

                    

            

                        

                        bool CheckWorkerS = Workersongcontroller.CheckWorkerSong(NameWorkerSongTextBox.Text, Age1, GenderWorkerSongTextBox.Text, RolWorkerSongTextBox.Text);

                        //Caso en que el trabajador no se encuentre en la BDD 
                        if (!CheckWorkerS)
                        {
                            Workersongcontroller.AddNewWorkerSToTemporaryList(NameWorkerSongTextBox.Text, Age1, GenderWorkerSongTextBox.Text, RolWorkerSongTextBox.Text);
                            MessageErrorLoadWorkerSdata.Text = "Se a agregado un nuevo trabajador a la lista temporal";
                            TotalNewWorkersSAdded++;

                        }
                        //Caso en que se encuentre en la BDD
                        else
                        {
                            Workersongcontroller.AddOldWorkerSToTemporaryList(Workersongcontroller.GetWorkerS(NameWorkerSongTextBox.Text, Age1, GenderWorkerSongTextBox.Text, RolWorkerSongTextBox.Text));

                            MessageErrorLoadWorkerSdata.Text = "Se a agregado un trabajador antiguo ";
                            ExistTemporaryWorkerS = true;
                        }

                        NameWorkerSongTextBox.Text = "";
                        AgeWorkerSongTextBox.Text = "";
                        GenderWorkerSongTextBox.Text = "";
                        RolWorkerSongTextBox.Text = "";

                        workerSAdded = true;
                        countWorkersS++;

                        Workersongcontroller.ShowWorkerssFromDb();

                    }
                    else
                    {
                        MessageErrorLoadWorkerSdata.Text = "Porfavor ingrese una edad valida";
                    }


                }

            }
        }
        //Carga el archivo mp3 o wav de la canción
        private void LoadSongFileButton_Click(object sender, EventArgs e)
        {
            if (countSFile < 1)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "mp3|*.mp3|wav|*.wav";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                string filePath1;


                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                   
                    filePath1 = openFileDialog1.FileName;

                    //Lee el contenido del archivo en un stream
                    var file1 = File.OpenRead(filePath1);

                    //Obtiene el nombre del archivo del filePath
                    filenameS = Path.GetFileName(filePath1);

                    //Obtiene la extension del archivo usado
                    SongExt= Path.GetExtension(openFileDialog1.FileName);

                    //Se crea una pelicula vacia (de cualquier tipo de formato agregado al filtro)
                    Stream SongCopy = new FileStream(filenameS, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);

                    //Se copia el stream de la pelicula agregada al archivo vacio en la carpeta debug
                    file1.CopyTo(SongCopy);

                    //Tamaño en mbs
                    SongSize = file1.Length / 1000000;

                    var player1 = new WindowsMediaPlayer();
                    var clip1 = player1.newMedia(filePath1);

                    DurationS = TimeSpan.FromSeconds(clip1.duration);

                    player1.close();


                    file1.Close();
                    SongCopy.Close();

                    MessageErrorLoadSongFile.ForeColor = Color.Green;
                    MessageErrorLoadSongFile.Text = "Archivo cargado con exito";
                    countSFile++;
                    SongLoaded = true;
                }
            }
            else
            {
                MessageErrorLoadSongFile.ForeColor = Color.Red;
                MessageErrorLoadSongFile.Text = "Ya se ha cargado un archivo de Canción!";
            }
        }
        //Checkea que toda la data a sido ingresada antes de ingresar los datos a las BDD respectivas.
        private void CheckAddNewSongButton_Click(object sender, EventArgs e)
        {
            if (!workerSAdded || !SongAdded || !SongLoaded)
            {
                MessageErrorCheckFinalData.Text = "Porfavor Ingrese por lo menos un elemento de cada tipo";
            }
            else
            {



                Workersongcontroller.AddWokerS();


                Songscontroller.AddNewSongToTemporaryList(SongName,SongAlbum, SongGender, DurationS, SongDatePublish,SongLyrics,SongExt,SongSize,Workersongcontroller.GetTemporaryWorkerSongList(),GetNewFilePath(DebugPath, filenameS));



     


                //Se guarda la cancion en la bdd (LA cancion lo obtiene de la lista temporal de canciones que solo contiene una cancion.)
                Songscontroller.AddSongToDB();

                //Si es que existe por lo menos un trabajador antiguo en la lista temporal , entonces se modifica
                //el atributo de cast de peliculas agregando los trabajadores antiguos en este
                if (ExistTemporaryWorkerS)
                {
                    Songscontroller.ModifyAtributtesFromOldWorkerS(SongName, SongDatePublish,SongAlbum ,SongGender,Workersongcontroller.GetOldWorkersSongFromTemporary());
                }


                //Se graban en un archivo .dat los contenidos de la BDD de trabajadores y de peliculas
                Songscontroller.SaveSongs();
                Workersongcontroller.SaveWorkersSong();

                //Se limpia los contenidos de las listas temporales para despues agregar nuevas pelis

                Songscontroller.ClearTemporarySongList();
                Workersongcontroller.ClearTemporaryNewWorkersS();


                AddNewSongPanel.Visible = false;
                AddNewMoviesPanel.Visible = false;
                ExistTemporaryWorkerS = false;

                countSFile = 0;
                countSongCreated = 0;

                MessageErrorLoadDataSongLabel.Text = "";
                MessageErrorLoadSongFile.Text = "";
                MessageErrorLoadWorkerSdata.Text = "";
                MessageErrorCheckFinalData.Text = "";

                Songscontroller.ShowSongs();

                IndexLastAddedSong = TotalNewWorkersSAdded;

            }
        }
        public void LoadWorkersSongRols()
        {
            List<string> Rols1 = new List<string> { "Composer", "Artist" };

            foreach (string s in Rols1)
            {
                ViewRolsWorkerSongList.Items.Add(s);
            }

        }
        private void ViewRolsWorkerSongList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RolWorkerSongTextBox.Text = ViewRolsWorkerSongList.Items[ViewRolsWorkerSongList.SelectedIndex].ToString();
        }

        
        protected void BackToLoginMenuButton_Click(object sender, EventArgs e)
        {
            AdminMenuPanel.Visible = false;
            NewUserPanel.Visible = false;

            UsernameLoginBox.Text = "";
            PasswordLoginBox.Text = "";
            MessageErrorLogin.Text = "";
        }



        //MENU DE USUARIO BUTTONS

        private void SearchMoviesButton_Click(object sender, EventArgs e)
        {

        }

        private void SearchSongsButton_Click(object sender, EventArgs e)
        {
            NewUserPanel.Visible = true;
            AdminMenuPanel.Visible = true;
            AddNewMoviesPanel.Visible = true;
            AddNewSongPanel.Visible = true;
            NormalUserPanel.Visible = true;
            ConfigurationPanel.Visible = true;
            CreatePlaylistPanel.Visible = true;
            SongSearchPanelMenu.Visible = true;

            ShowSongsInfoInListBox();
        }

        private void CreateNewPlaylistButton_Click(object sender, EventArgs e)
        {
            NewUserPanel.Visible = true;
            AdminMenuPanel.Visible = true;
            AddNewMoviesPanel.Visible = true;
            AddNewSongPanel.Visible = true;
            NormalUserPanel.Visible = true;
            ConfigurationPanel.Visible = true;
            CreatePlaylistPanel.Visible = true;

            SeePlaylistAlreadyAddedSongsListBox.Items.Clear();
            ShowPlaylistS();
            SeePlaylistAlreadyAddedMoviesListBox.Items.Clear();
            ShowPlaylistM();

        }


        //BOTONES configuracion del perfil
        private void ConfigurationButton_Click(object sender, EventArgs e)
        {

            ConfigurationPanel.Visible = true;
            ShowEmailLabel.Text = Usercontroller.ReturnUserByUserName(LastUsername).Email;

        }


        private void ChangeUserNameButton_Click_1(object sender, EventArgs e)
        {
            int Verificador = Usercontroller.VerifyLogin(ShowUserNameLabel.Text, InputActualPasswordTextBox.Text);

            if (Verificador == 1)
            {
                MessageErrorChangeUserNameLabel.ForeColor = Color.Green;
                MessageErrorChangeUserNameLabel.Text = "Nombre de usuario cambiado correctamente";

                Usercontroller.ChangeUserName(ShowUserNameLabel.Text, ChangeUserNameTextBox.Text, InputActualPasswordTextBox.Text);

                LastUsername = Usercontroller.ReturnUserByUserName(ChangeUserNameTextBox.Text).UserName;

                Usercontroller.SaveUsers();
                ShowUserNameLabel.Text = ChangeUserNameTextBox.Text;
                Usercontroller.ShowUsers();


            }
            else
            {
                MessageErrorChangeUserNameLabel.ForeColor = Color.Red;
                MessageErrorChangeUserNameLabel.Text = "Ingrese los parametros correctos para cambiar el nombre del usuario";
            }
        }

        private void ChangePrivacyButton_Click_1(object sender, EventArgs e)
        {
            ActualPrivacyUserLabel.Text = (!Usercontroller.ReturnUserByUserName(LastUsername).Privacy).ToString();

            Usercontroller.ChangePrivacy(LastUsername);

            ConfirmPrivacyUserLabel.ForeColor = Color.Green;
            ConfirmPrivacyUserLabel.Text = "Privacidad cambiada con exito";
            Usercontroller.SaveUsers();
        }

        private void BackButtonToUserMenu_Click_1(object sender, EventArgs e)
        {
            ConfigurationPanel.Visible = false;
            ConfirmPrivacyUserLabel.Text = "";
        }



        //Menu create PLAYLIST ACTIONs
        

        private void NewPlaylistSongButton_Click(object sender, EventArgs e)
        {
            MessageErrorNewPlaylistS.ForeColor = Color.Red;
            if (CheckTextBoxEmpty(NamePlaylistSongTextBox) || CheckTextBoxEmpty(TypeFileStoredInPlaylistS))
            {
                MessageErrorNewPlaylistS.Text = "Porfavor rellene los datos";
            }
            else
            {
                if(TypeFileStoredInPlaylistS.Text != ".mp3" && TypeFileStoredInPlaylistS.Text != ".wav")
                {
                    MessageErrorNewPlaylistS.Text = "Porfavor ingrese un formato valido para el tipo de archivo permitido en la PlaylistS";
                }
                else
                {
                    //Retorna true si es que se encuentra ese nombre de playlist en las listas guardadas por el usuario, y retorna falso en caso contrario
                    bool VerificadorPlaylistS = Usercontroller.VerifyPlaylistSInProfile(Usercontroller.ReturnUserByUserName(LastUsername), NamePlaylistSongTextBox.Text);

                    if(VerificadorPlaylistS)
                    {
                        MessageErrorNewPlaylistS.Text = "Este nombre de playlisS ya existe en su Usuario";
                    }
                    else
                    {
                        //Se agrega una nueva playlistS (vacia sin canciones) al usuario.
                        Usercontroller.AddNewPlaylistSToUser(Usercontroller.ReturnUserByUserName(LastUsername), NamePlaylistSongTextBox.Text, TypeFileStoredInPlaylistS.Text);
                        MessageErrorNewPlaylistS.ForeColor = Color.Green;
                        MessageErrorNewPlaylistS.Text = "PlaylistS Creada con exito!";

                        SeePlaylistAlreadyAddedMoviesListBox.Items.Clear();
                        ShowPlaylistS();

                        Usercontroller.SaveUsers();


                    }
                }
            
                

                
            }
        }
        private void NewPlaylistMovieButton_Click(object sender, EventArgs e)
        {
            MessageErrorNewPlaylistS.ForeColor = Color.Red;
            if (CheckTextBoxEmpty(NamePlaylistMovieTextBox) || CheckTextBoxEmpty(TypeFileStoredInPlaylistM))
            {
                MessageErrorNewPlaylistM.Text = "Porfavor rellene los datos";
            }
            else
            {
                if (TypeFileStoredInPlaylistM.Text != ".mp4" && TypeFileStoredInPlaylistM.Text != ".avi")
                {
                    MessageErrorNewPlaylistM.Text = "Porfavor ingrese un formato valido para el tipo de archivo permitido en la PlaylistM";
                }
                else
                {
                    //Retorna true si es que se encuentra ese nombre de playlist en las listas guardadas por el usuario, y retorna falso en caso contrario
                    bool VerificadorPlaylistM = Usercontroller.VerifyPlaylistMInProfile(Usercontroller.ReturnUserByUserName(LastUsername), NamePlaylistMovieTextBox.Text);

                    if (VerificadorPlaylistM)
                    {
                        MessageErrorNewPlaylistM.Text = "Este nombre de playlisS ya existe en su Usuario";
                    }
                    else
                    {
                        //Se agrega una nueva playlistS (vacia sin canciones) al usuario.
                        Usercontroller.AddNewPlaylistMToUser(Usercontroller.ReturnUserByUserName(LastUsername), NamePlaylistMovieTextBox.Text, TypeFileStoredInPlaylistM.Text);
                        MessageErrorNewPlaylistM.ForeColor = Color.Green;
                        MessageErrorNewPlaylistM.Text = "PlaylistM Creada con exito!";

                        SeePlaylistAlreadyAddedMoviesListBox.Items.Clear();
                        ShowPlaylistM();

                        Usercontroller.SaveUsers();


                    }
                }
            }
        }
        //Muestra los nombres de las playlist de un cierto usuario (obtenido del ultimo usuario logeado) y lo muestra
        //En un ListBox
        public void ShowPlaylistS()
        {
            foreach(string s in Usercontroller.ShowPlaylistSNamesFromUser(Usercontroller.ReturnUserByUserName(LastUsername)))
            {
                SeePlaylistAlreadyAddedSongsListBox.Items.Add(s);
            }
        }
        public void ShowPlaylistM()
        {
            foreach (string s in Usercontroller.ShowPlaylistMNamesFromUser(Usercontroller.ReturnUserByUserName(LastUsername)))
            {
                SeePlaylistAlreadyAddedMoviesListBox.Items.Add(s);
            }
        }

        private void BackButtonFromCreatePlaylistPanel_Click(object sender, EventArgs e)
        {
            CreatePlaylistPanel.Visible = false;
            ConfigurationPanel.Visible = false;
        }

        //BUSCADOR DE CANCIONES
        //Selecciona la cancion que quiere ver del buscador de canciones.
        private void SeeSongsFromSearchListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectIndex = SeeSongsFromSearchListBox.SelectedIndex;

            var player = new WindowsMediaPlayer();

            player.URL = Songscontroller.ReturnFilePathFromDB(SelectIndex);
            player.controls.play();


        }

        private void SearchConfirmButton_Click(object sender, EventArgs e)
        {
            MessageErrorFromSearchSongLabel.ForeColor = Color.Red;
            if (CheckTextBoxEmpty(TitleSongTextBoxSearchSong) && CheckTextBoxEmpty(LogicTextBoxSearchSong) && CheckTextBoxEmpty(RolTextBoxSearchSong) && CheckTextBoxEmpty(CharacTextBoxSearchSong) && CheckTextBoxEmpty(ResolutionTextBoxSearchSong) && CheckTextBoxEmpty(EvaluationTextBoxSearchSong) && CheckTextBoxEmpty(CategoryTextBoxSearchSong))
            {
                MessageErrorFromSearchSongLabel.Text = "Porfavor ingrese por lo menos un parametro de busqueda";
            }
        }

        public void ShowSongsInfoInListBox()
        {

            foreach (string s in Songscontroller.ShowSongsInListString())
            {
                SeeSongsFromSearchListBox.Items.Add(s);
            }
        }
    }
}
