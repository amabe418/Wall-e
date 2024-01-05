using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Walle
{
    [DefaultEvent("_TextChanged")]
    public partial class CustomTextBox : UserControl
    {
        //fields
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 2;
        private bool underlinedStyle = false;
      
        private int borderRadius = 20;

        //Constructor
        public CustomTextBox()
        {
            InitializeComponent();
        }
        //events
        public event EventHandler _TextChanged;

        //Properties
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        public int BorderSize
        {
            get => borderSize;
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        public bool UnderlinedStyle
        {
            get => underlinedStyle;
            set
            {
                underlinedStyle = value;
                this.Invalidate();
            }
        }
        public bool PasswordChar
        {
            get
            {
                return textBox1.UseSystemPasswordChar;
            }
            set
            {
                textBox1.UseSystemPasswordChar = value;
            }
        }
        public AutoScaleMode ScaleMode
        {
            get
            {
                return AutoScaleMode;
            }
            set
            {
                AutoScaleMode = value;
            }

        }
        public bool Multiline
        {
            get
            {
                return textBox1.Multiline;
            }
            set
            {
                textBox1.Multiline = value;
            }
        }


        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                textBox1.BackColor = value;
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                textBox1.ForeColor = value;
            }
        }

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                textBox1.Font = value;
                if (DesignMode)
                    UpdateControlHeight();
            }
        }

        public string Texts
        {
            get => textBox1.Text;
            set
            {
                textBox1.Text = value;
            }

        }

        public int BorderRadius
        {
            get => borderRadius;

            set
            {
                if (value > 0)
                {
                    borderRadius = value;
                    this.Invalidate();
                }
            }
        }




        //overriden methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Define the rounding degree for the corners
            int cornerRadius = borderRadius;

            using (GraphicsPath path = new GraphicsPath())
            {
                int width = this.Width;
                int height = this.Height;

                // Add arcs to the path for each corner
                path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90); // Top left
                path.AddArc(width - (cornerRadius * 2), 0, cornerRadius * 2, cornerRadius * 2, 270, 90); // Top right
                path.AddArc(width - (cornerRadius * 2), height - (cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 0, 90); // Bottom right
                path.AddArc(0, height - (cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 90, 90); // Bottom left
                path.CloseFigure();

                // Set the clipping region to create the rounded area
                e.Graphics.SetClip(path);


                // Set the control's Region to the rounded rectangle path
                this.Region = new Region(path);
                // Draw the image within the clipped area
                // e.Graphics.DrawImage(this.Image, 0, 0, this.Width, this.Height);

                // Draw the rounded rectangle border
                using (Pen borderPen = new Pen(borderColor, borderSize))
                {
                    e.Graphics.DrawPath(borderPen, path);
                }
            }
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if(this.DesignMode)
             UpdateControlHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }
        //Private method
        private void UpdateControlHeight()
        {
            if (textBox1.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.MinimumSize = new Size(0, txtHeight);
                textBox1.Multiline = false;

                this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (_TextChanged != null)
                _TextChanged.Invoke(sender, e);
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void CustomTextBox_Load(object sender, EventArgs e)
        {

        }
    }
}
