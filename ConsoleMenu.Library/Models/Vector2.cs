namespace ConsoleMenu.Library.Models;
/// <summary>
/// Represents a point in a 2D space or 2D area with integers for X and Y.
/// </summary>
public class Vector2 : IEquatable<Vector2>
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 operator +(Vector2 v1, Vector2 v2)
        => new(v1.X + v2.X, v1.Y + v2.Y);

    public static Vector2 operator -(Vector2 v1, Vector2 v2)
        => new(v1.X - v2.X, v1.Y - v2.Y);

    public static Vector2 operator *(Vector2 v1, int i)
        => new(v1.X * i, v1.Y * i);

    public bool Equals(Vector2 other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return other is Vector2 vector2 && Equals(vector2);
    }

    public override string ToString() => $"({X}, {Y})";

    public static bool operator ==(Vector2 left, Vector2 right)
        => left.Equals(right);

    public static bool operator !=(Vector2 left, Vector2 right)
        => left.Equals(right) == false;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public Vector2 Duplicate() => new(X, Y);

    public static Vector2 ZERO => new(0, 0);
    public static Vector2 UP => new(0, -1);
    public static Vector2 DOWN => new(0, 1);
    public static Vector2 LEFT => new(-1, 0);
    public static Vector2 RIGHT => new(1, 0);
    public static Vector2 LEFT_UP => new(-1, -1);
    public static Vector2 LEFT_DOWN => new(-1, 1);
    public static Vector2 RIGHT_UP => new(1, -1);
    public static Vector2 RIGHT_DOWN => new(1, 1);
}