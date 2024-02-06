using ConsoleMenu.Render;

namespace ConsoleMenu.Program.Examples.Renderer;
internal class RadioButtonContentRenderer : IContentRenderer
{
    public Vector2 AreaNeeded(IMenuItem menuItem) => new(menuItem.Title.Length + 4, 1);

    public void Render(IMenuItem menuItem)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        char IsMarkedChar = menuItem.IsMarked ? (char)4 : ' ';
        ConsoleColor fgColor = menuItem.IsSelected ? ConsoleColor.Blue : ConsoleColor.Black;

        ContentHelpers.WriteAtPosition(menuItem.Position, $"({IsMarkedChar}) {menuItem.Title}", fgColor, ConsoleColor.Gray);
    }
}