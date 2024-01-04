using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;
public class DefaultContentRender : ContentRender
{
    private static ConsoleColor _normalFgColor = ConsoleColor.Black;
    private static ConsoleColor _markedColor = ConsoleColor.Green;
    private static ConsoleColor _selectedColor = ConsoleColor.Blue;
    private ConsoleColor _foregroundColor = _normalFgColor;
    private ConsoleColor _backgroundColor = ConsoleColor.Gray;

    public override Vector2 AreaNeeded() => new(Content.Length + 2, 1);
    public override void Render(Vector2 position) => Render(position, x =>
        {
            if (IsMarked)
            {
                _backgroundColor = _markedColor;
                _foregroundColor = _normalFgColor;
            }
            else
            {
                _backgroundColor = ConsoleColor.Gray;
                _foregroundColor = _normalFgColor;
            }

            WriteAtPosition(position, "[", IsSelected ? _selectedColor : _backgroundColor, _backgroundColor);

            WriteAtPosition(new Vector2(position.X + 1, position.Y), Content, _foregroundColor, _backgroundColor);

            WriteAtPosition(new Vector2(position.X + 1 + Content.Length, position.Y),
                "]", IsSelected ? _selectedColor : _backgroundColor, _backgroundColor);
        });
}
