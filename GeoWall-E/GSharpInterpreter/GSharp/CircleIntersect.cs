namespace GSharpInterpreter;
public static partial class Intersect
{
    static object InterseccionCircunferencias(Circle circulo1, Circle circulo2)
    {
        double x1 = circulo1.Center.X;
        double y1 = circulo1.Center.Y;
        double r1 = circulo1.Radius.Value;

        double x2 = circulo2.Center.X;
        double y2 = circulo2.Center.Y;
        double r2 = circulo2.Radius.Value;

        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

        if (distance > r1 + r2 || distance < Math.Abs(r1 - r2))
        {
            return new List<Point>(); // Circunferencias no se intersectan 
        }
        else if (distance == 0 && r1 == r2)
        {
            return new Undefined(); // Circunferencias coincidentes (undefined)
        }
        else if (distance == r1 + r2 || distance == Math.Abs(r1 - r2))
        {
            // Circunferencias tangentes en un punto
            double x = (r1 * x2 + r2 * x1) / (r1 + r2);
            double y = (r1 * y2 + r2 * y1) / (r1 + r2);

            Point tangentPoint = new Point(x, y);

            List<Point> intersectionPoints = new List<Point>()
            {
                tangentPoint,
            };
            return intersectionPoints;
        }
        else
        {
            // Circunferencias se cortan en dos puntos
            double a = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(distance, 2)) / (2 * distance);
            double h = Math.Sqrt(Math.Pow(r1, 2) - Math.Pow(a, 2));

            double x = x1 + (a * (x2 - x1)) / distance;
            double y = y1 + (a * (y2 - y1)) / distance;

            double intersectX1 = x + (h * (y2 - y1)) / distance;
            double intersectY1 = y - (h * (x2 - x1)) / distance;

            double intersectX2 = x - (h * (y2 - y1)) / distance;
            double intersectY2 = y + (h * (x2 - x1)) / distance;

            Point intersectionPoint1 = new Point(intersectX1, intersectY1);
            Point intersectionPoint2 = new Point(intersectX2, intersectY2);

            List<Point> intersectionPoints = new List<Point>()
            {
                intersectionPoint1,
                intersectionPoint2,
            };
            return intersectionPoints;
        }
    }



    public static object InterseccionCircunferenciaArco(Circle circunferencia, Arc arco)
    {
        Circle circulo = CirculoAPartirDeArco(arco);
        object posibleInterseccion = InterseccionCircunferencias(circunferencia, circulo);
        if (posibleInterseccion is Undefined)
        {
            return posibleInterseccion;
        }
        else
        {
            List<Point> interseccion = (List<Point>)posibleInterseccion;
            if (interseccion.Count == 0)
            {
                return interseccion;
            }
            else
            {
                List<Point> result = new List<Point>();
                foreach (var punto in interseccion)
                {
                    if (EstaEnArco(punto, arco))
                    {
                        result.Add(punto);
                    }
                }
                return result;
            }
        }
    }

    public static object InterseccionArcos(Arc arco1, Arc arco2)
    {
        Circle circulo1 = CirculoAPartirDeArco(arco1);
        object posibleInterseccion = InterseccionCircunferenciaArco(circulo1, arco2);
        if (posibleInterseccion is Undefined)
        {
            return posibleInterseccion;
        }
        else
        {
            List<Point> interseccion = (List<Point>)posibleInterseccion;
            if (interseccion.Count == 0)
            {
                return interseccion;
            }
            else
            {
                List<Point> result = new List<Point>();
                foreach (var punto in interseccion)
                {
                    if (EstaEnArco(punto, arco1))
                    {
                        result.Add(punto);
                    }
                }
                return result;
            }
        }
    }
}