using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Program.Examples;
internal class AddChildren
{
    public static void Run()
    {
        IMenuItem menu = new MenuItem("Simple menu");
        menu.AddChild("My SubMenu #1");
        menu[0].AddChild("Sub1");
        menu[0].AddChild("Sub2");
        string subMenu2Title = "My SubMenu #2";
        menu.AddChild(subMenu2Title);
        menu[subMenu2Title].AddChild("Sub1");
        menu[subMenu2Title].AddChild("Sub2");
        menu.AddChild("My SubMenu #3");
        menu.OrientationOfChildren = Orientation.Vertical;
        menu.Render();
    }
}