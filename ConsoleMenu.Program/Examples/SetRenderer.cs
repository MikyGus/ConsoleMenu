using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render;

namespace ConsoleMenu.Program.Examples;
internal class SetRenderer
{
    public static void Run()
    {
        IMenuItem menuItem1 = new MenuItem("Menu 1");
        menuItem1.SetRenderer<CheckboxContentRender>();
        IMenuItem menuItem2 = new MenuItem("Menu 2");
        menuItem2.SetRenderer<CheckboxContentRender>();
        menuItem2.Content.IsMarked = true;
        IMenuItem menuItem3 = new MenuItem("Menu 3");
        menuItem3.SetRenderer<CheckboxContentRender>();

        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.Children.Add(1, menuItem1);
        menu.Children.Add(2, menuItem2);
        menu.Children.Add(3, menuItem3);
        menu.OrientationOfChildren = Orientation.Horizontal;
        menu.Render();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }
}