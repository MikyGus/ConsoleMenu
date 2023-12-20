// See https://aka.ms/new-console-template for more information
using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

Console.WriteLine("******** Console Menu ***********");
Console.WriteLine("\n\n\n");


var menu = new MenuItem("Settings")
{
    
    Position = new Vector2(1, 1)
};
menu.ContentRenderer().IsSelected = true;
menu.ContentRenderer().IsMarked = true;
menu.AddChildItem(1, new MenuItem("Players"));
menu.Render();
