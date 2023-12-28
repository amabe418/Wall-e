using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G__Interpreter;

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
            //SalidaDeEvaluacion.Texts = "prueba";
           /* using (Graphics graph = Lienzo.CreateGraphics())
            using (Pen pen = new Pen(Color.Black, 2F))
            {
                graph.DrawLine(pen, 320, 260,310, 230);

            }*/

        }

        public void DrawPoint(Figure.Point point, Color color)
        {
            using (Graphics graph = Lienzo.CreateGraphics())
            using (Pen pen = new Pen(color, 4F))
            {
                graph.DrawEllipse(pen, (float)point.X, (float)point.Y, 6F, 6F);
            }
            
        }

        public void DrawLine(Figure.Line line, Color color)// dibujar una linea de extremo a extremo de la pantalla
        {
            //el objeto linea ya viene con pendiente.
            if (line.P1.X == line.P2.X) //pendiente infinita
            {
                throw new NotImplementedException();
            }
            else // pendiente finita
            {
              Point leftExtreme = new Point(0, (float)(line.P1.Y + (0 - line.P1.X) * line.Slope));//extremo izquierdo
              Point rightExtreme = new Point(this.Width, (float)(line.P1.Y + (this.Width - line.P1.X) * line.Slope)); //estremo derecho
                using (Pen pen = new Pen(color))
                using(Graphics graph = Lienzo.CreateGraphics())
                {
                    graph.DrawLine(pen, (float)leftExtreme.X, (float)leftExtreme.Y, (float)rightExtreme.X, (float)rightExtreme.Y);
                }
            }

            throw new NotImplementedException();
        }

        public void DrawSegment(Figure.Segment segment, Color color)
        {
            using (Graphics graph = Lienzo.CreateGraphics())
            using (Pen pen = new Pen(color, 4F))
            {
                graph.DrawLine(pen, (float)segment.P1.X, (float)segment.P1.Y, (float)segment.P2.X, (float)segment.P2.Y);
            }

        }

        public void DrawRay(Figure.Ray ray, Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawCircle(Figure.Circle circle, Color color)
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(color);
                g.DrawEllipse(pen, (float)circle.Center.X - (float)circle.Radius, (float)(circle.Center.Y - circle.Radius), (float)circle.Radius * 2, (float)circle.Radius * 2);
            }

        }

        public void DrawArc(Figure.Arc arc, Color color)
        {
             using (Graphics g = this.CreateGraphics())
             {
              Pen pen = new Pen(color);
              g.DrawArc(pen, (float)(arc.Center.X - arc.Radius), (float)(arc.Center.Y - arc.Radius), (float)arc.Radius * 2, (float)(arc.Radius * 2), (float)(arc.StartAngle), (float)(arc.EndAngle - arc.StartAngle));
             }
        }

        public void DrawText(string text, Figure.Point point, Color color)
        {
            throw new NotImplementedException();
        }

        public void Print(List<string> text)
        {
            foreach (var result in text)
            {
                SalidaDeEvaluacion.Texts += result + "\n";
            }
        }

        public void ReportError(string message)
        {
            SalidaDeEvaluacion.Texts += message + "\n";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

       
    
}
