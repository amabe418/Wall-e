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
            this.SalidaDeEvaluacion = new Walle.CustomTextBox();
            this.EntradaDeCodigo = new Walle.CustomTextBox();
            this.Lienzo = new Walle.CustomPictureBox();
            this.Ejecutar = new Walle.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).BeginInit();
            this.SuspendLayout();
            // 
            // SalidaDeEvaluacion
            // 
            this.SalidaDeEvaluacion.AutoSize = true;
            this.SalidaDeEvaluacion.BackColor = System.Drawing.Color.White;
            this.SalidaDeEvaluacion.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.SalidaDeEvaluacion.BorderRadius = 15;
            this.SalidaDeEvaluacion.BorderSize = 8;
            this.SalidaDeEvaluacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SalidaDeEvaluacion.Location = new System.Drawing.Point(293, 33);
            this.SalidaDeEvaluacion.Margin = new System.Windows.Forms.Padding(4);
            this.SalidaDeEvaluacion.Multiline = true;
            this.SalidaDeEvaluacion.Name = "SalidaDeEvaluacion";
            this.SalidaDeEvaluacion.Padding = new System.Windows.Forms.Padding(15, 7, 15, 7);
            this.SalidaDeEvaluacion.PasswordChar = false;
            this.SalidaDeEvaluacion.Size = new System.Drawing.Size(217, 335);
            this.SalidaDeEvaluacion.TabIndex = 3;
            this.SalidaDeEvaluacion.Texts = "";
            this.SalidaDeEvaluacion.UnderlinedStyle = false;
            // 
            // EntradaDeCodigo
            // 
            this.EntradaDeCodigo.AutoSize = true;
            this.EntradaDeCodigo.BackColor = System.Drawing.Color.White;
            this.EntradaDeCodigo.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.EntradaDeCodigo.BorderRadius = 15;
            this.EntradaDeCodigo.BorderSize = 8;
            this.EntradaDeCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntradaDeCodigo.Location = new System.Drawing.Point(50, 33);
            this.EntradaDeCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.EntradaDeCodigo.Multiline = true;
            this.EntradaDeCodigo.Name = "EntradaDeCodigo";
            this.EntradaDeCodigo.Padding = new System.Windows.Forms.Padding(50);
            this.EntradaDeCodigo.PasswordChar = false;
            this.EntradaDeCodigo.Size = new System.Drawing.Size(213, 335);
            this.EntradaDeCodigo.TabIndex = 2;
            this.EntradaDeCodigo.Texts = "";
            this.EntradaDeCodigo.UnderlinedStyle = false;
            // 
            // Lienzo
            // 
            this.Lienzo.BackColor = System.Drawing.Color.White;
            this.Lienzo.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.Lienzo.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.Lienzo.BorderColor2 = System.Drawing.Color.LightSeaGreen;
            this.Lienzo.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.Lienzo.BorderRadius = 25;
            this.Lienzo.BorderSize = 10;
            this.Lienzo.GradientAngle = 50F;
            this.Lienzo.Location = new System.Drawing.Point(541, 33);
            this.Lienzo.Name = "Lienzo";
            this.Lienzo.Size = new System.Drawing.Size(471, 471);
            this.Lienzo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Lienzo.TabIndex = 1;
            this.Lienzo.TabStop = false;
            // 
            // Ejecutar
            // 
            this.Ejecutar.AutoSize = true;
            this.Ejecutar.BackColor = System.Drawing.Color.Turquoise;
            this.Ejecutar.BackgroundColor = System.Drawing.Color.Turquoise;
            this.Ejecutar.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.Ejecutar.BorderRadius = 35;
            this.Ejecutar.BorderSize = 2;
            this.Ejecutar.FlatAppearance.BorderSize = 0;
            this.Ejecutar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ejecutar.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ejecutar.ForeColor = System.Drawing.Color.White;
            this.Ejecutar.Location = new System.Drawing.Point(153, 408);
            this.Ejecutar.Name = "Ejecutar";
            this.Ejecutar.Size = new System.Drawing.Size(257, 83);
            this.Ejecutar.TabIndex = 0;
            this.Ejecutar.Text = "Ejecutar";
            this.Ejecutar.TextColor = System.Drawing.Color.White;
            this.Ejecutar.UseVisualStyleBackColor = false;
            this.Ejecutar.Click += new System.EventHandler(this.CustomButton2_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1088, 584);
            this.Controls.Add(this.SalidaDeEvaluacion);
            this.Controls.Add(this.EntradaDeCodigo);
            this.Controls.Add(this.Lienzo);
            this.Controls.Add(this.Ejecutar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        public CustomButton Ejecutar;
        public CustomPictureBox Lienzo;
        public CustomTextBox EntradaDeCodigo;
        public CustomTextBox SalidaDeEvaluacion;

       
    }
}

