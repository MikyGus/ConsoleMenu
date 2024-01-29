using ConsoleMenu.Library.FormInput;
using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render;

namespace ConsoleMenu.Program.Playground;
internal class All
{
    public static void Run()
    {
        var menu = new MenuItem("Settings")
        {
            Position = new Vector2(1, 1)
        };

        var subMenu = new MenuItem("My SubMenu");
        subMenu.Children.Add(1, new MenuItem("Sub1"));
        subMenu.Children.Add(4, new MenuItem("Sub2"));
        var enterValueMenuItem = new MenuItem("Please enter value");
        enterValueMenuItem.OnKeyPressed += (m, k) =>
        {
            if (k.Key == ConsoleKey.Enter)
            {
                var value = new TextInput(m.Position, 20);
                m.Content.Title = value.GetUserInput(out string _text) ? _text : m.Content.Title;
                //value.Render();
                //value.EraseContent();
                //m.Content.Title = value.Text;
                m.ReRender();
            }
        };
        subMenu.Children.Add(1, enterValueMenuItem);
        subMenu.OrientationOfChildren = Orientation.Horizontal;

        var subsubMenu2 = new MenuItem("My SubSubMenu2");
        subsubMenu2.Children.Add(6, new MenuItem("Sub1"));
        subsubMenu2.Children.Add(4, new MenuItem("Sub2"));
        subsubMenu2.Children.Add(1, new MenuItem("Sub3"));
        subsubMenu2.OrientationOfChildren = Orientation.Horizontal;

        var myActionPackedMenuItem = new MenuItem("ActionMenu");
        myActionPackedMenuItem.OnKeyPressed += SetItemMark;

        var subsubMenu3 = new MenuItem("My SubSubMenu2");
        subsubMenu3.Children.Add(6, new MenuItem("Sub1"));
        subsubMenu3.Children.Add(4, new MenuItem("Sub2"));
        subsubMenu3.Children.Add(1, myActionPackedMenuItem);
        subsubMenu3.OrientationOfChildren = Orientation.Horizontal;
        subsubMenu3.PositionOffsetOfFirstChild = new Vector2(5, 0);

        var subsubMenu4 = new MenuItem("My SubSubMenu2");
        subsubMenu4.Children.Add(6, new MenuItem("Sub1"));
        subsubMenu4.Children.Add(4, new MenuItem("Sub2"));
        subsubMenu4.Children.Add(1, new MenuItem("Sub3"));
        subsubMenu4.OrientationOfChildren = Orientation.Horizontal;
        //subsubMenu4.SetRenderer<RadioButtonContentRenderer>();
        subsubMenu4.OnKeyPressed += (m, k) =>
        {
            if (k.Key == ConsoleKey.Enter)
            {
                m.Content.IsMarked = !m.Content.IsMarked;
                m.Content.Render();
            }
        };

        MenuItem subsubsubMenu = new MenuItem("My SubSubMenu");
        subsubsubMenu.Children.Add(6, subsubMenu2);
        subsubsubMenu.Children.Add(4, subsubMenu3);
        subsubsubMenu.Children.Add(1, subsubMenu4);
        subsubsubMenu.OrientationOfChildren = Orientation.Vertical;

        MenuItem subMenu2 = new MenuItem("My SubMenu2");
        subMenu2.Children.Add(6, subsubsubMenu);
        subMenu2.Children.Add(4, new MenuItem("Sub2"));
        subMenu2.Children.Add(1, new MenuItem("Sub3"));
        subMenu2.SetRenderer<CheckboxContentRender>();
        subMenu2.OnKeyPressed += SetItemMark;
        subMenu2.Content.IsMarked = true;
        subMenu2.OnSelectionChanged += x =>
        {
            x.OldItem.Item.Children.IsVisible = false;
            x.NewItem.Item.Children.IsVisible = true;
            x.NewItem.Item.ReRender();
        };
        subMenu2.OnSelectionRendered += x =>
        {
            x.Item.Item.Children.IsVisible = x.IsSelected;
            x.Item.Item.ReRender();
        };

        menu.Children.Add(1, subMenu);
        menu.Children.Add(1, subMenu2);
        menu.Children.Add(1, new MenuItem("Players"));
        menu.Children.Add(1, new MenuItem("Pl"));
        var CrazyNameMenu = new MenuItem("Plsdlfjksldkjfsldjfsldjflsjdfl");
        CrazyNameMenu.OnKeyPressed += SetItemMark;
        CrazyNameMenu.OnKeyPressed += SetItemMarkOnParent;
        menu.Children.Add(1, CrazyNameMenu);
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
            menu.KeyPressed(keyInput);
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