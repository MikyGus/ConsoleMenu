namespace ConsoleMenu.Program.Examples;
internal class HideUnhide
{
    public static void Run()
    {
        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.AddChild("Settings");
        menu["Settings"].AddChild("First");
        menu["Settings"].AddChild("My SubMenu #1");
        menu["Settings"][0].AddChild("Sub1");
        menu["Settings"][0][0].OnKeyPressed += ((m, k) =>
        {
            if (k.Key == ConsoleKey.Enter)
            {
                m.Configure(x => x.IsMarked = !m.IsMarked);
                // TODO: content render!!
                //                m.Content.Render();
            }
        });
        menu["Settings"][0].AddChild("Sub2");
        menu["Settings"][0].Configure(o =>
        {
            o.OrientationOfChildren = Orientation.Horizontal;
        });
        menu["Settings"][0].OnKeyPressed += SetItemMarkOnParent;


        menu["Settings"].AddChild("My SubMenu #2");
        menu["Settings"]["My SubMenu #2"].OnKeyPressed += ((m, k) =>
        {
            if (k.Modifiers == ConsoleModifiers.Control && k.Key == ConsoleKey.H)
            {
                m.Configure(o => o.IsChildrenVisible = !m.IsChildrenVisible);
                m.ReRender();
            }
        });

        menu["Settings"]["My SubMenu #2"].AddChild("My SubMenu #2a");
        menu["Settings"]["My SubMenu #2"][0].AddChild("My SubMenu #2aa");
        menu["Settings"]["My SubMenu #2"][0][0].AddChild("My SubMenu #2aaa");
        menu["Settings"]["My SubMenu #2"].AddChild("Sub2");
        menu["Settings"].AddChild("My SubMenu #3");
        menu["Settings"]["My SubMenu #3"].OnKeyPressed += SetItemMark;
        menu["Settings"].Configure(o => { o.OrientationOfChildren = Orientation.Horizontal; });

        menu.AddChild("Hello, World");
        //menu.Children.Add(2, new MenuItem("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent sit amet justo ac mauris hendrerit dapibus. Donec urna dolor, dapibus a libero sed, tempus luctus libero. Aliquam fringilla mi vitae pulvinar efficitur."));
        menu.Configure(x => x.IsSelected = true);

        menu.Render();


        _ = Console.ReadKey();
        //subMenu2.IsVisible = true;
        //subMenu2.ReRender();

        //subMenu2.EraseContent();

        //menu.IsVisible = false;
        //menu.Render();
        //menu.IsVisible = true;
        //menu.Render();


        // Render() use these to render
        //menu.ContentRenderer.Render(menu.Position);
        //menu.Children.Render();

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