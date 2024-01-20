using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Program.Examples;
internal class SetAction
{
    public static void Run()
    {
        IMenuItem subMenu = new MenuItem("My SubMenu #1");
        IMenuItem subsubMenu1 = new MenuItem("Sub1");
        subsubMenu1.OnKeyPressed += (m, k) =>
        {
            if (k.Key == ConsoleKey.Enter)
            {
                m.Content.IsMarked = !m.Content.IsMarked;
                m.Content.Render();
            }
        };
        subMenu.Children.Add(1, subsubMenu1);
        subMenu.Children.Add(2, new MenuItem("Sub2"));
        subMenu.Children.Orientation = Library.Managers.ContentOrientation.Horizontal;
        subMenu.OnKeyPressed += SetItemMarkOnParent;

        IMenuItem subMenu2 = new MenuItem("My SubMenu #2");
        subMenu2.OnKeyPressed += (m, k) =>
        {
            if (k.Modifiers == ConsoleModifiers.Control && k.Key == ConsoleKey.H)
            {
                m.Children.IsVisible = !m.Children.IsVisible;
                m.ReRender();
            }
        };
        subMenu2.OnKeyPressed += SetItemMark;
        subMenu2.Children.Add(1, new MenuItem("Sub1"));
        subMenu2.Children.Add(2, new MenuItem("Sub2"));

        IMenuItem subMenu3 = new MenuItem("My SubMenu #3");
        subMenu3.OnKeyPressed += SetItemMark;

        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.Children.Add(1, subMenu);
        menu.Children.Add(2, subMenu2);
        menu.Children.Add(3, subMenu3);
        menu.Children.Orientation = Library.Managers.ContentOrientation.Horizontal;
        menu.Content.IsSelected = true;
        menu.Render();

        // Render() use these to render
        //menu.Content.Render();
        //menu.Children.Render();
        //menu.ReRender();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            _ = menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }

    static void SetItemMark(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return;
        }

        item.Content.IsMarked = !item.Content.IsMarked;
        item.Content.Render();
    }

    static void SetItemMarkOnParent(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return;
        }

        if (item.Parent is not null)
        {
            item.Parent.Content.IsMarked = !item.Parent.Content.IsMarked;
            item.Parent.Content.Render();
        }
        item.Content.Render();
    }

    static void SetItemMarkChildren(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return;
        }

        if (item.Children.HaveChildren())
        {
            foreach (IChildItem child in item.Children.GetChildren())
            {
                child.Item.Content.IsMarked = !child.Item.Content.IsMarked;
            }
            item.Render();
        }
    }
}