using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Program.Examples;
internal class SetAction
{
    public static void Run()
    {
        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.AddChild("My SubMenu #1");
        menu[0].AddChild("Sub 1");
        menu[0]["Sub 1"].OnKeyPressed += (m, k) =>
        {
            if (k.Key == ConsoleKey.Enter)
            {
                m.Content.IsMarked = !m.Content.IsMarked;
                m.Content.Render();
            }
        };
        menu[0].AddChild("Sub 2");
        menu[0].OrientationOfChildren = Orientation.Horizontal;
        menu[0].OnKeyPressed += SetItemMarkOnParent;

        menu.AddChild("My SubMenu #2");
        menu[1].OnKeyPressed += (m, k) =>
        {
            if (k.Modifiers == ConsoleModifiers.Control && k.Key == ConsoleKey.H)
            {
                m.IsChildrenVisible = !m.IsChildrenVisible;
                m.ReRender();
            }
        };
        menu[1].OnKeyPressed += SetItemMark;
        menu[1].AddChild("Sub1");
        menu[1].AddChild("Sub2");

        menu.AddChild("My SubMenu #3");
        menu[2].OnKeyPressed += SetItemMark;

        menu.OrientationOfChildren = Orientation.Horizontal;
        menu.Content.IsSelected = true;
        menu.Render();

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

        if (item.HaveChildren())
        {
            foreach (IMenuItem child in item.GetChildren())
            {
                child.Content.IsMarked = !child.Content.IsMarked;
            }
            item.Render();
        }
    }
}