namespace GSharpInterpreter;
public static partial class Intersect
{
    public static object InterseccionPuntoRecta(Line recta, Point punto)
    {
        var (m, n) = EcuacionRecta(recta);
        if (punto.Y == (m * punto.X + n))
        {
            return new List<Point>()
            {
                punto,
            };
        }
        return new List<Point>();
    }

    public static object InterseccionPuntoSegmento( Segment segmento, Point punto)
    {
        if(EstaEnSegmento(punto, segmento))
        {
            return new List<Point>()
            {
                punto,
            };
        }
        return new List<Point>();
    }

    public static object InterseccionPuntoRayo(Ray rayo, Point punto)
    {
        Segment segmento = SegmentoAPartirDeRayo(rayo);
        if(EstaEnSegmento(punto))
        {
            return new List<Point>()
            {
                punto,
            };
        }
        return new List<Point>();
    }

    public static object InterseccionPuntoArco(Arc arco, Point punto)
    {
        if(EstaEnArco(punto))
        {
            return new List<Point>()
            {
                punto,
            };
        }
        return new List<Point>();
    }

    public static object  InterseccionPuntoCircunferencia( Circle circunferencia, Point punto)
    { 
        Point centro = circunferencia.Center;
        double distanciaEntrePuntos = Math.Pow(centro.X-punto.X) + Math.Pow(centro.Y - punto.Y);
        if(distanciaEntrePuntos == Math.Pow(circunferencia.Radius.Value))
        {
            return new List<Point>()
            {
                punto,
            };
        }

        return new List<Point>();
    }

}