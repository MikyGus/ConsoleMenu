using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;

public static class ContentHelpers
{
    public static void WriteAtPosition(Vector2 position, string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        Console.SetCursorPosition(position.X, position.Y);
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.Write(text);
    }
}