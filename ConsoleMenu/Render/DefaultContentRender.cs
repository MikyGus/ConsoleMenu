namespace ConsoleMenu.Render;
public class DefaultContentRender : IContentRenderer
{
    public Vector2 AreaNeeded(IMenuItem menuItem) => new(menuItem.Title.Length + 2, 1);

    public void Render(IMenuItem menuItem)
    {

        ConsoleColor selectedColor = ConsoleColor.Blue;
        ConsoleColor bgColor;
        ConsoleColor fgColor;
        if (menuItem.IsMarked)
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
            menuItem.IsSelected ? "[" : " ",
            selectedColor,
            bgColor);
        ContentHelpers.WriteAtPosition(
            new Vector2(menuItem.Position.X + 1, menuItem.Position.Y),
            menuItem.Title,
            fgColor,
            bgColor);
        ContentHelpers.WriteAtPosition(
            new Vector2(menuItem.Position.X + 1 + menuItem.Title.Length, menuItem.Position.Y),
            menuItem.IsSelected ? "]" : " ",
            selectedColor,
            bgColor);
    }
}