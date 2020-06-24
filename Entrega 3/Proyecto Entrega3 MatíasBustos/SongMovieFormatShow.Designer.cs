namespace ProyectoEntrega3MatíasBustos
{
    partial class SongMovieFormatShow
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.AlbumImagePictureBox = new System.Windows.Forms.PictureBox();
            this.TitleSongLbl = new System.Windows.Forms.Label();
            this.BandLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AlbumImagePictureBox
            // 
            this.AlbumImagePictureBox.Location = new System.Drawing.Point(14, 12);
            this.AlbumImagePictureBox.Name = "AlbumImagePictureBox";
            this.AlbumImagePictureBox.Size = new System.Drawing.Size(64, 59);
            this.AlbumImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AlbumImagePictureBox.TabIndex = 0;
            this.AlbumImagePictureBox.TabStop = false;
            // 
            // TitleSongLbl
            // 
            this.TitleSongLbl.AutoSize = true;
            this.TitleSongLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleSongLbl.ForeColor = System.Drawing.Color.White;
            this.TitleSongLbl.Location = new System.Drawing.Point(93, 21);
            this.TitleSongLbl.Name = "TitleSongLbl";
            this.TitleSongLbl.Size = new System.Drawing.Size(35, 18);
            this.TitleSongLbl.TabIndex = 1;
            this.TitleSongLbl.Text = "Title";
            // 
            // BandLbl
            // 
            this.BandLbl.AutoSize = true;
            this.BandLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BandLbl.ForeColor = System.Drawing.Color.White;
            this.BandLbl.Location = new System.Drawing.Point(93, 55);
            this.BandLbl.Name = "BandLbl";
            this.BandLbl.Size = new System.Drawing.Size(40, 16);
            this.BandLbl.TabIndex = 2;
            this.BandLbl.Text = "Band";
            // 
            // SongMovieFormatShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.BandLbl);
            this.Controls.Add(this.TitleSongLbl);
            this.Controls.Add(this.AlbumImagePictureBox);
            this.Name = "SongMovieFormatShow";
            this.Size = new System.Drawing.Size(280, 89);
            this.Click += new System.EventHandler(this.SongsFormat_Click);
            ((System.ComponentModel.ISupportInitialize)(this.AlbumImagePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AlbumImagePictureBox;
        private System.Windows.Forms.Label TitleSongLbl;
        private System.Windows.Forms.Label BandLbl;
    }
}
