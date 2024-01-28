﻿using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Program.Examples;
internal class HideUnhide
{
    public static void Run()
    {
        IMenuItem subMenu = new MenuItem("My SubMenu #1");
        IMenuItem subsubMenu1 = new MenuItem("Sub1");
        subsubMenu1.OnKeyPressed += ((m, k) =>
        {
            if (k.Key == ConsoleKey.Enter)
            {
                m.Content.IsMarked = !m.Content.IsMarked;
                m.Content.Render();
            }
        });
        subMenu.Children.Add(1, subsubMenu1);
        subMenu.Children.Add(2, new MenuItem("Sub2"));
        subMenu.OrientationOfChildren = Orientation.Horizontal;
        subMenu.OnKeyPressed += SetItemMarkOnParent;

        IMenuItem subMenu2 = new MenuItem("My SubMenu #2");
        subMenu2.OnKeyPressed += ((m, k) =>
        {
            if (k.Modifiers == ConsoleModifiers.Control && k.Key == ConsoleKey.H)
            {
                m.Children.IsVisible = !m.Children.IsVisible;
                m.ReRender();
            }
        });
        IMenuItem subMenu21 = new MenuItem("My SubMenu #21");
        IMenuItem subMenu211 = new MenuItem("My SubMenu #211");
        IMenuItem subMenu2111 = new MenuItem("My SubMenu #2111");
        subMenu2.Children.Add(1, subMenu21);
        subMenu21.Children.Add(1, subMenu211);
        subMenu211.Children.Add(1, subMenu2111);
        subMenu2.Children.Add(2, new MenuItem("Sub2"));

        IMenuItem subMenu3 = new MenuItem("My SubMenu #3");
        subMenu3.OnKeyPressed += SetItemMark;

        IMenuItem menu1 = new MenuItem("Settings");
        menu1.Children.Add(1, new MenuItem("First"));
        menu1.Children.Add(1, subMenu);
        menu1.Children.Add(2, subMenu2);
        menu1.Children.Add(3, subMenu3);
        menu1.OrientationOfChildren = Orientation.Horizontal;

        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.Children.Add(1, menu1);
        menu.Children.Add(2, new MenuItem("Hello, World"));
        //menu.Children.Add(2, new MenuItem("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent sit amet justo ac mauris hendrerit dapibus. Donec urna dolor, dapibus a libero sed, tempus luctus libero. Aliquam fringilla mi vitae pulvinar efficitur."));
        menu.Content.IsSelected = true;


        menu.Render();


        _ = Console.ReadKey();
        //subMenu2.IsVisible = true;
        //subMenu2.ReRender();

        subMenu2.EraseContent();

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
            menu.KeyPressed(keyInput);
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
            foreach (IChildItem child in item.Children.GetChildren())
            {
                child.Item.Content.IsMarked = !child.Item.Content.IsMarked;
            }
            item.Render();
        }
    }
}