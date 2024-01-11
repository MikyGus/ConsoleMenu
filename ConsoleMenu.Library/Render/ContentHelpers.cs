using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;

public static class ContentHelpers
{
    public static void WriteAtPosition(Vector2 position, string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        if (IsOutsideBufferWidth(position))
        {
            return;
        }
        else if (TextIsLongerThanBufferWidth(position, text))
        {
            text = ShortenTextToFitConsoleBuffer(position, text);
        }

        Console.SetCursorPosition(position.X, position.Y);
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.Write(text);
    }

    private static bool IsOutsideBufferWidth(Vector2 position) => position.X >= Console.BufferWidth;
    private static bool TextIsLongerThanBufferWidth(Vector2 position, string text) => position.X + text.Length > Console.BufferWidth;
    private static string ShortenTextToFitConsoleBuffer(Vector2 position, string text)
    {
        int stringLength = Console.BufferWidth - position.X < 0 ? 0 : Console.BufferWidth - position.X;
        return text[..stringLength];
    }
}