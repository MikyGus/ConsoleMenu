using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;
public class CheckboxContentRender : IContentRenderer
{
    public Vector2 AreaNeeded(IMenuItem menuItem) => new(menuItem.Content.Title.Length + 4, 1);

    public void Render(IMenuItem menuItem)
    {
        string IsMarkedChar = menuItem.Content.IsMarked ? "X" : " ";
        ConsoleColor fgColor = menuItem.Content.IsSelected ? ConsoleColor.Blue : ConsoleColor.Black;
        ContentHelpers.WriteAtPosition(menuItem.Position, $"[{IsMarkedChar}] {menuItem.Content.Title}", fgColor, ConsoleColor.Gray);
    }
}