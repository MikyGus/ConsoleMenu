// See https://aka.ms/new-console-template for more information
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render;

Console.WriteLine("******** Console Menu ***********");
Console.WriteLine("\n\n\n");


var menu = new MenuItem("Settings")
{
    Position = new Vector2(1, 1)
};

var subMenu = new MenuItem("My SubMenu");
subMenu.Children.Add(1, new MenuItem("Sub1"));
subMenu.Children.Add(4, new MenuItem("Sub2"));
subMenu.Children.Add(1, new MenuItem("Sub3"));
subMenu.Children.ContentOrientation = ContentOrientation.Horizontal;

var subsubMenu2 = new MenuItem("My SubSubMenu2");
subsubMenu2.Children.Add(6, new MenuItem("Sub1"));
subsubMenu2.Children.Add(4, new MenuItem("Sub2"));
subsubMenu2.Children.Add(1, new MenuItem("Sub3"));
subsubMenu2.Children.ContentOrientation = ContentOrientation.Horizontal;

var myActionPackedMenuItem = new MenuItem("ActionMenu");
myActionPackedMenuItem.SetAction(SetItemMark);

var subsubMenu3 = new MenuItem("My SubSubMenu2");
subsubMenu3.Children.Add(6, new MenuItem("Sub1"));
subsubMenu3.Children.Add(4, new MenuItem("Sub2"));
subsubMenu3.Children.Add(1, myActionPackedMenuItem);
subsubMenu3.Children.ContentOrientation = ContentOrientation.Horizontal;
subsubMenu3.Children.PositionOffsetOfFirstChild = new Vector2(5, 0);

var subsubMenu4 = new MenuItem("My SubSubMenu2");
subsubMenu4.Children.Add(6, new MenuItem("Sub1"));
subsubMenu4.Children.Add(4, new MenuItem("Sub2"));
subsubMenu4.Children.Add(1, new MenuItem("Sub3"));
subsubMenu4.Children.ContentOrientation = ContentOrientation.Horizontal;

var subsubsubMenu = new MenuItem("My SubSubMenu");
subsubsubMenu.Children.Add(6, subsubMenu2);
subsubsubMenu.Children.Add(4, subsubMenu3);
subsubsubMenu.Children.Add(1, subsubMenu4);
subsubsubMenu.Children.ContentOrientation = ContentOrientation.Vetical;

var subMenu2 = new MenuItem("My SubMenu2");
subMenu2.Children.Add(6, subsubsubMenu);
subMenu2.Children.Add(4, new MenuItem("Sub2"));
subMenu2.Children.Add(1, new MenuItem("Sub3"));
subMenu2.SetRenderer<CheckboxContentRender>();
subMenu2.SetAction(SetItemMark);
subMenu2.ContentRenderer.IsMarked = true;

menu.Children.Add(1, subMenu);
menu.Children.Add(1, subMenu2);
menu.Children.Add(1, new MenuItem("Players"));
menu.Children.Add(1, new MenuItem("Pl"));
menu.Children.Add(1, new MenuItem("Plsdlfjksldkjfsldjfsldjflsjdfl"));
menu.Children.ContentOrientation = ContentOrientation.Vetical;
menu.Children.PositionOffsetToNextChild = 1;
menu.SetRenderer<DefaultContentRender>();
menu.ContentRenderer.IsSelected = true;
menu.ContentRenderer.IsMarked = false;
menu.Render();

ConsoleKeyInfo keyInput;
do
{
    keyInput = Console.ReadKey(true);
    menu.KeyPressed(keyInput);
} while (keyInput.Key != ConsoleKey.Escape);


Console.WriteLine("Press a key to exit");
Console.ReadKey();


bool SetItemMark(IMenuItem item, ConsoleKeyInfo key)
{
    if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.E)
    {
        return false;
    }

    //if (item.Parent is not null)
    //{
    //    item.Parent.ContentRenderer.IsMarked = !item.Parent.ContentRenderer.IsMarked;
    //    item.Parent.ContentRenderer.Render(item.Parent.Position);
    //}
    if (item.Children.HaveChildren())
    {
        foreach (IChildItem child in item.Children.GetChildren())
        {
            child.Item.ContentRenderer.IsMarked = !child.Item.ContentRenderer.IsMarked;
        }
        item.Render();
    }

    //item.ContentRenderer.IsMarked = !item.ContentRenderer.IsMarked;
    //item.SetRenderer<DefaultContentRender>();

    //item.ContentRenderer.Render(item.Position);
    return false;
}
