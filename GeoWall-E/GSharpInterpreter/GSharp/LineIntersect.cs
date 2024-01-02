
namespace GSharpInterpreter;
public static partial class Intersect
{

    public static object InterseccionRectas(Line recta1, Line recta2)
    {
        var (m1, n1) = EcuacionRecta(recta1);
        var (m2, n2) = EcuacionRecta(recta2);
        if (m1 == m2 && n1 == n2) //si las pendientes y el termino independiente son iguales, las rectas son coincidentes
        {
            return new Undefined();
        }
        else
        {
            var interceptos = new List<Point>();
            if (m1 == m2) // si las pendientes son iguales, son rectas paralelas
            {
                return interceptos;
            }
            else // la formula para hallar el intersecto es la sustitucion de las ecuaciones de las rectas cuando queda un 2x2
            {
                double x1 = recta1.P1.X;
                double y1 = recta1.P1.Y;
                double x2 = recta1.P2.X;
                double y2 = recta1.P2.Y;

                double x3 = recta2.P1.X;
                double y3 = recta2.P1.Y;
                double x4 = recta2.P2.X;
                double y4 = recta2.P2.Y;
                double denominador = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

                double intersectX = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / denominador;
                double intersectY = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / denominador;

                interceptos.Add(new Point(intersectX, intersectY));
                return interceptos;
            }
        }
    }


    public static object InterseccionRectaSegmento(Line recta, Segment segmento)
    {
        Line nuevaRecta = new Line(segmento.P1, segmento.P2);
        object intersectos = InterseccionRectas(recta, nuevaRecta);
        if (intersectos is Undefined)
        {
            return intersectos;
        }
        List<Point> intersecto = (List<Point>)intersectos;
        if (intersecto.Count == 0)
        {
            return intersecto;
        }
        else
        {
            if (EstaEnSegmento(intersecto[0], segmento))
            {
                return intersecto;
            }
            else
            {
                return new List<Point>();
            }
        }
    }




    public static object InterseccionRectaCirculo(Line recta, Circle circunferencia)
    {
        var (m, c) = EcuacionRecta(recta);
        var centro = circunferencia.Center;
        var radio = circunferencia.Radius.Value;

        double centroX = centro.X;
        double centroY = centro.Y;

        //propiedades a,b y c de la ecuacion del discriminante: b^2 -4*a*c.
        double A = 1 + Math.Pow(m, 2);
        double B = 2 * (m * c - m * centroY - centroX);
        double C = Math.Pow(centroY, 2) - Math.Pow(radio, 2) + Math.Pow(centroX, 2) - 2 * c * centroY + Math.Pow(c, 2);

        double discriminante = Math.Pow(B, 2) - 4 * A * C;

        var puntosInterseccion = new List<Point>();

        if (discriminante >= 0)
        {
            double x1 = (-B + Math.Sqrt(discriminante)) / (2 * A);
            double x2 = (-B - Math.Sqrt(discriminante)) / (2 * A);

            double y1 = m * x1 + c;
            double y2 = m * x2 + c;

            puntosInterseccion.Add(new Point(x1, y1));
            puntosInterseccion.Add(new Point(x2, y2));

        }
        return puntosInterseccion;
    }

    static object InterseccionRectaArco(Line recta, Arc arco)
    {
        var (m, c) = EcuacionRecta(recta);
        var centro = arco.Center;
        var radio = arco.Radius.Value;

        double centroX = centro.X; //h
        double centroY = centro.Y; //k

        //propiedades a,b y c de la ecuacion del discriminante: b^2 -4*a*c.
        double A = 1 + Math.Pow(m, 2);
        double B = 2 * (m * c - m * centroY - centroX);
        double C = Math.Pow(centroY, 2) - Math.Pow(radio, 2) + Math.Pow(centroX, 2) - 2 * c * centroY + Math.Pow(c, 2);

        double discriminante = Math.Pow(B, 2) - 4 * A * C;

        var puntosInterseccion = new List<Point>();

        if (discriminante >= 0)
        {
            double x1 = (-B + Math.Sqrt(discriminante)) / (2 * A);
            double x2 = (-B - Math.Sqrt(discriminante)) / (2 * A);

            double y1 = m * x1 + c;
            double y2 = m * x2 + c;

            if (EstaEnArco(new Point(x1, y1), arco))
            {
                puntosInterseccion.Add(new Point(x1, y1));
            }
            if (EstaEnArco(new Point(x2, y2), arco))
            {
                puntosInterseccion.Add(new Point(x2, y2));
            }
        }

        return puntosInterseccion;
    }




}
