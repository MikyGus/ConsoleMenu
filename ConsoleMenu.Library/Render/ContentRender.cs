using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;
public abstract class ContentRender : IContentRender
{
    public bool IsSelected { get; set; }
    public bool IsMarked { get; set; }
    public string Content { get; set; }


    public abstract Vector2 AreaNeeded();

    public abstract void Render(Vector2 position);

    protected void Render(Vector2 position, Action<Vector2> action)
    {
        if (position is null)
        {
            throw new ArgumentNullException(nameof(position));
        }

        ConsoleColor tempBgColor = Console.BackgroundColor;
        ConsoleColor tempFgColor = Console.ForegroundColor;

        action(position);

        Console.BackgroundColor = tempBgColor;
        Console.ForegroundColor = tempFgColor;
    }

    public void WriteAtPosition(Vector2 position, string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        Console.SetCursorPosition(position.X, position.Y);
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.Write(text);
    }
}