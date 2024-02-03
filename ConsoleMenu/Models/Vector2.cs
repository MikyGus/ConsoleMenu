namespace ConsoleMenu;
/// <summary>
/// Represents a point in a 2D space or 2D area with integers for X and Y.
/// </summary>
public record struct Vector2(int X, int Y)
{
    public int X { get; set; } = X;
    public int Y { get; set; } = Y;

    public static Vector2 operator +(Vector2 v1, Vector2 v2)
        => new(v1.X + v2.X, v1.Y + v2.Y);

    public static Vector2 operator -(Vector2 v1, Vector2 v2)
        => new(v1.X - v2.X, v1.Y - v2.Y);

    public static Vector2 operator *(Vector2 v1, int i)
        => new(v1.X * i, v1.Y * i);

    public override readonly string ToString() => $"({X}, {Y})";

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