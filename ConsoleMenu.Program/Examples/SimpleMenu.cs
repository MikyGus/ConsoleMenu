namespace ConsoleMenu.Program.Examples;
internal class SimpleMenu
{
    public static void Render_SimpleMenu_Horizontal()
    {
        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.AddChild("Menu 1");
        menu.AddChild("Menu 2");
        menu.AddChild("Menu 3");
        menu.Configure(o =>
        {
            o.OrientationOfChildren = Orientation.Horizontal;
        });
        menu.Render();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            _ = menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }

    public static void Render_SimpleMenu_Vertical()
    {
        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.AddChild("Menu 1");
        menu.AddChild("Menu 2");
        menu.AddChild("Menu 3");
        menu.Configure(o =>
        {
            o.OrientationOfChildren = Orientation.Vertical;
        });
        menu.Render();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }
}