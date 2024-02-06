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
                m.Configure(x => x.IsMarked = !m.IsMarked);
                // TODO: content render
                //m.Content.Render();
            }
        };
        menu[0].AddChild("Sub 2");
        menu[0].Configure(o => { o.OrientationOfChildren = Orientation.Horizontal; });
        menu[0].OnKeyPressed += SetItemMarkOnParent;

        menu.AddChild("My SubMenu #2");
        menu[1].OnKeyPressed += (m, k) =>
        {
            if (k.Modifiers == ConsoleModifiers.Control && k.Key == ConsoleKey.H)
            {
                m.Configure(o => o.IsChildrenVisible = !m.IsChildrenVisible);
                m.ReRender();
            }
        };
        menu[1].OnKeyPressed += SetItemMark;
        menu[1].AddChild("Sub1");
        menu[1].AddChild("Sub2");

        menu.AddChild("My SubMenu #3");
        menu[2].OnKeyPressed += SetItemMark;

        menu.Configure(x =>
        {
            x.OrientationOfChildren = Orientation.Horizontal;
            x.IsSelected = true;
        });
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
        item.Configure(x => x.IsMarked = !item.IsMarked);
        // TODO: Content render
        //item.Content.Render();
    }

    static void SetItemMarkOnParent(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return;
        }

        if (item.Parent is not null)
        {
            item.Parent.Configure(x => x.IsMarked = !item.Parent.IsMarked);
            item.Parent.Render(); // Replace me with below
            // TODO: Content render
            //item.Parent.Content.Render();
        }
        // TODO: Content render
        //item.Content.Render();
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
                child.Configure(x => x.IsMarked = !child.IsMarked);
            }
            item.Render();
        }
    }
}