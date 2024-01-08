using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Program.Examples;
internal class SetAction
{
    public static void Run()
    {
        IMenuItem subMenu = new MenuItem("My SubMenu #1");
        subMenu.Children.Add(1, new MenuItem("Sub1"));
        subMenu.Children.Add(2, new MenuItem("Sub2"));
        subMenu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
        subMenu.SetAction(SetItemMarkOnParent);

        IMenuItem subMenu2 = new MenuItem("My SubMenu #2");
        subMenu2.Children.Add(1, new MenuItem("Sub1"));
        subMenu2.Children.Add(2, new MenuItem("Sub2"));
        subMenu2.SetAction(SetItemMarkChildren);

        IMenuItem subMenu3 = new MenuItem("My SubMenu #3");
        subMenu3.SetAction(SetItemMark);

        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.Children.Add(1, subMenu);
        menu.Children.Add(2, subMenu2);
        menu.Children.Add(3, subMenu3);
        menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
        menu.Content.IsSelected = true;
        menu.Render();

        // Render() use these to render
        //menu.ContentRenderer.Render(menu.Position);
        //menu.Children.Render();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }

    static bool SetItemMark(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return false;
        }

        item.Content.IsMarked = !item.Content.IsMarked;
        item.Content.Render();
        return false;
    }

    static bool SetItemMarkOnParent(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return false;
        }

        if (item.Parent is not null)
        {
            item.Parent.Content.IsMarked = !item.Parent.Content.IsMarked;
            item.Parent.Content.Render();
        }
        item.Content.Render();
        return false;
    }

    static bool SetItemMarkChildren(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return false;
        }

        if (item.Children.HaveChildren())
        {
            foreach (IChildItem child in item.Children.GetChildren())
            {
                child.Item.Content.IsMarked = !child.Item.Content.IsMarked;
            }
            item.Render();
        }
        return false;
    }
}