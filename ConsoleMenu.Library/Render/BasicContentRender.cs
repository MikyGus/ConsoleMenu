using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render.Contents;
public class BasicContentRender : IContentRender
{
    private readonly string _title;
    private static ConsoleColor _normalFgColor = ConsoleColor.Black;
    private static ConsoleColor _normalBgColor = ConsoleColor.Gray;
    private static ConsoleColor _markedColor = ConsoleColor.Green;
    private static ConsoleColor _selectedColor = ConsoleColor.Blue;
    private ConsoleColor _foregroundColor = _normalFgColor;
    private ConsoleColor _backgroundColor = _normalBgColor;

    public bool IsSelected { get; set; }
    public bool IsMarked { get; set; }

    public BasicContentRender(string title)
    {
        _title = title;
    }
    public Vector2 AreaNeeded() => new(_title.Length + 2, 1);
    public void Render(Vector2 position)
    {
        if (position is null)
            throw new ArgumentNullException(nameof(position));

        var tempBgColor = Console.BackgroundColor;
        var tempFgColor = Console.ForegroundColor;

        if (IsMarked)
        {
            _backgroundColor = _markedColor;
            _foregroundColor = _normalFgColor;
        }

        WriteAtPosition(position, "[", IsSelected ? _selectedColor : _backgroundColor, _backgroundColor);

        WriteAtPosition(new Vector2(position.X + 1, position.Y), _title, _foregroundColor, _backgroundColor);

        WriteAtPosition(new Vector2(position.X + 1 + _title.Length, position.Y),
            "]", IsSelected ? _selectedColor : _backgroundColor, _backgroundColor);

        Console.BackgroundColor = tempBgColor;
        Console.ForegroundColor = tempFgColor;
    }

    private void WriteAtPosition(Vector2 position, string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        Console.SetCursorPosition(position.X,position.Y);
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.Write(text);
    }
}
