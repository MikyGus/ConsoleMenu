using ConsoleMenu.FormInput;
using ConsoleMenu.Render;

namespace ConsoleMenu.Program.Playground;
internal class All
{
    public static void Run()
    {
        var menu = new MenuItem("Settings")
        {
            Position = new Vector2(1, 1)
        };
        menu.AddChild("My SubMenu");
        menu[0].AddChild("Sub1");
        menu[0].AddChild("Sub2");
        menu[0].AddChild("Please enter value");
        menu[0][2].OnKeyPressed += (m, k) =>
        {
            if (k.Key == ConsoleKey.Enter)
            {
                var value = new TextInput(m.Position, 20);
                m.Content.Title = value.GetUserInput(out string _text) ? _text : m.Content.Title;
                m.ReRender();
            }
        };
        menu[0].OrientationOfChildren = Orientation.Horizontal;

        menu.AddChild("My SubMenu2");
        menu["My SubMenu2"].AddChild("My SubSubMenu");
        menu["My SubMenu2"][0].AddChild("My SubSubMenu2");
        menu["My SubMenu2"][0][0].AddChild("Sub1");
        menu["My SubMenu2"][0][0].AddChild("Sub2");
        menu["My SubMenu2"][0][0].AddChild("Sub3");
        menu["My SubMenu2"][0][0].OrientationOfChildren = Orientation.Horizontal;
        menu["My SubMenu2"][0].AddChild("My SubSubMenu2");
        menu["My SubMenu2"][0][1].AddChild("Sub1");
        menu["My SubMenu2"][0][1].AddChild("Sub2");
        menu["My SubMenu2"][0][1].AddChild("ActionMenu");
        menu["My SubMenu2"][0][1]["ActionMenu"].OnKeyPressed += SetItemMark;
        menu["My SubMenu2"][0][1].OrientationOfChildren = Orientation.Horizontal;
        menu["My SubMenu2"][0][1].PositionOffsetOfFirstChild = new Vector2(5, 0);
        menu["My SubMenu2"][0].AddChild("My SubSubMenu2");
        menu["My SubMenu2"][0][2].AddChild("Sub1");
        menu["My SubMenu2"][0][2].AddChild("Sub2");
        menu["My SubMenu2"][0][2].AddChild("Sub3");
        menu["My SubMenu2"][0][2].OrientationOfChildren = Orientation.Horizontal;
        //menu["My SubMenu2"][0][2].SetRenderer<RadioButtonContentRenderer>();
        menu["My SubMenu2"][0][2].OnKeyPressed += (m, k) =>
        {
            if (k.Key == ConsoleKey.Enter)
            {
                m.Content.IsMarked = !m.Content.IsMarked;
                m.Content.Render();
            }
        };
        menu["My SubMenu2"][0].OrientationOfChildren = Orientation.Vertical;
        menu["My SubMenu2"].AddChild("Sub2");
        menu["My SubMenu2"].AddChild("Sub3");
        menu["My SubMenu2"].SetRenderer<CheckboxContentRender>();
        menu["My SubMenu2"].OnKeyPressed += SetItemMark;
        menu["My SubMenu2"].Content.IsMarked = true;
        menu["My SubMenu2"].OnSelectionChanged += x =>
        {
            x.OldItem.IsChildrenVisible = false;
            x.NewItem.IsChildrenVisible = true;
            x.NewItem.ReRender();
        };
        menu["My SubMenu2"].OnSelectionRendered += x =>
        {
            x.Item.IsChildrenVisible = x.IsSelected;
            x.Item.ReRender();
        };

        menu.AddChild("Players");
        menu.AddChild("Pl");
        menu.AddChild("Plsdlfjksldkjfsldjfsldjflsjdfl");
        menu["Plsdlfjksldkjfsldjfsldjflsjdfl"].OnKeyPressed += SetItemMark;
        menu["Plsdlfjksldkjfsldjfsldjflsjdfl"].OnKeyPressed += SetItemMarkOnParent;
        menu.OrientationOfChildren = Orientation.Vertical;
        menu.PositionOffsetToNextChild = 1;
        //menu.SetRenderer<DefaultContentRender>();
        menu.Content.IsSelected = true;
        //menu.Content.IsMarked = false;
        menu.Render();

        //Console.ReadKey(true);
        //subsubsubMenu.SetRenderer<CheckboxContentRender>();


        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            _ = menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);


        Console.WriteLine("Press a key to exit");
        Console.ReadKey();
    }

    static void SetItemMark(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return;
        }

        //if (item.Parent is not null)
        //{
        //    item.Parent.ContentRenderer.IsMarked = !item.Parent.ContentRenderer.IsMarked;
        //    item.Parent.ContentRenderer.Render(item.Parent.Position);
        //}
        //if (item.Children.HaveChildren())
        //{
        //    foreach (IChildItem child in item.Children.GetChildren())
        //    {
        //        child.Item.Content.IsMarked = !child.Item.Content.IsMarked;
        //    }
        //    item.Render();
        //}

        item.Content.IsMarked = !item.Content.IsMarked;
        item.Content.Render();
        //item.SetRenderer<DefaultContentRender>();

        //item.ContentRenderer.Render(item.Position);
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
}