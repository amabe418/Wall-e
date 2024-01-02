namespace Walle
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Ejecutar = new Walle.CustomButton();
            this.Restaurar = new Walle.CustomButton();
            this.SalidaDeEvaluacion = new Walle.CustomTextBox();
            this.EntradaDeCodigo = new Walle.CustomTextBox();
            this.Lienzo = new Walle.CustomPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(111, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 34);
            this.label1.TabIndex = 6;
            this.label1.Text = "Input";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(359, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Console";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Mongolian Baiti", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1281, 69);
            this.label3.TabIndex = 8;
            this.label3.Text = "GEOWALL-E";
            this.label3.UseMnemonic = false;
            // 
            // Ejecutar
            // 
            this.Ejecutar.BackColor = System.Drawing.Color.White;
            this.Ejecutar.BackgroundColor = System.Drawing.Color.White;
            this.Ejecutar.BorderColor = System.Drawing.Color.White;
            this.Ejecutar.BorderRadius = 70;
            this.Ejecutar.BorderSize = 2;
            this.Ejecutar.FlatAppearance.BorderSize = 0;
            this.Ejecutar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ejecutar.ForeColor = System.Drawing.Color.White;
            this.Ejecutar.Image = ((System.Drawing.Image)(resources.GetObject("Ejecutar.Image")));
            this.Ejecutar.Location = new System.Drawing.Point(576, 271);
            this.Ejecutar.Name = "Ejecutar";
            this.Ejecutar.Size = new System.Drawing.Size(100, 88);
            this.Ejecutar.TabIndex = 5;
            this.Ejecutar.TextColor = System.Drawing.Color.White;
            this.Ejecutar.UseVisualStyleBackColor = false;
            this.Ejecutar.Click += new System.EventHandler(this.Ejecutar_Click);
            // 
            // Restaurar
            // 
            this.Restaurar.BackColor = System.Drawing.Color.White;
            this.Restaurar.BackgroundColor = System.Drawing.Color.White;
            this.Restaurar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Restaurar.BackgroundImage")));
            this.Restaurar.BorderColor = System.Drawing.Color.White;
            this.Restaurar.BorderRadius = 70;
            this.Restaurar.BorderSize = 2;
            this.Restaurar.FlatAppearance.BorderSize = 0;
            this.Restaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Restaurar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Restaurar.ForeColor = System.Drawing.Color.White;
            this.Restaurar.ImageKey = "(none)";
            this.Restaurar.Location = new System.Drawing.Point(576, 416);
            this.Restaurar.Name = "Restaurar";
            this.Restaurar.Size = new System.Drawing.Size(100, 88);
            this.Restaurar.TabIndex = 4;
            this.Restaurar.TabStop = false;
            this.Restaurar.TextColor = System.Drawing.Color.White;
            this.Restaurar.UseVisualStyleBackColor = false;
            this.Restaurar.Click += new System.EventHandler(this.Restaurar_Click);
            // 
            // SalidaDeEvaluacion
            // 
            this.SalidaDeEvaluacion.AutoSize = true;
            this.SalidaDeEvaluacion.BackColor = System.Drawing.Color.White;
            this.SalidaDeEvaluacion.BorderColor = System.Drawing.Color.Transparent;
            this.SalidaDeEvaluacion.BorderRadius = 15;
            this.SalidaDeEvaluacion.BorderSize = 8;
            this.SalidaDeEvaluacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SalidaDeEvaluacion.ForeColor = System.Drawing.Color.Black;
            this.SalidaDeEvaluacion.Location = new System.Drawing.Point(285, 134);
            this.SalidaDeEvaluacion.Margin = new System.Windows.Forms.Padding(4, 30, 4, 30);
            this.SalidaDeEvaluacion.Multiline = true;
            this.SalidaDeEvaluacion.Name = "SalidaDeEvaluacion";
            this.SalidaDeEvaluacion.Padding = new System.Windows.Forms.Padding(10, 40, 10, 42);
            this.SalidaDeEvaluacion.PasswordChar = false;
            this.SalidaDeEvaluacion.ScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.SalidaDeEvaluacion.Size = new System.Drawing.Size(267, 506);
            this.SalidaDeEvaluacion.TabIndex = 3;
            this.SalidaDeEvaluacion.Texts = "";
            this.SalidaDeEvaluacion.UnderlinedStyle = false;
            // 
            // EntradaDeCodigo
            // 
            this.EntradaDeCodigo.AutoSize = true;
            this.EntradaDeCodigo.BackColor = System.Drawing.Color.White;
            this.EntradaDeCodigo.BorderColor = System.Drawing.Color.White;
            this.EntradaDeCodigo.BorderRadius = 15;
            this.EntradaDeCodigo.BorderSize = 8;
            this.EntradaDeCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntradaDeCodigo.ForeColor = System.Drawing.Color.Black;
            this.EntradaDeCodigo.Location = new System.Drawing.Point(31, 135);
            this.EntradaDeCodigo.Margin = new System.Windows.Forms.Padding(4, 30, 4, 30);
            this.EntradaDeCodigo.Multiline = true;
            this.EntradaDeCodigo.Name = "EntradaDeCodigo";
            this.EntradaDeCodigo.Padding = new System.Windows.Forms.Padding(10, 40, 10, 42);
            this.EntradaDeCodigo.PasswordChar = false;
            this.EntradaDeCodigo.ScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.EntradaDeCodigo.Size = new System.Drawing.Size(246, 506);
            this.EntradaDeCodigo.TabIndex = 2;
            this.EntradaDeCodigo.Texts = "";
            this.EntradaDeCodigo.UnderlinedStyle = false;
            // 
            // Lienzo
            // 
            this.Lienzo.BackColor = System.Drawing.Color.White;
            this.Lienzo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Lienzo.BackgroundImage")));
            this.Lienzo.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.Lienzo.BorderColor = System.Drawing.Color.Transparent;
            this.Lienzo.BorderColor2 = System.Drawing.Color.LightSeaGreen;
            this.Lienzo.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.Lienzo.BorderRadius = 25;
            this.Lienzo.BorderSize = 10;
            this.Lienzo.Enabled = false;
            this.Lienzo.GradientAngle = 50F;
            this.Lienzo.Location = new System.Drawing.Point(699, 134);
            this.Lienzo.Name = "Lienzo";
            this.Lienzo.Size = new System.Drawing.Size(554, 507);
            this.Lienzo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Lienzo.TabIndex = 1;
            this.Lienzo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1281, 680);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ejecutar);
            this.Controls.Add(this.Restaurar);
            this.Controls.Add(this.SalidaDeEvaluacion);
            this.Controls.Add(this.EntradaDeCodigo);
            this.Controls.Add(this.Lienzo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "GeoWall-e";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public CustomPictureBox Lienzo;
        public CustomTextBox EntradaDeCodigo;
        public CustomTextBox SalidaDeEvaluacion;
        private CustomButton Restaurar;
        private CustomButton Ejecutar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

