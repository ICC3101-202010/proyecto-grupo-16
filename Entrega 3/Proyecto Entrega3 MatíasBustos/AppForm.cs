using ProyectoEntrega3MatíasB_MatíasR.Controllers;
using ProyectoEntrega3MatíasBustos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
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
        TextBox NameBandTextBox = new TextBox();
        Label NameBandLabel = new Label();
        Button BackToLoginMenuButton = new Button();

        Button AddAlbumImageForSong = new Button();
        Button AddMovieImage = new Button();

        UserController Usercontroller = new UserController();
        MovieController Moviecontroller = new MovieController();
        SongsController Songscontroller = new SongsController();

        //Index y counters necesarios para la creación
        int count = 0;
        int countCreateMovie = 0;
        int TotalNewWorkersAdded = 0;


     
        //Se utilizara para establecer la nueva localizacion del archivo con un metodo local
        public string DebugPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
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


        public AppForm()
        {
            InitializeComponent();
            AddAlbumImageForSong.Text = "Agregar ImgAlbum";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            MessageErrorLogin.ForeColor = Color.Red;
            NewUserPanel.Controls.Add(MessageErrorNewUser);
            AdminMenuPanel.Controls.Add(BackToLoginMenuButton);

            LoadSongFileButton.Text = "Cargar archivo canción";

            //Carga de data en los archivo .dat
            Usercontroller.LoadUsers();
            Songscontroller.LoadSongs();
            Moviecontroller.LoadMovies();

            this.Size = new Size(800, 500);
            LoadWorkerMovieRols();
            LoadWorkersSongRols();

            NameBandTextBox.Size = NameSongtextBox.Size;
            NameBandTextBox.Location = new Point(NameSongtextBox.Location.X, NameSongtextBox.Location.Y - 25);
            AddNewSongPanel.Controls.Add(NameBandTextBox);

            AddAlbumImageForSong.Size = LoadSongFileButton.Size;
            AddAlbumImageForSong.Location = new Point(LoadSongFileButton.Location.X, LoadSongFileButton.Location.Y +50);
            AddNewSongPanel.Controls.Add(AddAlbumImageForSong);


            AddMovieImage.Text = "Agregar Imagen";
            AddMovieImage.Size = LoadMovieFileButton.Size;
            AddMovieImage.Location = new Point(LoadMovieFileButton.Location.X, LoadMovieFileButton.Location.Y + 50);
            AddNewMoviesPanel.Controls.Add(AddMovieImage);

            ProfilePictureBox.BackgroundImageLayout = ImageLayout.Stretch;

            NameBandLabel.Text = "Nombre Banda";
            NameBandLabel.Location = new Point(label44.Location.X, NameBandTextBox.Location.Y);
            AddNewSongPanel.Controls.Add(NameBandLabel);

            ChangeProfilePicButton.Click += ChangeProfilePicButton_Click;
            AddAlbumImageForSong.Click += AddAlbumImageForSong_Click;
            AddMovieImage.Click += AddMovieImage_Click;

        }
        string MovieImageFileName = "";
        bool MovieImageButtonClickedAndAccepted = false;
        private void AddMovieImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "png|*.png|jpg|*.jpg";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            string filePathImgMovie;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                filePathImgMovie = openFileDialog.FileName;

                //Lee el contenido del archivo en un stream
                var file = File.OpenRead(filePathImgMovie);
                string FileNameImagMovie;
                //Obtiene el nombre del archivo del filePath
                FileNameImagMovie = Path.GetFileName(filePathImgMovie);

                MovieImageFileName = FileNameImagMovie;

                //Se crea una pelicula vacia (de cualquier tipo de formato agregado al filtro)
                Stream MovieCopy = new FileStream(FileNameImagMovie, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Se copia el stream de la pelicula agregada al archivo vacio en la carpeta debug
                file.CopyTo(MovieCopy);

                file.Close();
                MovieCopy.Close();
                MessageBox.Show("Imagen de pelicula agregado con exito!");
                MovieImageButtonClickedAndAccepted = true;
            }
        }

        string AlbumImageFileName = "";
        bool AlbumButtonClickedAndAccepted = false;
        private void AddAlbumImageForSong_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "png|*.png|jpg|*.jpg";
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

               AlbumImageFileName = filenameM;

                //Se crea una pelicula vacia (de cualquier tipo de formato agregado al filtro)
                Stream MovieCopy = new FileStream(filenameM, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Se copia el stream de la pelicula agregada al archivo vacio en la carpeta debug
                file.CopyTo(MovieCopy);

                file.Close();
                MovieCopy.Close();
                MessageBox.Show("Imagen de album agregado con exito!");
                AlbumButtonClickedAndAccepted = true;
            }
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

                        //Carga de las imagenes para los botones respectivos (desde la carpeta debug, deberia funcionar en cualquier pc):
                        HomePictureBox.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "HomeIcon3.png"));
                        ConfigurationPictureBox.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "ConfigurationIcon.png"));
                        SeeSongOrMovieInMainPanelButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "UpArrowIcon.png"));
                        AlbumImgOrMovieImgPictureBox.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "SampleAlbumDogo.jpg"));
                        HideAddSongOrMoviePanelButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "DownArrowIcon.png"));
                        ShowInfoSongOrMovieBotton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "UpArrowIcon.png"));
                        FlatButtonAndBorderToArgbColorAndStretchImag(HideAddSongOrMoviePanelButton, 0, 0, 0);
                        FlatButtonAndBorderToArgbColorAndStretchImag(ShowInfoSongOrMovieBotton, 40, 40, 40);
                        //Icono de favorito aun no agregado a fav
                        FavButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "NotYetAddedFavIcon.png"));
                        //NextSongButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "NextSongButton.png"));
                        //BackSongButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "BackButtonUPDATE.png"));
                        //PlayAndStopButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "PlayButton.png"));
                        AddSongOrMovieToPlaylist.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "AddSongToPlaylistButton.png"));
                        FlatButtonAndBorderToArgbColorAndStretchImag(AddSongOrMovieToPlaylist, 40, 40, 40);
                        //Se establece el label del nombre de usario en configuracion
                        UserNameConfigurationLabel.Text = LastUsername;
                        //Establece el label de configuración al inicio del logeo
                        if (Usercontroller.ReturnUserByUserName(LastUsername).Privacy)
                        {
                            PrivacyStatusUserLabel.Text = "Privado";
                        }
                        else
                        {
                            PrivacyStatusUserLabel.Text = "Publico";
                        }

                        //Menu Usuario Normal

                        this.WindowState = FormWindowState.Maximized;

                        //Se le quita los bordes estandar y se les asigna el color de su borde como el color del panel en el cual estan
                        AlbumImgOrMovieImgPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                        HomePictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                        ConfigurationPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                        ProfilePictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                        FavButton.BackgroundImageLayout = ImageLayout.Stretch;
                        FlatButtonAndBorderToArgbColorAndStretchImag(FavButton, 40, 40, 40);
                        //FlatButtonAndBorderToArgbColorAndStretchImag(PlayAndStopButton, 40, 40, 40);
                        //FlatButtonAndBorderToArgbColorAndStretchImag(BackSongButton, 40, 40, 40);
                        //FlatButtonAndBorderToArgbColorAndStretchImag(NextSongButton, 40, 40, 40);
                        FlatButtonAndBorderToArgbColorAndStretchImag(SeeSongOrMovieInMainPanelButton, 40, 40, 40);

                        this.MaximizeBox = true;
                        this.MinimizeBox = true;

                        //Intenta cargar una imagen del usuario si es que tiene 
                        try
                        {
                            ProfilePictureBox.BackgroundImage = Image.FromFile(Usercontroller.ReturnUserByUserName(LastUsername).ProfilePicPath);
                        }
                        catch
                        {

                        }
                       

                        NewUserPanel.Visible = true;
                        AddNewSongPanel.Visible = true;
                        AddNewMoviesPanel.Visible = true;
                        AdminMenuPanel.Visible = true;
                        NormalUserPanel.Visible = true;

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

            this.Size = new Size(1000, 500);
        

  

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

                        bool CheckWorkerInMovie = Moviecontroller.CheckWorkerMInDB(WorkerNameTextBox.Text, WorkerRolTextBox.Text, WorkerGenreTextBox.Text ,Age);


                        //Caso en que el trabajador no se encuentre en la BDD 
                        if (!CheckWorkerInMovie)
                        {
                            Moviecontroller.AddNEWWorkerMToList(WorkerNameTextBox.Text, WorkerRolTextBox.Text, WorkerGenreTextBox.Text, Age);
                            MessageErrorWorkerAdd.Text = "Se a agregado un nuevo trabajador a la lista temporal";
                            TotalNewWorkersAdded++;

                        }
                        //Caso en que se encuentre en la BDD
                        else
                        {
                            Moviecontroller.AddOldWorkerMFromDBToList(WorkerNameTextBox.Text, WorkerRolTextBox.Text, WorkerGenreTextBox.Text, Age);

                            MessageErrorWorkerAdd.Text = "Se a agregado un trabajador antiguo ";
                 
                        }

                        WorkerNameTextBox.Text = "";
                        WorkerAgeTextBox.Text = "";
                        WorkerGenreTextBox.Text = "";
                        WorkerRolTextBox.Text = "";

                        WorkedAdded = true;

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
                if (MovieImageFileName.Length > 0 && MovieImageButtonClickedAndAccepted)
                {
                    Moviecontroller.AddNewMovieToDBWithImag(MovieTitle, CategoryMovie, DurationM, DatePublishMovie, MovieDescrption, MovieStudio, MovieExt, MoviesSize, Moviecontroller.GetWorkerMovie(), GetNewFilePath(DebugPath, filenameM),MovieImageFileName);
                }
                else
                {
                    Moviecontroller.AddMovieToDB(MovieTitle, CategoryMovie, DurationM, DatePublishMovie, MovieDescrption, MovieStudio, MovieExt, MoviesSize, Moviecontroller.GetWorkerMovie(), GetNewFilePath(DebugPath, filenameM));
                }
       
                Moviecontroller.SaveMovies();

                //Se limpia los contenidos de las listas temporales para despues agregar nuevas pelis

                Moviecontroller.ClearWorkerMInList();

                AddNewMoviesPanel.Visible = false;

                Moviecontroller.LoadMovies();
       

                count = 0;
                countCreateMovie = 0;
                MovieImageButtonClickedAndAccepted = false;
                MessageErrorAddingMovie.Text = "";
                MessageErrorWorkerAdd.Text = "";
                MessageConfirmationLoadMovie.Text = "";
                MessageErrorNotComplete.Text = "";



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
    
        bool workerSAdded = false;
        bool SongLoaded = false;

        //Contiene el nombre del archivo incluyendo la extension (cancion)
        string filenameS;
        private void AddNewSongButton_Click(object sender, EventArgs e)
        {
            AddNewMoviesPanel.Visible = true;
            AddNewSongPanel.Visible = true;

            SongAdded = false;
            workerSAdded = false;
            SongLoaded = false;

            this.Size = new Size(1000, 500);


        }

        double SongSize;
        string SongExt;
        TimeSpan DurationS;

        //Datos de peliculas Ingresados por el Admin
        string SongName;
        string NameBand;
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
            bool CheckNameBandTextBox = CheckTextBoxEmpty(NameBandTextBox);


            MessageErrorAddingMovie.Text = "";

            if (CheckNameSongtextBox || CheckAlbumSongtextBox || CheckGenreSongtextBox || CheckLyricsSongtextBox || CheckDatePublishSongtextBox || CheckNameBandTextBox)
            {


                MessageErrorLoadDataSongLabel.ForeColor = System.Drawing.Color.Red;
                MessageErrorLoadDataSongLabel.Text = "Ingrese todos los parametros necesarios";
            }

            else
            {
                bool CheckSongInDB = Songscontroller.SongChecker(NameSongtextBox.Text, AlbumSongtextBox.Text, LyricsSongtextBox.Text, GenreSongtextBox.Text, DatePublishSongtextBox.Text,NameBandTextBox.Text); 

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
                        NameBand = NameBandTextBox.Text;


                        NameSongtextBox.Text = "";
                        AlbumSongtextBox.Text = "";
                        GenreSongtextBox.Text = "";
                        LyricsSongtextBox.Text = "";
                        DatePublishSongtextBox.Text = "";
                        NameBandTextBox.Text = "";
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

                    

            

                        //Chekea si el trabajador se encuentra o no en la BDD de canciones (de los trabajadores)
                        bool CheckWorkerS = Songscontroller.CheckWorkerSong(NameWorkerSongTextBox.Text, RolWorkerSongTextBox.Text, Age1, GenderWorkerSongTextBox.Text);
                   
                        //Caso en que el trabajador no se encuentre en la BDD 

              
                        if (!CheckWorkerS)
                        {
                            Songscontroller.AddNEWWorkerSToTemporaryList(NameWorkerSongTextBox.Text, RolWorkerSongTextBox.Text, GenderWorkerSongTextBox.Text, Age1);
                            MessageErrorLoadWorkerSdata.Text = "Se a agregado un nuevo trabajador a la lista temporal";
                            TotalNewWorkersSAdded++;

                        }
                        //Caso en que se encuentre en la BDD
                        else
                        {
          
                            Songscontroller.AddOLDWorkerSToTemporaryList(NameWorkerSongTextBox.Text, RolWorkerSongTextBox.Text, GenderWorkerSongTextBox.Text, Age1);
       
                            MessageErrorLoadWorkerSdata.Text = "Se a agregado un trabajador antiguo ";
                        }

                        NameWorkerSongTextBox.Text = "";
                        AgeWorkerSongTextBox.Text = "";
                        GenderWorkerSongTextBox.Text = "";
                        RolWorkerSongTextBox.Text = "";

                        workerSAdded = true;
                        countWorkersS++;



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

                if(AlbumImageFileName.Length >0 && AlbumButtonClickedAndAccepted)
                {
                    Songscontroller.AddNewSongToDBWithImagAlbum(SongName, SongAlbum, SongGender, DurationS, SongDatePublish, SongLyrics, SongExt, SongSize, Songscontroller.RetunWorkerSFromSong(), GetNewFilePath(DebugPath, filenameS), NameBand, AlbumImageFileName);
                }
                else
                {
                    Songscontroller.AddNewSongToDB(SongName, SongAlbum, SongGender, DurationS, SongDatePublish, SongLyrics, SongExt, SongSize, Songscontroller.RetunWorkerSFromSong(), GetNewFilePath(DebugPath, filenameS), NameBand);
                }
                    
                

                Songscontroller.SaveSongs();
  
                Songscontroller.ClearWorkerSList();

                Songscontroller.LoadSongs();

                AddNewSongPanel.Visible = false;
                AddNewMoviesPanel.Visible = false;

                countSFile = 0;
                countSongCreated = 0;

                MessageErrorLoadDataSongLabel.Text = "";
                MessageErrorLoadSongFile.Text = "";
                MessageErrorLoadWorkerSdata.Text = "";
                MessageErrorCheckFinalData.Text = "";

                AlbumButtonClickedAndAccepted = false;
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

      
      
        public void FlatButtonAndBorderToArgbColorAndStretchImag(Button button,int num1,int num2,int num3)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.FromArgb(num1, num2, num3);
            button.FlatAppearance.BorderSize = 1;
            button.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void HomeMenuUserLabel_Click(object sender, EventArgs e)
        {
            ConfigurationPanel.Visible = false;
            SearchResultPanel.Visible = false;
            
        }

        private void ConfigurationMenuUserLabel_Click(object sender, EventArgs e)
        {
            ConfigurationPanel.Visible = true;
            SearchResultPanel.Visible = false;
        }
        private void ChangeProfilePicButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg|*.jpg|png|*.png";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            string filePath;


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                var file = File.OpenRead(filePath);
                filenameM = Path.GetFileName(filePath);
                Stream ProfilePicCopy = new FileStream(filenameM, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
                file.CopyTo(ProfilePicCopy);
                Usercontroller.ReturnUserByUserName(UserNameConfigurationLabel.Text).ProfilePicPath = GetNewFilePath(DebugPath,Path.GetFileName(filePath));
                
                Usercontroller.SaveUsers();
                Usercontroller.LoadUsers();

                
                ProfilePictureBox.BackgroundImage =Image.FromFile(filePath);
                ProfilePictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                ProfilePicCopy.Close();
                file.Close();
            }
        }
        //MENU DE USUARIO BUTTONS

        //Reacción a eventos de clickeo de los labels de PLAYLIST 
        private void FavSongLabel_Click(object sender, EventArgs e)
        {
            ShowFavSongsLayoutPanel.Controls.Clear();
            FavSongsUsrPanel.Visible = true;
            PlaylistLastFormatSongOrMovieClickedInfo.Visible = false;
            ConfigurationPanel.Visible = true;
            SearchResultPanel.Visible = true;
            FavMoviesUsrPanel.Visible = false;
            PopulateItemsPanelFromFavSongsLPanelUSR(LastUsername);
        }

        private void FavMoviesLabel_Click(object sender, EventArgs e)
        {
            ShowFavMoviesLayoutPanel.Controls.Clear();
            PlaylistLastFormatSongOrMovieClickedInfo.Visible = false;
            FavMoviesUsrPanel.Visible = true;
            FavSongsUsrPanel.Visible = true;
            ConfigurationPanel.Visible = true;
            SearchResultPanel.Visible = true;
            PlaylistSongPanel.Visible = false;
            PopulateItemsPanelFromFavMoviesLPanelUSR(LastUsername);
        }
        //Creacion de playlistS y vistas de estas (Menu de playlistS)
        private void SongPlaylistLabel_Click(object sender, EventArgs e)
        {
            ShowPlaylistSongLayoutPanel.Controls.Clear();
            PopulatePlaylistLayoutPanel(LastUsername, "Song");
            PlaylistLastFormatSongOrMovieClickedInfo.Visible = false;
            PlaylistSongPanel.Visible = true;
            FavMoviesUsrPanel.Visible = true;
            FavSongsUsrPanel.Visible = true;
            ConfigurationPanel.Visible = true;
            SearchResultPanel.Visible = true;
        }
        //Creacion de playlistM y vistas de estas (Menu de playlistM)
        private void MoviePlaylistLabel_Click(object sender, EventArgs e)
        {
            PlaylistLastFormatSongOrMovieClickedInfo.Visible = true;
            ShowPlaylistMUsrLayoutPanel.Controls.Clear();
            PopulatePlaylistLayoutPanel(LastUsername, "Movie");
            PlaylistMoviePanel.Visible = true;
            PlaylistSongPanel.Visible = true;
            FavMoviesUsrPanel.Visible = true;
            FavSongsUsrPanel.Visible = true;
            ConfigurationPanel.Visible = true;
            SearchResultPanel.Visible = true;
        }
        //Start WMP position
        Point StartPositionWMP = new Point(586,13);
        //Button Para mostrar mostrar lo que se esta reproducciendo en el Panel de información
        bool LastWMPScreenState;
        private void SeeSongOrMovieInMainPanelButton_Click(object sender, EventArgs e)
        {
            LastWMPScreenState = axWindowsMediaPlayer1.fullScreen;
            if(LastWMPScreenState)
            {
                try
                {
                    axWindowsMediaPlayer1.fullScreen = false;
                }
                catch
                {
                }
                
            }
            else
            {
                try
                {
                    axWindowsMediaPlayer1.fullScreen = true;
                }
                catch
                {
                }
                
            }
        }

        //Cambio del filtro del buscador
        private void SearchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        string LastSearch;
        int countSearchS = 0;
        int countSearchM = 0;
        //Confirmar Busqueda
        private void SearchMoviesOrSongsButton_Click(object sender, EventArgs e)
        {
            ConfigurationPanel.Visible = true;
            SearchResultPanel.Visible = true;
            FavSongsUsrPanel.Visible = false;
            SongsShowLayoutFlowPanel.Controls.Clear();
            MoviesShowLayoutFlowPanel.Controls.Clear();
            //La busqueda sera invalida y no se hara nada si es q el textbox de busqueda esta vacio o si no se a seleccionado un fitro de busqueda
            if (CheckTextBoxEmpty(SearchMenuUserTextBox) || SearchComboBox.SelectedIndex == -1 || SearchMenuUserTextBox.Text == LastSearch)
            {
            }
            //ALL filter (que funcione con las entradas de texto simples)
            else if(SearchComboBox.SelectedIndex == 0)
            {

            }
            //Titulo (Nombre Canción/Peliculas) filter
            else if (SearchComboBox.SelectedIndex == 1)
            {
              
                //Se guarda la ultima busqueda
                LastSearch = SearchMenuUserTextBox.Text;

                //Se esconden el panel de config
                Songscontroller.AddtoSearchSongByNameList(SearchMenuUserTextBox.Text);
                Songscontroller.AddToSearchStringListOfList();

                Moviecontroller.AddtoSearchMovieByNameList(SearchMenuUserTextBox.Text);
                Moviecontroller.AddToSearchStringListOfList();


                int count = 0;
                int countM = 0;
                string SongNameFromSearch="";
                string BandNameFromSearch="";
                foreach(List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                {
                    
                    foreach(string s in StringList)
                    {
                        
                        if(count==0)
                        {
                            SongNameFromSearch = s;
                        }
                        else
                        {
                            BandNameFromSearch = s;
                        }
                        count++;
                    }
                    PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                    count = 0;
                }
                Songscontroller.ClearSearchLists();
                countSearchS++;


                string MovieTitleFromSearch = "";
                string MovieStudioFromSearch = "";
                foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                {

                    foreach (string s in StringList)
                    {

                        if (countM == 0)
                        {
                            MovieTitleFromSearch = s;
                        }
                        else
                        {
                            MovieStudioFromSearch = s;
                        }
                        countM++;
                    }
                    PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                    countM = 0;
                }
                Moviecontroller.ClearSearchLists();
                countSearchM++;
            }
            //BandName/Studio filter
            else if (SearchComboBox.SelectedIndex == 2)
            {
                //Se guarda la ultima busqueda
                LastSearch = SearchMenuUserTextBox.Text;

                //Se esconden el panel de config
                Songscontroller.AddtoSeachSongByBandName(SearchMenuUserTextBox.Text);
                Songscontroller.AddToSearchStringListOfList();

                Moviecontroller.AddtoSearchMovieByStudioName(SearchMenuUserTextBox.Text);
                Moviecontroller.AddToSearchStringListOfList();


                int count = 0;
                int countM = 0;
                string SongNameFromSearch = "";
                string BandNameFromSearch = "";
                foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                {

                    foreach (string s in StringList)
                    {

                        if (count == 0)
                        {
                            SongNameFromSearch = s;
                        }
                        else
                        {
                            BandNameFromSearch = s;
                        }
                        count++;
                    }
                    PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                    count = 0;
                }
                Songscontroller.ClearSearchLists();
                countSearchS++;


                string MovieTitleFromSearch = "";
                string MovieStudioFromSearch = "";
                foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                {

                    foreach (string s in StringList)
                    {

                        if (countM == 0)
                        {
                            MovieTitleFromSearch = s;
                        }
                        else
                        {
                            MovieStudioFromSearch = s;
                        }
                        countM++;
                    }
                    PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                    countM = 0;
                }
                Moviecontroller.ClearSearchLists();
                countSearchM++;
            }

            //Nombre (Artista/Compositor) filter
            else if (SearchComboBox.SelectedIndex == 3)
            {
                //Se guarda la ultima busqueda
                LastSearch = SearchMenuUserTextBox.Text;

                //Se esconden el panel de config
                Songscontroller.AddtoSearchSongByNameArtistOrComposer(SearchMenuUserTextBox.Text);
                Songscontroller.AddToSearchStringListOfList();

                Moviecontroller.AddtoSeachMovieByWorkerName(SearchMenuUserTextBox.Text);
                Moviecontroller.AddToSearchStringListOfList();


                int count = 0;
                int countM = 0;
                string SongNameFromSearch = "";
                string BandNameFromSearch = "";
                foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                {

                    foreach (string s in StringList)
                    {

                        if (count == 0)
                        {
                            SongNameFromSearch = s;
                        }
                        else
                        {
                            BandNameFromSearch = s;
                        }
                        count++;
                    }
                    PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                    count = 0;
                }
                Songscontroller.ClearSearchLists();
                countSearchS++;


                string MovieTitleFromSearch = "";
                string MovieStudioFromSearch = "";
                foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                {

                    foreach (string s in StringList)
                    {

                        if (countM == 0)
                        {
                            MovieTitleFromSearch = s;
                        }
                        else
                        {
                            MovieStudioFromSearch = s;
                        }
                        countM++;
                    }
                    PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                    countM = 0;
                }
                Moviecontroller.ClearSearchLists();
               
            }
     
            //Edad ("") (>) filter
            else if (SearchComboBox.SelectedIndex == 4)
            {
                try
                {
                    //Se guarda la ultima busqueda
                    LastSearch = SearchMenuUserTextBox.Text;

                    //Se esconden el panel de config
                    Songscontroller.AddtoSearchSongByAge(4,Int32.Parse(SearchMenuUserTextBox.Text));
                    Songscontroller.AddToSearchStringListOfList();

                    Moviecontroller.AddtoSearchMovieByAge(4, Int32.Parse(SearchMenuUserTextBox.Text));
                    Moviecontroller.AddToSearchStringListOfList();


                    int count = 0;
                    int countM = 0;
                    string SongNameFromSearch = "";
                    string BandNameFromSearch = "";
                    foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (count == 0)
                            {
                                SongNameFromSearch = s;
                            }
                            else
                            {
                                BandNameFromSearch = s;
                            }
                            count++;
                        }
                        PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                        count = 0;
                    }
                    Songscontroller.ClearSearchLists();
                    countSearchS++;


                    string MovieTitleFromSearch = "";
                    string MovieStudioFromSearch = "";
                    foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (countM == 0)
                            {
                                MovieTitleFromSearch = s;
                            }
                            else
                            {
                                MovieStudioFromSearch = s;
                            }
                            countM++;
                        }
                        PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                        countM = 0;
                    }
                    Moviecontroller.ClearSearchLists();
                }
                catch
                {
                    MessageBox.Show("Porfavor ingrese un numero");
                }
            }
            //Edad ("") (<) filter
            else if (SearchComboBox.SelectedIndex == 5)
            {
                try
                {
                    //Se guarda la ultima busqueda
                    LastSearch = SearchMenuUserTextBox.Text;

                    //Se esconden el panel de config
                    Songscontroller.AddtoSearchSongByAge(5, Int32.Parse(SearchMenuUserTextBox.Text));
                    Songscontroller.AddToSearchStringListOfList();

                    Moviecontroller.AddtoSearchMovieByAge(5, Int32.Parse(SearchMenuUserTextBox.Text));
                    Moviecontroller.AddToSearchStringListOfList();


                    int count = 0;
                    int countM = 0;
                    string SongNameFromSearch = "";
                    string BandNameFromSearch = "";
                    foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (count == 0)
                            {
                                SongNameFromSearch = s;
                            }
                            else
                            {
                                BandNameFromSearch = s;
                            }
                            count++;
                        }
                        PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                        count = 0;
                    }
                    Songscontroller.ClearSearchLists();
                    countSearchS++;


                    string MovieTitleFromSearch = "";
                    string MovieStudioFromSearch = "";
                    foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (countM == 0)
                            {
                                MovieTitleFromSearch = s;
                            }
                            else
                            {
                                MovieStudioFromSearch = s;
                            }
                            countM++;
                        }
                        PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                        countM = 0;
                    }
                    Moviecontroller.ClearSearchLists();
                }
                catch
                {
                    MessageBox.Show("Porfavor ingrese un numero");
                }
            }
            //Edad ("") (=) filter
            else if (SearchComboBox.SelectedIndex == 6)
            {
                try
                {
                    //Se guarda la ultima busqueda
                    LastSearch = SearchMenuUserTextBox.Text;

                    //Se esconden el panel de config
                    Songscontroller.AddtoSearchSongByAge(6, Int32.Parse(SearchMenuUserTextBox.Text));
                    Songscontroller.AddToSearchStringListOfList();

                    Moviecontroller.AddtoSearchMovieByAge(6, Int32.Parse(SearchMenuUserTextBox.Text));
                    Moviecontroller.AddToSearchStringListOfList();


                    int count = 0;
                    int countM = 0;
                    string SongNameFromSearch = "";
                    string BandNameFromSearch = "";
                    foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (count == 0)
                            {
                                SongNameFromSearch = s;
                            }
                            else
                            {
                                BandNameFromSearch = s;
                            }
                            count++;
                        }
                        PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                        count = 0;
                    }
                    Songscontroller.ClearSearchLists();
                    countSearchS++;


                    string MovieTitleFromSearch = "";
                    string MovieStudioFromSearch = "";
                    foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (countM == 0)
                            {
                                MovieTitleFromSearch = s;
                            }
                            else
                            {
                                MovieStudioFromSearch = s;
                            }
                            countM++;
                        }
                        PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                        countM = 0;
                    }
                    Moviecontroller.ClearSearchLists();
                }
                catch
                {
                    MessageBox.Show("Porfavor ingrese un numero");
                }
            }
            //Resolution Mb filter (>)
            else if (SearchComboBox.SelectedIndex == 7)
            {
                try
                {
                    //Se guarda la ultima busqueda
                    LastSearch = SearchMenuUserTextBox.Text;

                    //Se esconden el panel de config
                    Songscontroller.AddtoSearchSongBySize(7, Int32.Parse(SearchMenuUserTextBox.Text));
                    Songscontroller.AddToSearchStringListOfList();

                    Moviecontroller.AddtoSearchMovieBySize(7, Int32.Parse(SearchMenuUserTextBox.Text));
                    Moviecontroller.AddToSearchStringListOfList();


                    int count = 0;
                    int countM = 0;
                    string SongNameFromSearch = "";
                    string BandNameFromSearch = "";
                    foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (count == 0)
                            {
                                SongNameFromSearch = s;
                            }
                            else
                            {
                                BandNameFromSearch = s;
                            }
                            count++;
                        }
                        PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                        count = 0;
                    }
                    Songscontroller.ClearSearchLists();
                    countSearchS++;


                    string MovieTitleFromSearch = "";
                    string MovieStudioFromSearch = "";
                    foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (countM == 0)
                            {
                                MovieTitleFromSearch = s;
                            }
                            else
                            {
                                MovieStudioFromSearch = s;
                            }
                            countM++;
                        }
                        PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                        countM = 0;
                    }
                    Moviecontroller.ClearSearchLists();
                }
                catch
                {
                    MessageBox.Show("Porfavor ingrese un numero");
                }
            }
            //Resolution Mb filter (<)
            else if (SearchComboBox.SelectedIndex == 8)
            {
                try
                {
                    //Se guarda la ultima busqueda
                    LastSearch = SearchMenuUserTextBox.Text;

                    //Se esconden el panel de config
                    Songscontroller.AddtoSearchSongBySize(8, Int32.Parse(SearchMenuUserTextBox.Text));
                    Songscontroller.AddToSearchStringListOfList();

                    Moviecontroller.AddtoSearchMovieBySize(8, Int32.Parse(SearchMenuUserTextBox.Text));
                    Moviecontroller.AddToSearchStringListOfList();


                    int count = 0;
                    int countM = 0;
                    string SongNameFromSearch = "";
                    string BandNameFromSearch = "";
                    foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (count == 0)
                            {
                                SongNameFromSearch = s;
                            }
                            else
                            {
                                BandNameFromSearch = s;
                            }
                            count++;
                        }
                        PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                        count = 0;
                    }
                    Songscontroller.ClearSearchLists();
                    countSearchS++;


                    string MovieTitleFromSearch = "";
                    string MovieStudioFromSearch = "";
                    foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (countM == 0)
                            {
                                MovieTitleFromSearch = s;
                            }
                            else
                            {
                                MovieStudioFromSearch = s;
                            }
                            countM++;
                        }
                        PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                        countM = 0;
                    }
                    Moviecontroller.ClearSearchLists();
                }
                catch
                {
                    MessageBox.Show("Porfavor ingrese un numero");
                }
            }
            //Resolution Mb filter (=)
            else if (SearchComboBox.SelectedIndex == 9)
            {
                try
                {
                    //Se guarda la ultima busqueda
                    LastSearch = SearchMenuUserTextBox.Text;

                    //Se esconden el panel de config
                    Songscontroller.AddtoSearchSongBySize(9, Int32.Parse(SearchMenuUserTextBox.Text));
                    Songscontroller.AddToSearchStringListOfList();

                    Moviecontroller.AddtoSearchMovieBySize(9, Int32.Parse(SearchMenuUserTextBox.Text));
                    Moviecontroller.AddToSearchStringListOfList();


                    int count = 0;
                    int countM = 0;
                    string SongNameFromSearch = "";
                    string BandNameFromSearch = "";
                    foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (count == 0)
                            {
                                SongNameFromSearch = s;
                            }
                            else
                            {
                                BandNameFromSearch = s;
                            }
                            count++;
                        }
                        PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                        count = 0;
                    }
                    Songscontroller.ClearSearchLists();
                    countSearchS++;


                    string MovieTitleFromSearch = "";
                    string MovieStudioFromSearch = "";
                    foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (countM == 0)
                            {
                                MovieTitleFromSearch = s;
                            }
                            else
                            {
                                MovieStudioFromSearch = s;
                            }
                            countM++;
                        }
                        PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                        countM = 0;
                    }
                    Moviecontroller.ClearSearchLists();
                }
                catch
                {
                    MessageBox.Show("Porfavor ingrese un numero");
                }
            }
            //Rating filter (>)
            else if (SearchComboBox.SelectedIndex == 10)
            {
                try
                {
                    //Se guarda la ultima busqueda
                    LastSearch = SearchMenuUserTextBox.Text;

                    //Se esconden el panel de config
                    Songscontroller.AddtoSearchSongByRankingS(9, Int32.Parse(SearchMenuUserTextBox.Text));
                    Songscontroller.AddToSearchStringListOfList();

                    Moviecontroller.AddtoSearchMovieByRankingM(9, Int32.Parse(SearchMenuUserTextBox.Text));
                    Moviecontroller.AddToSearchStringListOfList();


                    int count = 0;
                    int countM = 0;
                    string SongNameFromSearch = "";
                    string BandNameFromSearch = "";
                    foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (count == 0)
                            {
                                SongNameFromSearch = s;
                            }
                            else
                            {
                                BandNameFromSearch = s;
                            }
                            count++;
                        }
                        PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                        count = 0;
                    }
                    Songscontroller.ClearSearchLists();
                    countSearchS++;


                    string MovieTitleFromSearch = "";
                    string MovieStudioFromSearch = "";
                    foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                    {

                        foreach (string s in StringList)
                        {

                            if (countM == 0)
                            {
                                MovieTitleFromSearch = s;
                            }
                            else
                            {
                                MovieStudioFromSearch = s;
                            }
                            countM++;
                        }
                        PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                        countM = 0;
                    }
                    Moviecontroller.ClearSearchLists();
                }
                catch
                {
                    MessageBox.Show("Porfavor ingrese un numero");
                }
            }
            //Rating filter (<)
            else if (SearchComboBox.SelectedIndex == 11)
            {

            }
            //Rating filter (=)
            else if (SearchComboBox.SelectedIndex == 12)
            {

            }
            //Categoria filter
            else if (SearchComboBox.SelectedIndex == 13)
            {
               
                //Se guarda la ultima busqueda
                LastSearch = SearchMenuUserTextBox.Text;

                //Se esconden el panel de config
                Songscontroller.AddtoSearchSongByCategory(SearchMenuUserTextBox.Text);
                Songscontroller.AddToSearchStringListOfList();

                Moviecontroller.AddtoSearchMovieByCategory(SearchMenuUserTextBox.Text);
                Moviecontroller.AddToSearchStringListOfList();


                int count = 0;
                int countM = 0;
                string SongNameFromSearch = "";
                string BandNameFromSearch = "";
                foreach (List<string> StringList in Songscontroller.ReturnSearchListOfListstringInfo())
                {

                    foreach (string s in StringList)
                    {

                        if (count == 0)
                        {
                            SongNameFromSearch = s;
                        }
                        else
                        {
                            BandNameFromSearch = s;
                        }
                        count++;
                    }
                    PopulateItemsPanelFromSongPanelWithASearchPattern(SongNameFromSearch, BandNameFromSearch);
                    count = 0;
                }
                Songscontroller.ClearSearchLists();
                countSearchS++;


                string MovieTitleFromSearch = "";
                string MovieStudioFromSearch = "";
                foreach (List<string> StringList in Moviecontroller.ReturnSearchListOfListstringInfo())
                {

                    foreach (string s in StringList)
                    {

                        if (countM == 0)
                        {
                            MovieTitleFromSearch = s;
                        }
                        else
                        {
                            MovieStudioFromSearch = s;
                        }
                        countM++;
                    }
                    PopulateItemsPanelFromMoviePanelWithASearchPattern(MovieTitleFromSearch, MovieStudioFromSearch);

                    countM = 0;
                }
                Moviecontroller.ClearSearchLists();
                countSearchM++;
            }
            else
            {
                

            }

        }
        //Button de cambio de privacidad en configuracion 
        private void ChangePrivacyButton_Click(object sender, EventArgs e)
        {
            bool PrivacidadActual = Usercontroller.ReturnUserByUserName(LastUsername).Privacy;
            Usercontroller.ReturnUserByUserName(LastUsername).Privacy = !PrivacidadActual;
            bool PrivacidadCambiada = !PrivacidadActual;

            if (PrivacidadCambiada == true)
            {
                PrivacyStatusUserLabel.Text = "Privado";
            }
            else
            {
                PrivacyStatusUserLabel.Text = "Publico";
            }
            Usercontroller.SaveUsers();
            Usercontroller.LoadUsers();
            MessageBox.Show("Privacidad cambiada con exito");
        }


        public void PopulateItemsPanelFromSongPanelWithASearchPattern(string SongName,string SongBand)
        {
            SongMovieFormatShow songsFormat = new SongMovieFormatShow();
            songsFormat.Titulo = SongName;
            songsFormat.NombreBanda = SongBand;
            songsFormat.Identificador = "Song";
            //SampleImage para la siguiente entrega AGREGAR IMAGEN DE ALBUM!!!
            //Checkear si el atributo de album no es nulo
            //Si es que el largo del string que guarda la imagen es mayor que cero (no vacio), entonces se utiliza la imagen grabada

            try
            {
                string FilePathAlbumSong = Songscontroller.ReturnSongBySongNameAndBandName(SongName, SongBand).AlbumfilePath1;
                if (FilePathAlbumSong.Length > 0)
                {
                    songsFormat.AlbumImage = Image.FromFile(GetNewFilePath(DebugPath, FilePathAlbumSong));
                }
            }
            catch
            {
                songsFormat.AlbumImage = Image.FromFile(GetNewFilePath(DebugPath, "SampleAlbumImage.jpg"));
            }
            
            songsFormat.Click += songMovieFormatShow_Click;
            songsFormat.MouseMove += OnMouseMoveOnBotton; ;
            songsFormat.MouseLeave += SongsFormat_MouseLeave;
            SongsShowLayoutFlowPanel.Controls.Add(songsFormat);
        }
        public void PopulateItemsPanelFromFavSongsLPanelUSR(string Username)
        {
            foreach(List<string> InfoS in Usercontroller.ReturnFavSongInfoUsr(Username))
            {
                SongMovieFormatShow songsFormat = new SongMovieFormatShow();
                songsFormat.Titulo = InfoS[0];
                songsFormat.NombreBanda = InfoS[1];
                songsFormat.Identificador = "Song";
                try
                {
                    if (InfoS[2].Length > 0)
                    {
                        songsFormat.AlbumImage = Image.FromFile(GetNewFilePath(DebugPath, InfoS[2]));
                    }
                }
                catch
                {
                    songsFormat.AlbumImage = Image.FromFile(GetNewFilePath(DebugPath, "SampleAlbumImage.jpg"));
                }
                
                songsFormat.Click += songMovieFormatShow_Click;
                songsFormat.MouseMove += OnMouseMoveOnBotton;
                songsFormat.MouseLeave += SongsFormat_MouseLeave;
                ShowFavSongsLayoutPanel.Controls.Add(songsFormat);

            }
        }
        public void PopulateItemsPanelFromFavMoviesLPanelUSR(string Username)
        {
            foreach (List<string> InfoM in Usercontroller.ReturnFavMovieInfoUsr(Username))
            {
                SongMovieFormatShow songsFormat = new SongMovieFormatShow();
                songsFormat.Titulo = InfoM[0];
                songsFormat.NombreBanda = InfoM[1];
                songsFormat.Identificador = "Movie";
                try
                {
                    if (InfoM[2].Length > 0)
                    {
                        songsFormat.AlbumImage = Image.FromFile(GetNewFilePath(DebugPath, InfoM[2]));
                    }
                }
                catch
                {
                    songsFormat.AlbumImage = Image.FromFile(GetNewFilePath(DebugPath, "SampleAlbumImage.jpg"));
                }

                songsFormat.Click += songMovieFormatShow_Click;
                songsFormat.MouseMove += OnMouseMoveOnBotton;
                songsFormat.MouseLeave += SongsFormat_MouseLeave;
                ShowFavMoviesLayoutPanel.Controls.Add(songsFormat);

            }
        }
        private void SongsFormat_MouseLeave(object sender, EventArgs e)
        {
            (sender as SongMovieFormatShow).BackColor = Color.FromArgb(64,64, 64);
        }

        private void OnMouseMoveOnBotton(object sender, MouseEventArgs e)
        {
            (sender as SongMovieFormatShow).BackColor = Color.FromArgb(105, 105, 105);
        }
        public void PopulatePlaylistLayoutPanel(string Username, string Identificador)
        {
            if (Identificador == "Song")
            {
                foreach (List<string> InfoPlaylistS in Usercontroller.ReturnPlaylistSInfoForUsr(LastUsername))
                {
                    PlaylistFormatUserController playlistFormatUserController = new PlaylistFormatUserController();
                    playlistFormatUserController.PlaylistName = InfoPlaylistS[0];
                    playlistFormatUserController.OwnerUsername = InfoPlaylistS[1];
                    playlistFormatUserController.TypePlaylistStorage = InfoPlaylistS[2];
                    playlistFormatUserController.IdentificadorPlaylist = "Song";
                    try
                    {
                        playlistFormatUserController.UsernamePic = Image.FromFile(Path.Combine(Usercontroller.ReturnUserByUserName(LastUsername).ProfilePicPath));
                    }
                    catch
                    {
                        playlistFormatUserController.UsernamePic = Image.FromFile("SampleProfile.jpg");
                    }
                    playlistFormatUserController.Click += PlaylistFormatUserController_Click;
                    playlistFormatUserController.MouseMove += PlaylistFormatUserController_MouseMove;
                    playlistFormatUserController.MouseLeave += PlaylistFormatUserController_MouseLeave;
                    ShowPlaylistSongLayoutPanel.Controls.Add(playlistFormatUserController);
                }    
                
            }
            else if (Identificador == "Movie")
            {
                foreach (List<string> InfoPlaylistM in Usercontroller.ReturnMoviesPlaylistMInfoForUsr(LastUsername))
                {
                    PlaylistFormatUserController playlistFormatUserController = new PlaylistFormatUserController();
                    playlistFormatUserController.PlaylistName = InfoPlaylistM[0];
                    playlistFormatUserController.OwnerUsername = InfoPlaylistM[1];
                    playlistFormatUserController.TypePlaylistStorage = InfoPlaylistM[2];
                    playlistFormatUserController.IdentificadorPlaylist = "Movie";
                    try
                    {
                        playlistFormatUserController.UsernamePic = Image.FromFile(Path.Combine(Usercontroller.ReturnUserByUserName(LastUsername).ProfilePicPath));
                    }
                    catch
                    {
                        playlistFormatUserController.UsernamePic = Image.FromFile("SampleProfile.jpg");
                    }
                    playlistFormatUserController.Click += PlaylistFormatUserController_Click;
                    playlistFormatUserController.MouseMove += PlaylistFormatUserController_MouseMove;
                    playlistFormatUserController.MouseLeave += PlaylistFormatUserController_MouseLeave;
                    ShowPlaylistMUsrLayoutPanel.Controls.Add(playlistFormatUserController);
                }
            }
        }
        public void PopulateItemsPanelFromMoviePanelWithASearchPattern(string Title,string Studio)
        {
            SongMovieFormatShow songsFormat = new SongMovieFormatShow();
            songsFormat.Titulo = Title;
            songsFormat.NombreBanda = Studio;
            songsFormat.Identificador = "Movie";
            //SampleImage para la siguiente entrega AGREGAR IMAGEN DE ALBUM!!!
            try
            {
                string FilePathimageMovie = Moviecontroller.ReturnMovieByTitleNameAndStudioName(Title, Studio).MovieImgFilePath1;
                if (FilePathimageMovie.Length > 0)
                {
                    songsFormat.AlbumImage = Image.FromFile(GetNewFilePath(DebugPath, FilePathimageMovie));
                }
            }
            catch
            {
                songsFormat.AlbumImage = Image.FromFile(GetNewFilePath(DebugPath, "SampleAlbumImage.jpg"));
            }
            songsFormat.Click += songMovieFormatShow_Click;
            songsFormat.MouseMove += OnMouseMoveOnBotton; 
            songsFormat.MouseLeave += SongsFormat_MouseLeave;
            MoviesShowLayoutFlowPanel.Controls.Add(songsFormat);
        }

        int ClickCounterReproductions = 0;
        
        string LastFilePathReproduce;
        //Reproductor de media
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        
        private void PlayAndStopButton_Click(object sender, EventArgs e)
        {
            string CurrentStatusPlayer = player.status;

            if (CurrentStatusPlayer == "Detenido")
            {
                try
                {
                    player.URL = LastFilePathReproduce;
                    player.controls.play();
                    //PlayAndStopButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath,"StopBottonUpdate.png"));
                }
                catch
                {

                }
            }
            //En este caso esta cuando el player se esta reproduciendo
            else
            {
                player.controls.stop();
                //PlayAndStopButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "PlayButton.png"));
            }
        }

        private void BackSongButton_Click(object sender, EventArgs e)
        {

        }

        private void NextSongButton_Click(object sender, EventArgs e)
        {

        }

        SongMovieFormatShow LastFormatClicked;
        //Clickeo de la cancion en la pestaña que muestra las canciones enontradas!
        private void songMovieFormatShow_Click(object sender, EventArgs e)
        {
            //Verificación primero de que se clikeo (una canción o una pelicula)
            string TitleSorM = (sender as SongMovieFormatShow).Titulo;
            string BandaOrStudioName = (sender as SongMovieFormatShow).NombreBanda;
            string Identificador = (sender as SongMovieFormatShow).Identificador;

            //Se guarda el ultimo formato clikeado
            LastFormatClicked = (sender as SongMovieFormatShow);
            if (Identificador == "Song")
            {
                try
                {
                    string FilePathAlbumSong = Songscontroller.ReturnSongBySongNameAndBandName(TitleSorM, BandaOrStudioName).AlbumfilePath1;
                    if (FilePathAlbumSong.Length > 0)
                    {
                        AlbumImgOrMovieImgPictureBox.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, FilePathAlbumSong));
                    }
                }
                catch
                {
                    AlbumImgOrMovieImgPictureBox.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "SampleAlbumImage.jpg"));
                }
            }
            else if(Identificador == "Movie")
            {
                try
                {
                    string FilePathIMGMovie = Moviecontroller.ReturnMovieByTitleNameAndStudioName(TitleSorM, BandaOrStudioName).MovieImgFilePath1;
                    if (FilePathIMGMovie.Length > 0)
                    {
                        AlbumImgOrMovieImgPictureBox.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, FilePathIMGMovie));
                    }
                }
                catch
                {
                    AlbumImgOrMovieImgPictureBox.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "SampleAlbumImage.jpg"));
                }
            }
           
            BandOrStudioNameLabel.Text = BandaOrStudioName;
            TitleSongOrMovieLabel.Text = TitleSorM;

            string FilePath = "";
            //Caso en que el formato clikeado es de una canción
            if (Identificador == "Song")
            {
                FilePath = Songscontroller.ReturnFilePathMp3FromNameSandB(TitleSorM, BandaOrStudioName);
                //Actualización del icono de fav c/r a la canción clickeada por el usr respectivo.
                axWindowsMediaPlayer1.URL = FilePath;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                if (Usercontroller.CheckSongInFavSongsUsr(LastUsername, TitleSorM, BandaOrStudioName))
                {
                    FavButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "AddedFavicon.png"));
                }
                else
                {
                    FavButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "NotYetAddedFavIcon.png"));
                }

            }
            //Caso en que el formato clikeado es de una pelicula
            else if(Identificador == "Movie")
            {
                FilePath = Moviecontroller.ReturnFilePathMovieSearch(TitleSorM, BandaOrStudioName);
                //Actualización del icono de fav c/r a la canción clickeada por el usr respectivo.
                axWindowsMediaPlayer1.URL = FilePath;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                if (Usercontroller.CheckMovieInFavMoviesUsr(LastUsername, TitleSorM, BandaOrStudioName))
                {
                    FavButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "AddedFavicon.png"));
                }
                else
                {
                    FavButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "NotYetAddedFavIcon.png"));
                }
            }
            else
            {
            }
           
        }

        //Aqui va mostrar la información completa de la canción 
        private void TitleSongOrMovieLabel_Click(object sender, EventArgs e)
        {

        }
        private void FavButton_Click(object sender, EventArgs e)
        {
            //Primero hay que chekear si es que hay una canción seleccionada (luego identificar si fue de una pelicula o no)
            string StatusWMP = axWindowsMediaPlayer1.status;
            Usercontroller.LoadUsers();
            //Significa que se puede estar reproduciendo una musica , o una canción esta detenida
            if (StatusWMP.Length > 0)
            {
                //Hay que revisar el clikeo del format actual para asi asignar a la lista de fav de canciones/movies c/r del usuario

                if (LastFormatClicked.Identificador == "Song")
                {
                    //CHECKEO
                    //Si es que es true significa que la canción clickeada ya existe en la lista de favs, por la tanto el click de fav
                    //va a quitar la canción clikeada de favsongs
                    bool CheckSongInFavUserList = Usercontroller.CheckSongInFavSongsUsr(LastUsername, LastFormatClicked.Titulo, LastFormatClicked.NombreBanda);
                    if(CheckSongInFavUserList == false)
                    {
                        //Se agrega la canción a la lista de favsS del user
                        Usercontroller.ReturnUserByUserName(LastUsername).FavSong1.Add(Songscontroller.ReturnSongBySongNameAndBandName(LastFormatClicked.Titulo, LastFormatClicked.NombreBanda));
                        FavButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "AddedFavicon.png"));
                    }
                    else
                    {
                        //Se hace un pop del elemento de canción en fav
                        Usercontroller.RemoveSongFromFavSongListFromUser(LastUsername, LastFormatClicked.Titulo, LastFormatClicked.NombreBanda);
                        FavButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "NotYetAddedFavIcon.png"));
                    }

                   
       
                }
                else if(LastFormatClicked.Identificador == "Movie")
                {
                    //CHECKEO
                    //Si es que es true significa que la canción clickeada ya existe en la lista de favs, por la tanto el click de fav
                    //va a quitar la canción clikeada de favsongs
                    bool CheckMovieInFavMoviesUsr = Usercontroller.CheckMovieInFavMoviesUsr(LastUsername, LastFormatClicked.Titulo, LastFormatClicked.NombreBanda);
                    if (CheckMovieInFavMoviesUsr == false)
                    {
                        //Se agrega la canción a la lista de favsS del user

                        Usercontroller.ReturnUserByUserName(LastUsername).FavMovies1.Add(Moviecontroller.ReturnMovieByTitleNameAndStudioName(LastFormatClicked.Titulo, LastFormatClicked.NombreBanda));
                        FavButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "AddedFavicon.png"));
                    }
                    else
                    {
                        //Se hace un pop del elemento de canción en fav
                        Usercontroller.RemoveMovieFromFavMovieListFromUser(LastUsername, LastFormatClicked.Titulo, LastFormatClicked.NombreBanda);
                        FavButton.BackgroundImage = Image.FromFile(GetNewFilePath(DebugPath, "NotYetAddedFavIcon.png"));

                    }

                }
                
                Usercontroller.SaveUsers();
                Usercontroller.LoadUsers();
            }
           
            
        }
        //Button para agregar canciones o peliculas a una playlist c/r si es de peliculas o canciones 
        private void AddSongToPlaylist_Click(object sender, EventArgs e)
        {
            
            //Revisar si hay alguna cancion seleccionada en el panel inferior
            if (TitleSongOrMovieLabel.Text == "Title (Song/Movie)" && BandOrStudioNameLabel.Text == "BandName/Studio")
            {
            }
            //Caso en que los labs son diferentes a la entrada de texto standar sin seleccion de pelicula o canción
            else
            {
                ShowPlaylistToAddSongOrMovie.Controls.Clear();
                OnAddSongOrMovieToPlaylistPanel.Visible = true;
                //Mostrar el subpanel layout el cual mostrara las playlist c/r a si es de una pelicula o cancion!
                //Identificación si es de pelicula o cancion
                if (LastFormatClicked.Identificador == "Song")
                {
                    //Mostrar las playlist de canciones
                    AddToPlaylistSongOrMovielbl.Text = "Agregar";
                    AddToPlaylistSongOrMovielbl.Text = AddToPlaylistSongOrMovielbl.Text + " canción a:";
                    Usercontroller.LoadUsers();
                    foreach (List<string> InfoPlaylistS in Usercontroller.ReturnPlaylistSInfoForUsr(LastUsername))
                    {
                        if("."+InfoPlaylistS[2] == Songscontroller.ReturnSongBySongNameAndBandName(TitleSongOrMovieLabel.Text, BandOrStudioNameLabel.Text).TypeFileS)
                        {
                            PlaylistFormatUserController playlistFormatUserController = new PlaylistFormatUserController();
                            playlistFormatUserController.PlaylistName = InfoPlaylistS[0];
                            playlistFormatUserController.OwnerUsername = InfoPlaylistS[1];
                            playlistFormatUserController.TypePlaylistStorage = InfoPlaylistS[2];
                            playlistFormatUserController.IdentificadorPlaylist = "Song";
                            try
                            {
                                playlistFormatUserController.UsernamePic = Image.FromFile(Usercontroller.ReturnUserByUserName(LastUsername).ProfilePicPath);
                            }
                            catch
                            {
                                playlistFormatUserController.UsernamePic = Image.FromFile("SampleProfile.jpg");
                            }
                            playlistFormatUserController.MouseMove += PlaylistFormatUserController_MouseMove;
                            playlistFormatUserController.MouseLeave += PlaylistFormatUserController_MouseLeave;
                            playlistFormatUserController.Click += PlaylistFormatUserController_Click1;
                            ShowPlaylistToAddSongOrMovie.Controls.Add(playlistFormatUserController);
                        }
                        
                        
                    }
                    
                }
                else if(LastFormatClicked.Identificador == "Movie")
                {
                    //Mostrar las playlist de peliculas
                    ShowPlaylistToAddSongOrMovie.Controls.Clear();
                    OnAddSongOrMovieToPlaylistPanel.Visible = true;
                    //Mostrar el subpanel layout el cual mostrara las playlist c/r a si es de una pelicula o cancion!
                    //Identificación si es de pelicula o cancion
                    //Mostrar las playlist de canciones
                    AddToPlaylistSongOrMovielbl.Text = "Agregar";
                    AddToPlaylistSongOrMovielbl.Text = AddToPlaylistSongOrMovielbl.Text + " pelicula a:";
                    Usercontroller.LoadUsers();
                    foreach (List<string> InfoPlaylistS in Usercontroller.ReturnMoviesPlaylistMInfoForUsr(LastUsername))
                    {
                        if ("." + InfoPlaylistS[2] == Moviecontroller.ReturnMovieByTitleNameAndStudioName(TitleSongOrMovieLabel.Text, BandOrStudioNameLabel.Text).TypeFileM)
                        {
                            PlaylistFormatUserController playlistFormatUserController = new PlaylistFormatUserController();
                            playlistFormatUserController.PlaylistName = InfoPlaylistS[0];
                            playlistFormatUserController.OwnerUsername = InfoPlaylistS[1];
                            playlistFormatUserController.TypePlaylistStorage = InfoPlaylistS[2];
                            playlistFormatUserController.IdentificadorPlaylist = "Movie";
                            try
                            {
                                playlistFormatUserController.UsernamePic = Image.FromFile(Usercontroller.ReturnUserByUserName(LastUsername).ProfilePicPath);
                            }
                            catch
                            {
                                playlistFormatUserController.UsernamePic = Image.FromFile("SampleProfile.jpg");
                            }
                            playlistFormatUserController.MouseMove += PlaylistFormatUserController_MouseMove;
                            playlistFormatUserController.MouseLeave += PlaylistFormatUserController_MouseLeave;
                            playlistFormatUserController.Click += PlaylistFormatUserController_Click1;
                            ShowPlaylistToAddSongOrMovie.Controls.Add(playlistFormatUserController);
                        }


                    }




                }
            }
        }
        //clikeo del formato , el cual va agregar canciones o peliculas a una playlist respectiva
        private void PlaylistFormatUserController_Click1(object sender, EventArgs e)
        {
            string Identificador = (sender as PlaylistFormatUserController).IdentificadorPlaylist;
            if(Identificador == "Song")
            {
                //Agrega la cancion selecciona a la playlist respectiva del usuario
                //Primero checkear si es que la cancion se encuentra ya en la PLaylist Respectiva
                bool CheckSongInPlySInUsr = Usercontroller.CheckSongInPlySInUsr((sender as PlaylistFormatUserController).PlaylistName, TitleSongOrMovieLabel.Text, BandOrStudioNameLabel.Text, LastUsername);
                if (CheckSongInPlySInUsr == false) 
                {
                    Usercontroller.AddSelectedSToPlySForUsr(TitleSongOrMovieLabel.Text, BandOrStudioNameLabel.Text, LastUsername, (sender as PlaylistFormatUserController).PlaylistName);
                    Usercontroller.SaveUsers();
                    MessageBox.Show("Canción agregada con exito a la playlist: " + (sender as PlaylistFormatUserController).PlaylistName);
                }
                else
                {
                    MessageBox.Show("Esta canción ya existe en la playlist seleccionada");
                }
                
            }
            else if(Identificador == "Movie")
            {
                //Agrega la cancion selecciona a la playlist respectiva del usuario
                //Primero checkear si es que la cancion se encuentra ya en la PLaylist Respectiva
                bool CheckMovieInPlySInUsr = Usercontroller.CheckMovieInPlyMInUsr((sender as PlaylistFormatUserController).PlaylistName, TitleSongOrMovieLabel.Text, BandOrStudioNameLabel.Text, LastUsername);
                if (CheckMovieInPlySInUsr == false)
                {
                    Usercontroller.AddSelectedMToPlyMForUsr(TitleSongOrMovieLabel.Text, BandOrStudioNameLabel.Text, LastUsername, (sender as PlaylistFormatUserController).PlaylistName);
                    Usercontroller.SaveUsers();
                    MessageBox.Show("Pelicula agregada con exito a la playlist: " + (sender as PlaylistFormatUserController).PlaylistName);
                }
                else
                {
                    MessageBox.Show("Esta pelicula ya existe en la playlist seleccionada");
                }
            }
            Usercontroller.LoadUsers();
        }

        //Botton de confirmación de la creación de la nueva playlist de canciones
        private void AddnewPlaylisSongConfirmationButton_Click(object sender, EventArgs e)
        {
            //Checkear si el nombre de la lista ya existe (solo bastan con el nombre de este)

            if(CheckTextBoxEmpty(PlaylistSongNameTextBox) || SongTypePlaylistSongComboBox.SelectedIndex ==-1)
            {
                MessageBox.Show("Porfavor rellene los datos necesarios para crear la playlist");
            }
            else
            {
                bool VerificadorDePlaylistSEnUsr = Usercontroller.CheckPLaylistSongInUsr(LastUsername, PlaylistSongNameTextBox.Text);
                //Se agrega la playlist al usr
                if (VerificadorDePlaylistSEnUsr == false)
                {
                    Usercontroller.AddNewPlaylisSToUsr(LastUsername, PlaylistSongNameTextBox.Text, SongTypePlaylistSongComboBox.SelectedItem.ToString());

                    MessageBox.Show("Playlist creada con exito");
                    PlaylistFormatUserController playlistFormatUserController = new PlaylistFormatUserController();
                    playlistFormatUserController.PlaylistName = PlaylistSongNameTextBox.Text;
                    playlistFormatUserController.TypePlaylistStorage = SongTypePlaylistSongComboBox.SelectedItem.ToString();
                    playlistFormatUserController.IdentificadorPlaylist = "PlyS";
                    playlistFormatUserController.OwnerUsername = LastUsername;

                    playlistFormatUserController.Click += PlaylistFormatUserController_Click;
                    playlistFormatUserController.MouseMove += PlaylistFormatUserController_MouseMove;
                    playlistFormatUserController.MouseLeave += PlaylistFormatUserController_MouseLeave;

                    string FileProfilePicfilename = Usercontroller.ReturnUserByUserName(LastUsername).ProfilePicPath;
                    try
                    {
                        playlistFormatUserController.UsernamePic = Image.FromFile(Path.Combine(DebugPath, FileProfilePicfilename));
                    }
                    catch
                    {
                        playlistFormatUserController.UsernamePic = Image.FromFile("SampleProfile.jpg");
                    }
                    

                    ShowPlaylistSongLayoutPanel.Controls.Add(playlistFormatUserController);
                }
                else
                {
                    MessageBox.Show("El nombre de esta nueva playlist ya existe!");
                }
            }

            


            
        }

        private void PlaylistFormatUserController_MouseLeave(object sender, EventArgs e)
        {
            (sender as PlaylistFormatUserController).BackColor = Color.FromArgb(64, 64, 64);
        }

        private void PlaylistFormatUserController_MouseMove(object sender, MouseEventArgs e)
        {
            (sender as PlaylistFormatUserController).BackColor = Color.FromArgb(105, 105, 105);
        }
        //Cuando se hace click a una playlist de canciones o peliculas 
        private void PlaylistFormatUserController_Click(object sender, EventArgs e)
        {
            string PlaylistNAME = (sender as PlaylistFormatUserController).PlaylistName;
            string Identificador = (sender as PlaylistFormatUserController).IdentificadorPlaylist;
            if (Identificador == "Song")
            {
                try
                {
                    ShowSongsInPlaylistLayoutPanel.Controls.Clear();
                    NamePlaylistClickedlbl.Text = "Canciones de la playlist:  ";
                    PlaylistLastFormatSongOrMovieClickedInfo.Visible = true;
                    PlaylistMoviePanel.Visible = false;
                    NamePlaylistClickedlbl.Text = NamePlaylistClickedlbl.Text + PlaylistNAME;
                    foreach (List<string> InfoSongsInPlaylist in Usercontroller.ReturnSongsInfoFromClickedPlaylistUSR(LastUsername, PlaylistNAME))
                    {
                        SongMovieFormatShow songMovieFormatShow = new SongMovieFormatShow();
                        songMovieFormatShow.Titulo = InfoSongsInPlaylist[0];
                        songMovieFormatShow.NombreBanda = InfoSongsInPlaylist[1];
                        songMovieFormatShow.Identificador = "Song";
                        try
                        {
                            songMovieFormatShow.AlbumImage = Image.FromFile(InfoSongsInPlaylist[2]);
                        }
                        catch
                        {
                            songMovieFormatShow.AlbumImage = Image.FromFile("SampleAlbumImage.jpg");
                        }
                        ShowSongsInPlaylistLayoutPanel.Controls.Add(songMovieFormatShow);
                        LastFormatClicked = songMovieFormatShow;
                        songMovieFormatShow.Click += songMovieFormatShow_Click;
                        songMovieFormatShow.MouseMove += OnMouseMoveOnBotton;
                        songMovieFormatShow.MouseLeave += SongsFormat_MouseLeave;
                    }
                }
                catch
                {
                }
                
            }
            else if(Identificador == "Movie")
            {
                try
                {
                    ShowSongsInPlaylistLayoutPanel.Controls.Clear();
                    NamePlaylistClickedlbl.Text = "Peliculas de la playlist:  ";
                    PlaylistLastFormatSongOrMovieClickedInfo.Visible = true;
                    PlaylistMoviePanel.Visible = false;
                    NamePlaylistClickedlbl.Text = NamePlaylistClickedlbl.Text + PlaylistNAME;
                    foreach (List<string> InfoMoviesInPlaylistM in Usercontroller.ReturnMoviesInfoFromClickedPlaylistMUSR(LastUsername, PlaylistNAME))
                    {
                        SongMovieFormatShow songMovieFormatShow = new SongMovieFormatShow();
                        songMovieFormatShow.Titulo = InfoMoviesInPlaylistM[0];
                        songMovieFormatShow.NombreBanda = InfoMoviesInPlaylistM[1];
                        songMovieFormatShow.Identificador = "Movie";
                        try
                        {
                            songMovieFormatShow.AlbumImage = Image.FromFile(InfoMoviesInPlaylistM[2]);
                        }
                        catch
                        {
                            songMovieFormatShow.AlbumImage = Image.FromFile("SampleAlbumImage.jpg");
                        }
                        ShowSongsInPlaylistLayoutPanel.Controls.Add(songMovieFormatShow);
                        LastFormatClicked = songMovieFormatShow;
                        songMovieFormatShow.Click += songMovieFormatShow_Click;
                        songMovieFormatShow.MouseMove += OnMouseMoveOnBotton;
                        songMovieFormatShow.MouseLeave += SongsFormat_MouseLeave;
                    }
                }
                catch
                {
                }
            }
        }

        private void HideAddSongOrMoviePanelButton_Click(object sender, EventArgs e)
        {
            OnAddSongOrMovieToPlaylistPanel.Visible = false;
        }

        private void AddNewPlaylistMConfirmationButton_Click(object sender, EventArgs e)
        {
            //Checkear si el nombre de la lista ya existe (solo bastan con el nombre de este)

            if (CheckTextBoxEmpty(NameNewPlaylistMTextBox) || SelectFiletypeNewPlyMComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Porfavor rellene los datos necesarios para crear la playlist");
            }
            else
            {
                bool VerificadorDePlaylistSEnUsr = Usercontroller.CheckPLaylistMovieInUsr(LastUsername, NameNewPlaylistMTextBox.Text);
                //Se agrega la playlist al usr
                if (VerificadorDePlaylistSEnUsr == false)
                {
                    Usercontroller.AddNewPlaylisMToUsr(LastUsername, NameNewPlaylistMTextBox.Text, SelectFiletypeNewPlyMComboBox.SelectedItem.ToString());

                    MessageBox.Show("Playlist creada con exito");
                    PlaylistFormatUserController playlistFormatUserController = new PlaylistFormatUserController();
                    playlistFormatUserController.PlaylistName = NameNewPlaylistMTextBox.Text;
                    playlistFormatUserController.TypePlaylistStorage = SelectFiletypeNewPlyMComboBox.SelectedItem.ToString();
                    playlistFormatUserController.IdentificadorPlaylist = "PlyM";
                    playlistFormatUserController.OwnerUsername = LastUsername;

                    playlistFormatUserController.Click += PlaylistFormatUserController_Click;
                    playlistFormatUserController.MouseMove += PlaylistFormatUserController_MouseMove;
                    playlistFormatUserController.MouseLeave += PlaylistFormatUserController_MouseLeave;

                    string FileProfilePicfilename = Usercontroller.ReturnUserByUserName(LastUsername).ProfilePicPath;
                    try
                    {
                        playlistFormatUserController.UsernamePic = Image.FromFile(Path.Combine(DebugPath, FileProfilePicfilename));
                    }
                    catch
                    {
                        playlistFormatUserController.UsernamePic = Image.FromFile("SampleProfile.jpg");
                    }


                    ShowPlaylistMUsrLayoutPanel.Controls.Add(playlistFormatUserController);
                }
                else
                {
                    MessageBox.Show("El nombre de esta nueva playlist ya existe!");
                }
            }








        }
        //Muestra los trabajadores que participan en esa cancion o pelicula 
        private void ShowInfoSongOrMovieBotton_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.status.Length == 0)
            {
                
            }
            else
            {
                ShowWorkersFromSelectedMovieOrSongLayoutPanel.Controls.Clear();
                ShowMoreInfoNameSongOrMovielbl.Text = "";
                ShowMoreInfoBandOrStudioNamelbl.Text = "";
                //Mostrar panel que muestra la info de la cancion o pelicula clickeada
                string Identificador = LastFormatClicked.Identificador;

                if(Identificador == "Song")
                {
                    ShowMoreInfoNameSongOrMovielbl.Text = "Nombre: "+ TitleSongOrMovieLabel.Text;
                    ShowMoreInfoBandOrStudioNamelbl.Text = "Banda: " + BandOrStudioNameLabel.Text;
                    foreach(List<string> InfoS in Songscontroller.ReturnWorkersSFromSelectedSong(TitleSongOrMovieLabel.Text, BandOrStudioNameLabel.Text))
                    {
                        WorkerFormatUserController workerFormat = new WorkerFormatUserController();
                        workerFormat.NombreW = InfoS[0];
                        workerFormat.RolW = InfoS[1];
                        workerFormat.EdadW = InfoS[2];
                        workerFormat.Sexo = InfoS[3];
                        workerFormat.RankingW = InfoS[4];
                        workerFormat.Titulo = TitleSongOrMovieLabel.Text;
                        workerFormat.BandaOEstudio = BandOrStudioNameLabel.Text;
                        workerFormat.Identificador = "Song";
                        workerFormat.MouseMove += WorkerFormat_MouseMove; ;
                        workerFormat.MouseLeave += WorkerFormat_MouseLeave; ;
                        ShowWorkersFromSelectedMovieOrSongLayoutPanel.Controls.Add(workerFormat);
                    }
                }
                //Movie
                else
                {
                    ShowMoreInfoNameSongOrMovielbl.Text = "Nombre: " + TitleSongOrMovieLabel.Text;
                    ShowMoreInfoBandOrStudioNamelbl.Text = "Estudio: " + BandOrStudioNameLabel.Text;
                    ShowMoreInfoNameSongOrMovielbl.Text = "Nombre: " + TitleSongOrMovieLabel.Text;
                    ShowMoreInfoBandOrStudioNamelbl.Text = "Banda: " + BandOrStudioNameLabel.Text;
                    foreach (List<string> InfoS in Moviecontroller.ReturnWorkersMFromSelectedMovie(TitleSongOrMovieLabel.Text, BandOrStudioNameLabel.Text))
                    {
                        WorkerFormatUserController workerFormat = new WorkerFormatUserController();
                        workerFormat.NombreW = InfoS[0];
                        workerFormat.RolW = InfoS[1];
                        workerFormat.EdadW = InfoS[2];
                        workerFormat.Sexo = InfoS[3];
                        workerFormat.RankingW = InfoS[4];
                        workerFormat.Identificador = "Movie";
                        workerFormat.Titulo = TitleSongOrMovieLabel.Text;
                        workerFormat.BandaOEstudio = BandOrStudioNameLabel.Text;
                        workerFormat.MouseMove += WorkerFormat_MouseMove;
                        workerFormat.MouseLeave += WorkerFormat_MouseLeave;
                        
                        ShowWorkersFromSelectedMovieOrSongLayoutPanel.Controls.Add(workerFormat);
                    }
                }
                ShowInfoWorkerFromSongOrMovieSelectedPanel.Visible = true;
                PlaylistMoviePanel.Visible = true;
                PlaylistLastFormatSongOrMovieClickedInfo.Visible = true;
                PlaylistSongPanel.Visible = true;
                FavMoviesUsrPanel.Visible = true;
                FavSongsUsrPanel.Visible = true;
                SearchResultPanel.Visible = true;
                ConfigurationPanel.Visible = true;


            }
        }

        private void WorkerFormat_MouseLeave(object sender, EventArgs e)
        {
            (sender as WorkerFormatUserController).BackColor = Color.FromArgb(64, 64, 64);
        }

        private void WorkerFormat_MouseMove(object sender, MouseEventArgs e)
        {
            (sender as WorkerFormatUserController).BackColor = Color.FromArgb(105, 105, 105);
        }

    }
    

        
           
        
    


}
