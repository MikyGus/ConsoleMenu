using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;
public class DefaultContentRender : IContentRenderer
{
    public Vector2 AreaNeeded(IMenuItem menuItem) => new(menuItem.Content.Title.Length + 2, 1);

    public void Render(IMenuItem menuItem)
    {

        ConsoleColor selectedColor = ConsoleColor.Blue;
        ConsoleColor bgColor;
        ConsoleColor fgColor;
        if (menuItem.Content.IsMarked)
        {
            bgColor = ConsoleColor.Green;
            fgColor = ConsoleColor.Black;
        }
        else
        {
            bgColor = ConsoleColor.Gray;
            fgColor = ConsoleColor.Black;
        }
        ContentHelpers.WriteAtPosition(
            menuItem.Position,
            menuItem.Content.IsSelected ? "[" : " ",
            selectedColor,
            bgColor);
        ContentHelpers.WriteAtPosition(
            new Vector2(menuItem.Position.X + 1, menuItem.Position.Y),
            menuItem.Content.Title,
            fgColor,
            bgColor);
        ContentHelpers.WriteAtPosition(
            new Vector2(menuItem.Position.X + 1 + menuItem.Content.Title.Length, menuItem.Position.Y),
            menuItem.Content.IsSelected ? "]" : " ",
            selectedColor,
            bgColor);
    }
}