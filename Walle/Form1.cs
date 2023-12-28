using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSharpInterpreter;

namespace Walle
{
    public partial class Form1 : Form, IUserInterface
    {
        public int CanvasWidth => Lienzo.Width;

        public int CanvasHeight => Lienzo.Height;

        public Form1()
        {
            InitializeComponent();
        }

        private void CustomButton2_Click(object sender, EventArgs e)
        {
            //Reinicia el lienzo
            this.Lienzo.Refresh();
            //Guarda la informacion del TextBox donde se escribe el codigo
            string code = EntradaDeCodigo.Text;
            Interpreter.Execute(code, this);
            SalidaDeEvaluacion.Texts = "prueba";
            using (Graphics graph = Lienzo.CreateGraphics())
            using (Pen pen = new Pen(Color.Black, 2F))
            {
                graph.DrawLine(pen, 320, 260,310, 230);

            }

        }

        private Color GetColor(GSharpColor color)
        {
            switch (color)
            {
                case GSharpColor.BLACK:
                    return Color.Black;
                case GSharpColor.BLUE:
                    return Color.Blue;
                case GSharpColor.CYAN:
                    return Color.Cyan;
                case GSharpColor.GRAY:
                    return Color.Gray;
                case GSharpColor.GREEN:
                    return Color.Green;
                case GSharpColor.MAGENTA:
                    return Color.Magenta;
                case GSharpColor.RED:
                    return Color.Red;
                case GSharpColor.WHITE:
                    return Color.White;
                case GSharpColor.YELLOW:
                    return Color.Yellow;
                default:
                    return Color.Black;
            }
        }


        public void DrawPoint(GSharpInterpreter.Point point, GSharpColor color)
        {
            using (Graphics graph = Lienzo.CreateGraphics())
            using (Pen pen = new Pen(GetColor(color), 4F))
            {
                graph.DrawEllipse(pen, (float)point.X, (float)point.Y, 6F, 6F);
            }

        }

        public void DrawLine(Line line, GSharpColor color)
        {
            //el objeto linea ya viene con pendiente.
            if (line.P1.X == line.P2.X) //pendiente infinita
            {
                throw new NotImplementedException();
            }
            else // pendiente finita
            {
                Point leftExtreme = new Point(0, (float)(line.P1.Y + (0 - line.P1.X) * GetSlope(line.P1, line.P2)));//extremo izquierdo
                Point rightExtreme = new Point(this.Width, (float)(line.P1.Y + (this.Width - line.P1.X) * GetSlope(line.P1, line.P2))); //estremo derecho
                using (Pen pen = new Pen(GetColor(color)))
                using (Graphics graph = Lienzo.CreateGraphics())
                {
                    graph.DrawLine(pen, (float)leftExtreme.X, (float)leftExtreme.Y, (float)rightExtreme.X, (float)rightExtreme.Y);
                }
            }

            throw new NotImplementedException();
        }

        public void DrawSegment(Segment segment, GSharpColor color)
        {
            using (Graphics graph = Lienzo.CreateGraphics())
            using (Pen pen = new Pen(GetColor(color), 4F))
            {
                graph.DrawLine(pen, (float)segment.P1.X, (float)segment.P1.Y, (float)segment.P2.X, (float)segment.P2.Y);
            }

        }

        public void DrawRay(Ray ray, GSharpColor color)
        {
            throw new NotImplementedException();
        }

        public void DrawCircle(Circle circle, GSharpColor color)
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(GetColor(color));
                g.DrawEllipse(pen, (float)circle.Center.X - (float)circle.Radius.Value, (float)(circle.Center.Y - circle.Radius.Value), (float)circle.Radius.Value * 2, (float)circle.Radius.Value * 2);
            }
        }

        public void DrawArc(Arc arc, GSharpColor color)
        {
            /* using (Graphics g = this.CreateGraphics())
             {
                 Pen pen = new Pen(GetColor(color));
                 g.DrawArc(pen, (float)(arc.Center.X - arc.Radius.Value), (float)(arc.Center.Y - arc.Radius.Value), (float)arc.Radius.Value * 2, (float)(arc.Radius.Value * 2), (float)(arc.InitialRayPoint), (float)(arc.EndAngle - arc.StartAngle));
             }*/
            throw new NotImplementedException();
        }

        public void DrawText(string text, GSharpInterpreter.Point point, GSharpColor color)
        {
            throw new NotImplementedException();
        }

        public void Print(string text)
        {
           
                SalidaDeEvaluacion.Texts += text + "\n";
            
        }
        public void ReportError(string message)
        {
            SalidaDeEvaluacion.Texts += message + "\n";
        }


        private float GetSlope(GSharpInterpreter.Point p1, GSharpInterpreter.Point p2)
        {
            return (float)((p2.X - p1.X) / (p2.Y - p1.Y));
        }

    }
}
