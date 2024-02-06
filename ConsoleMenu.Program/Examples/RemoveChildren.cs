namespace ConsoleMenu.Program.Examples;
internal class RemoveChildren
{
    public static void Run()
    {
        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.AddChild("Menu 1");
        menu.AddChild("Menu 2");
        menu.AddChild("Menu 3");
        IMenuItem menuItem = menu[0];
        menu.RemoveChild(menuItem);
        menu.RemoveChild(1);
        menu.Configure(o => { o.OrientationOfChildren = Orientation.Horizontal; });
        menu.Render();
    }
}