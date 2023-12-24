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
menu.ContentRenderer.IsSelected = true;
menu.ContentRenderer.IsMarked = true;
menu.Children.Add(1, new MenuItem("Players"));
menu.Children.Add(1, new MenuItem("Pl"));
menu.Children.Add(1, new MenuItem("Plsdlfjksldkjfsldjfsldjflsjdfl"));
var subMenu = new MenuItem("My SubMenu");
subMenu.Children.Add(1, new MenuItem("Sub1"));
subMenu.Children.Add(4, new MenuItem("Sub2"));
subMenu.Children.Add(1, new MenuItem("Sub3"));
menu.Children.Add(1, subMenu);
menu.Children.ContentOrientation = ContentOrientation.Horizontal;
menu.Children.PositionOffsetToNextChild = 15;
menu.Render();

ConsoleKeyInfo keyInput;
do
{
    keyInput = Console.ReadKey();
    menu.PerformAction(keyInput);
} while (keyInput.Key != ConsoleKey.Escape);


Console.WriteLine("Press a key to exit");
Console.ReadKey();
