using System.Xml;

namespace GSharpInterpreter;
public static partial class Intersect
{
    public static object InterseccionSegmentos(Segment segmento1, Segment segmento2)
    {
        Line recta1 = new Line(segmento1.P1, segmento1.P2);
        Line recta2 = new Line(segmento1.P1, segmento1.P2);
        object posibleInterseccion = InterseccionRectas(recta1, recta2);
        if (posibleInterseccion is Undefined)
        {
            return posibleInterseccion;
        }
        else
        {
            List<Point> interseccion = (List<Point>)posibleInterseccion;
            if (interseccion.Count == 0 || (EstaEnSegmento(interseccion[0], segmento1) && EstaEnSegmento(interseccion[0], segmento2)))
            {
                return interseccion;
            }
            else
            {
                return new List<Point>();
            }
        }
    }
    public static object InterseccionSegmentoRayo(Segment segmento, Ray rayo)
    {
        Segment segmento2 = SegmentoAPartirDeRayo(rayo);
        return InterseccionSegmentos(segmento, segmento2);
    }
    static List<Point> InterseccionSegmentoArco(Segment segmento, Arc arco)
    {
        var interseccion = new List<Point>();
        var posibleInterseccion = (List<Point>)InterseccionRectaArco(new Line(segmento.P1, segmento.P2), arco);
        if (!posibleInterseccion.Any())
        {
            return interseccion;
        }

        foreach (var punto in posibleInterseccion)
        {
            if (EstaEnSegmento(punto, segmento))
            {
                interseccion.Add(punto);
            }

        }
        return interseccion;

    }

    static List<Point> InterseccionSegmentoCircunferencia(Segment segmento, Circle circunferencia)
    {
        var interseccion = new List<Point>();
        var posibleInterseccion = (List<Point>)InterseccionRectaCirculo(new Line(segmento.P1, segmento.P2), circunferencia);
        if (!posibleInterseccion.Any())
        {
            return interseccion;
        }

        foreach (var punto in posibleInterseccion)
        {
            if (EstaEnSegmento(punto, segmento))
            {
                interseccion.Add(punto);
            }

        }
        return interseccion;

    }
}