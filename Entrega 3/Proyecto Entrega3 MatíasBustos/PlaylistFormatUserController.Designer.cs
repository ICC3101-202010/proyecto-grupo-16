namespace ProyectoEntrega3MatíasBustos
{
    partial class PlaylistFormatUserController
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
            this.OwnerUsernamelbl = new System.Windows.Forms.Label();
            this.PlaylistSongNamelbl = new System.Windows.Forms.Label();
            this.OwnerUserPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.OwnerUserPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OwnerUsernamelbl
            // 
            this.OwnerUsernamelbl.AutoSize = true;
            this.OwnerUsernamelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OwnerUsernamelbl.ForeColor = System.Drawing.Color.White;
            this.OwnerUsernamelbl.Location = new System.Drawing.Point(93, 58);
            this.OwnerUsernamelbl.Name = "OwnerUsernamelbl";
            this.OwnerUsernamelbl.Size = new System.Drawing.Size(124, 18);
            this.OwnerUsernamelbl.TabIndex = 4;
            this.OwnerUsernamelbl.Text = "OwnerUserName";
            // 
            // PlaylistSongNamelbl
            // 
            this.PlaylistSongNamelbl.AutoSize = true;
            this.PlaylistSongNamelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlaylistSongNamelbl.ForeColor = System.Drawing.Color.White;
            this.PlaylistSongNamelbl.Location = new System.Drawing.Point(93, 24);
            this.PlaylistSongNamelbl.Name = "PlaylistSongNamelbl";
            this.PlaylistSongNamelbl.Size = new System.Drawing.Size(99, 20);
            this.PlaylistSongNamelbl.TabIndex = 3;
            this.PlaylistSongNamelbl.Text = "PlaylistName";
            // 
            // OwnerUserPictureBox
            // 
            this.OwnerUserPictureBox.Location = new System.Drawing.Point(23, 17);
            this.OwnerUserPictureBox.Name = "OwnerUserPictureBox";
            this.OwnerUserPictureBox.Size = new System.Drawing.Size(64, 59);
            this.OwnerUserPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.OwnerUserPictureBox.TabIndex = 5;
            this.OwnerUserPictureBox.TabStop = false;
            // 
            // PlaylistFormatUserController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.OwnerUserPictureBox);
            this.Controls.Add(this.OwnerUsernamelbl);
            this.Controls.Add(this.PlaylistSongNamelbl);
            this.Name = "PlaylistFormatUserController";
            this.Size = new System.Drawing.Size(273, 99);
            ((System.ComponentModel.ISupportInitialize)(this.OwnerUserPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label OwnerUsernamelbl;
        private System.Windows.Forms.Label PlaylistSongNamelbl;
        private System.Windows.Forms.PictureBox OwnerUserPictureBox;
    }
}
