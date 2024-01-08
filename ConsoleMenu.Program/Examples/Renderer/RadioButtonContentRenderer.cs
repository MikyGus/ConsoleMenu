using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render;

namespace ConsoleMenu.Program.Examples.Renderer;
internal class RadioButtonContentRenderer : IContentRenderer
{
    public Vector2 AreaNeeded(IMenuItem menuItem) => new(menuItem.Content.Title.Length + 4, 1);

    public void Render(IMenuItem menuItem)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        char IsMarkedChar = menuItem.Content.IsMarked ? (char)4 : ' ';
        ConsoleColor fgColor = menuItem.Content.IsSelected ? ConsoleColor.Blue : ConsoleColor.Black;

        ContentHelpers.WriteAtPosition(menuItem.Position, $"({IsMarkedChar}) {menuItem.Content.Title}", fgColor, ConsoleColor.Gray);
    }
}
