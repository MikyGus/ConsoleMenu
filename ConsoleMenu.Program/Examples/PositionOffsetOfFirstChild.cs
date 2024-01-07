using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Program.Examples;
internal class PositionOffsetOfFirstChild
{
    public static void Run()
    {
        MenuItem menu = new MenuItem("Simple menu");
        menu.Position = new Vector2(0, 1);
        menu.Children.Add(1, new MenuItem("Menu 1"));
        menu.Children.Add(2, new MenuItem("Menu 2"));
        menu.Children.Add(3, new MenuItem("Menu 3"));
        menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
        menu.Children.PositionOffsetOfFirstChild = new Vector2(10, 0);
        menu.Render();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }
}
