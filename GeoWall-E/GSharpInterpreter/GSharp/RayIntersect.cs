namespace GSharpInterpreter;
public static partial class Intersect
{
    public static object InterseccionRectaRayo(Line recta, Ray rayo) //intersectar rectas con rectas, rayos o segmentos es practicamente lo mismo, solo que el dominio se restringe
    {
        // un rayo es un segmento que va desde el punto donde inicia hasta el punto limite del lienzo.
        // solo hay que encontrar cual es el punto limite que pertenece.
        Segment segmento = SegmentoAPartirDeRayo(rayo);
        return InterseccionRectaSegmento(recta, segmento);
    }

    public static object InterseccionRayos(Ray rayo1, Ray rayo2)
    {
        Segment segmento1 = SegmentoAPartirDeRayo(rayo1);
        Segment segmento2 = SegmentoAPartirDeRayo(rayo2);
        return InterseccionSegmentos(segmento1, segmento2);
    }

    public static object IntercepcionRayoCircunferencia(Ray rayo, Circle circunferencia) //ver el caso en el que sea el mismo punto, se supone que se manejo antes de llegar a este punto.
    {
        Segment segmentoDeRayo = SegmentoAPartirDeRayo(rayo);
        return InterseccionSegmentoCircunferencia(segmentoDeRayo, circunferencia);
    }

    public static object IntercepcionRayoArco(Ray rayo, Arc arco)
    {
        Segment segmentoDeRayo = SegmentoAPartirDeRayo(rayo);
        return InterseccionSegmentoArco(segmentoDeRayo, arco);
    }

    #region Metodos Auxiliares
    static double DistanciaPuntoRecta(Point punto, Line recta)
    {
        double x1 = recta.P1.X;
        double y1 = recta.P1.Y;
        double x2 = recta.P2.X;
        double y2 = recta.P2.Y;
        double x0 = punto.X;
        double y0 = punto.Y;

        // Cálculo de la distancia entre el punto y la recta usando la fórmula
        double distancia = Math.Abs((y2 - y1) * x0 - (x2 - x1) * y0 + x2 * y1 - y2 * x1) / Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));

        return distancia;
    }


    static (double, double) EcuacionRecta(Line line)
    {
        double m = (line.P2.Y - line.P1.Y) / (line.P2.X - line.P1.X);
        double c = line.P1.Y - m * line.P1.X;
        return (m, c);
    }

    static bool EstaEnArco(Point punto, Arc arco)
    {
        double anguloPunto = Math.Atan2(punto.Y - arco.Center.Y, punto.Y - arco.Center.X);
        double anguloInicio = Math.Atan2(arco.InitialRayPoint.Y - arco.Center.Y, arco.InitialRayPoint.X - arco.Center.X);
        double anguloFin = Math.Atan2(arco.FinalRayPoint.Y - arco.Center.Y, arco.FinalRayPoint.X - arco.Center.X);
        // if (anguloFin <= anguloInicio)
        // {
        //     anguloFin += 360; // Ajusta el ángulo final sumando 360 grados si es menor o igual al ángulo inicial
        // }
        //esa parte de arriba esta en veremos.

        if (anguloInicio < anguloFin)
        {
            return anguloInicio <= anguloPunto && anguloPunto <= anguloFin;
        }
        else
        {
            return anguloInicio <= anguloPunto || anguloPunto <= anguloFin;
        }
    }

    static bool EstaEnSegmento(Point punto, Segment segmento)
    {
        double minX = Math.Min(segmento.P1.X, segmento.P2.X);
        double maxX = Math.Max(segmento.P1.X, segmento.P2.X);
        double minY = Math.Min(segmento.P1.Y, segmento.P2.Y);
        double maxY = Math.Max(segmento.P1.Y, segmento.P2.Y);

        // Verificar si el punto está dentro del rango de los puntos extremos del segmento
        if (punto.X >= minX && punto.X <= maxX && punto.Y >= minY && punto.Y <= maxY)
        {
            // Calcular la ecuación paramétrica del segmento
            double t = ((punto.X - segmento.P1.X) * (segmento.P2.X - segmento.P1.X) +
                        (punto.Y - segmento.P1.Y) * (segmento.P2.Y - segmento.P1.Y)) /
                        (Math.Pow(segmento.P2.X - segmento.P1.X, 2) + Math.Pow(segmento.P2.Y - segmento.P1.Y, 2));

            // Verificar si el valor de t está entre 0 y 1 para estar dentro del segmento
            if (t >= 0 && t <= 1)
            {
                return true;
            }
        }

        return false;
    }

    static Segment SegmentoAPartirDeRayo(Ray rayo)
    {
        Point puntoLimite;
        Line rectaDeRayo = new Line(rayo.P1, rayo.P2);
        if (rayo.P1.X == rayo.P2.X)
        {
            if (rayo.P1.Y > rayo.P2.Y)
            {
                puntoLimite = new Point(rayo.P1.X, 0);
            }
            else
            {
                puntoLimite = new Point(rayo.P1.X, Interpreter.UI.CanvasHeight);
            }
        }
        else
        {
            var (m, n) = EcuacionRecta(rectaDeRayo);

            // Si el punto final está a la derecha del punto inicial
            if (rayo.P2.X >= rayo.P1.X)
            {
                float x = Interpreter.UI.CanvasWidth; // Coordenada X en el borde del PictureBox
                float y = (float)(rayo.P1.Y + m * (x - rayo.P1.X)); // Calcular Y correspondiente al borde del PictureBox
                puntoLimite = new Point(x, y);
            }
            // Si el punto final está a la izquierda del punto inicial
            else
            {
                float x = 0; // Coordenada X en el borde del PictureBox
                float y = (float)(rayo.P1.Y + m * (x - rayo.P1.X)); // Calcular Y correspondiente al borde del PictureBox
                puntoLimite = new Point(x, y);
            }
        }
        return new Segment(rayo.P1, puntoLimite);
    }

    static Circle CirculoAPartirDeArco (Arc arco)
    {
        return new Circle(arco.Center, arco.Radius);
    }

    #endregion

}