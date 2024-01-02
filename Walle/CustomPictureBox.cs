using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.ComponentModel;



namespace Walle
{
   public  class CustomPictureBox: PictureBox
    {
        //fields
        private int borderSize = 2;
        private int borderRadius = 40;
        private Color borderColor = Color.PaleVioletRed;
        private Color borderColor2 = Color.HotPink;
        private DashStyle borderLineStyle = DashStyle.Solid;
        private DashCap borderCapStyle = DashCap.Flat;
        private float gradientAngle = 50F;

        //constructor
        public CustomPictureBox()
        {
            this.Size = new Size(100, 100);
            this.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        //Properties
        public int BorderSize
        {
            get => borderSize;
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        public int BorderRadius
        {
            get => borderRadius;
            set 
                {
                borderRadius = value;
                this.Invalidate();
            }
        }
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                this.Invalidate();
            }
            }
        public Color BorderColor2
        {
            get => borderColor2;
            set
            {
                borderColor2 = value;
                this.Invalidate();
            }
            }
        public DashStyle BorderLineStyle
        {
            get => borderLineStyle;
            set
            {
                borderLineStyle = value;
                this.Invalidate();
            }
            }
        public DashCap BorderCapStyle
        {
            get => borderCapStyle;
            set
            {
                borderCapStyle = value;
                this.Invalidate();
            }
        }
        public float GradientAngle
        {
            get => gradientAngle;
            set
            {
                gradientAngle = value;
                this.Invalidate();
            }
        }
      

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new Size(this.Width, this.Height);

        }

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

      

    }
}
