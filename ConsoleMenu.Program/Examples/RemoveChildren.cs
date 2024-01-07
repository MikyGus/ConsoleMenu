using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Program.Examples;
internal class RemoveChildren
{
    public static void Run()
    {
        IMenuItem menuItem = new MenuItem("Menu 1");

        MenuItem menu = new MenuItem("Simple menu");
        menu.Position = new Vector2(0, 1);
        menu.Children.Add(1, menuItem);
        menu.Children.Add(2, new MenuItem("Menu 2"));
        menu.Children.Add(3, new MenuItem("Menu 3"));
        menu.Children.Remove(menuItem);
        menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
        menu.Render();
    }
}
