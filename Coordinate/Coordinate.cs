using UnityEngine;

//Coodinate object
[System.Serializable]
public struct Coordinate
{
    public int x;
    public int y;

    public static readonly Coordinate north = new Coordinate(0, 1);
    public static readonly Coordinate northeast = new Coordinate( 1, 1);                    
    public static readonly Coordinate east = new Coordinate(1, 0);
    public static readonly Coordinate southeast = new Coordinate( 1,-1);                                   
    public static readonly Coordinate south = new Coordinate(0, -1);
    public static readonly Coordinate southwest = new Coordinate(-1,-1);                                  
    public static readonly Coordinate west = new Coordinate(-1, 0);
    public static readonly Coordinate northwest = new Coordinate(-1, 1);

    public static readonly Coordinate up = new Coordinate(0, 1);
    public static readonly Coordinate right = new Coordinate(1, 0);
    public static readonly Coordinate down = new Coordinate(0, -1);
    public static readonly Coordinate left = new Coordinate(-1, 0);

    public static readonly Coordinate zero = new Coordinate(0, 0);
    public static readonly Coordinate one = new Coordinate(1, 1);

   
    public static Coordinate operator *(Coordinate c1, int c2)
    {
        return new Coordinate(c1.x * c2, c1.y * c2);
    }
    public static Coordinate operator /(Coordinate c1, int c2)
    {
        return new Coordinate(c1.x / c2, c1.y / c2);
    }
    public static Coordinate operator +(Coordinate c1, Coordinate c2)
    {
        return new Coordinate(c1.x + c2.x, c1.y + c2.y);
    }
    public static Coordinate operator -(Coordinate c1, Coordinate c2)
    {
        return new Coordinate(c1.x - c2.x, c1.y - c2.y);
    }
    public static Coordinate operator +(Coordinate c1, Direction c2)
    {
        return c1 + c2.toCoord();
    }
    public static Coordinate operator -(Coordinate c1, Direction c2)
    {
        return c1 - c2.toCoord();
    }

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(object obj)
    {
        bool result = false;

        if (obj.GetType() == typeof(Coordinate))
        {
            Coordinate newObj = (Coordinate)obj;
            result = (x == newObj.x && y == newObj.y);
        }
        return result;
    }

    public static bool operator ==(Coordinate c1, Coordinate c2)
    {
        bool result = false;
        if (c1.x == c2.x && c1.y == c2.y)
            result = true;
        return result;
    }

    public static bool operator !=(Coordinate c1, Coordinate c2)
    {
        bool result = true;
        if (c1.x == c2.x && c1.y == c2.y)
            result = false;
        return result;
    }

    public int Distance(Coordinate target){
        return Mathf.Abs(x - target.x) + Mathf.Abs(y - target.y);
    }

    public static implicit operator Direction(Coordinate c)
    {
        Direction result;

        if (c == north)
            result = Direction.north;

        else if (c == northeast)
            result = Direction.northeast;

        else if (c == east)
            result = Direction.east;

        else if (c == southeast)
            result = Direction.southeast;

        else if (c == south)
            result = Direction.south;

        else if (c == southwest)
            result = Direction.southwest;

        else if (c == west)
            result = Direction.west;

        else if (c == northwest)
            result = Direction.northwest;

        else
            result = Direction.nothing;

        return result;
    } 


    public Vector3 ToVector3(float value = 0.0f){
        return new Vector3(x, value, y);
    }

    public static implicit operator Vector3(Coordinate c)
    {
        return new Vector3(c.x, 0.0f, c.y);
    }

    public override string ToString(){
        return "[" + x + "/" + y + "]";
    }
  
    public static implicit operator Coordinate(Vector3 vector)
    {
        return new Coordinate(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.z));
    }

    public static implicit operator Coordinate(Vector2 vector)
    {
        return new Coordinate(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }
    
    public static Coordinate FromVector3(Vector3 vector)
    {
        return new Coordinate(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.z));
    }

    public bool IsInside(Coordinate areaMax,Coordinate areaMin = default(Coordinate))
    {
        return ( x <  areaMax.x && y <  areaMax.y &&
                 x >= areaMin.x && y >= areaMin.y);
    }
}

public static class MatrixExtention
{
    public static Coordinate Size<T>(this T[,] matrix) {
        return new Coordinate(matrix.GetLength(0), matrix.GetLength(1));
    } 

    public static bool Inside<T>(this T[,] matrix, Coordinate c) {
        return ( c.x < matrix.GetLength(0) && 
                 c.y < matrix.GetLength(1) &&
                 c.x >= 0 && 
                 c.y >= 0);
    }

    public static T Get<T>(this T[,] matrix, Coordinate coordinate){
        return matrix[coordinate.x, coordinate.y];
    }

    public static void Set<T>(this T[,] matrix, Coordinate coordinate, T o){
        matrix[coordinate.x, coordinate.y] = o;
    }

    public static int Width<T>(this T[,] matrix) {
        return matrix.GetLength(0);
    }

    public static int Height<T>(this T[,] matrix) {
        return matrix.GetLength(1);
    }

    public static void Foreach2D<T>(this T[,] matrix, Foreach2DDelegate<T> iterateMethod) {
        for (int y = 0; y < matrix.Height(); y++)
            for (int x = 0; x < matrix.Width(); x++)
                iterateMethod(x, y, ref matrix[x, y]);
    }

    public static void Foreach2D<T>(this T[,] matrix, Foreach2DCoordinateDelegate<T> iterateMethod) {
        for (int y = 0; y < matrix.Height(); y++)
            for (int x = 0; x < matrix.Width(); x++)
                iterateMethod(new Coordinate(x,y), ref matrix[x, y]);
    }

    public static bool IsInside<T>(this T[,] matrix, Coordinate c) {
        return (c.x < matrix.Width() && c.y < matrix.Height());
    }

    public static bool IsInside<T>(this T[,] matrix, int x, int y) {
        return (x < matrix.Width() && y < matrix.Height());
    }
}

public delegate void Foreach2DDelegate<T>(int x, int y, ref T o);

public delegate void Foreach2DCoordinateDelegate<T>(Coordinate c, ref T o);
