// See https://aka.ms/new-console-template for more information
using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Managers;

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

var subsubsubMenu = new MenuItem("My SubSubMenu");
subsubsubMenu.Children.Add(6, subsubMenu2);
subsubsubMenu.Children.Add(4, new MenuItem("Sub2"));
subsubsubMenu.Children.Add(1, new MenuItem("Sub3"));


var subMenu2 = new MenuItem("My SubMenu2");
subMenu2.Children.Add(6, subsubsubMenu);
subMenu2.Children.Add(4, new MenuItem("Sub2"));
subMenu2.Children.Add(1, new MenuItem("Sub3"));

menu.ContentRenderer.IsSelected = true;
menu.ContentRenderer.IsMarked = true;
menu.Children.Add(1, subMenu);
menu.Children.Add(1, subMenu2);
menu.Children.Add(1, new MenuItem("Players"));
menu.Children.Add(1, new MenuItem("Pl"));
menu.Children.Add(1, new MenuItem("Plsdlfjksldkjfsldjfsldjflsjdfl"));
menu.Children.ContentOrientation = ContentOrientation.Vetical;
menu.Children.PositionOffsetToNextChild = 1;
menu.Render();

ConsoleKeyInfo keyInput;
do
{
    keyInput = Console.ReadKey();
    menu.PerformAction(keyInput);
} while (keyInput.Key != ConsoleKey.Escape);


Console.WriteLine("Press a key to exit");
Console.ReadKey();
