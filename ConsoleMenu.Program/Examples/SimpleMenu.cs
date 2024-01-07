using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Program.Examples;
internal class SimpleMenu
{
    public static void Render_SimpleMenu_Horizontal()
    {
        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.Children.Add(1, new MenuItem("Menu 1"));
        menu.Children.Add(2, new MenuItem("Menu 2"));
        menu.Children.Add(3, new MenuItem("Menu 3"));
        menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
        menu.Render();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }

    public static void Render_SimpleMenu_Vertical()
    {
        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.Children.Add(1, new MenuItem("Menu 1"));
        menu.Children.Add(2, new MenuItem("Menu 2"));
        menu.Children.Add(3, new MenuItem("Menu 3"));
        menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Vetical;
        menu.Render();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }
}